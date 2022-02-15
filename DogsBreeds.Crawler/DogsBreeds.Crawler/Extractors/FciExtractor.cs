using HtmlAgilityPack;
using System.Globalization;
using WeAreGeekers.DogsBreeds.Crawler.Responses;

namespace WeAreGeekers.DogsBreeds.Crawler.Extractors
{
    public static class FciExtractor
    {

        #region Public Methods

        /// <summary>
        /// Extractor of Fci Breeds detailed data
        /// </summary>
        /// <returns></returns>
        public static List<ResponseBreedDetailData> ExtractFromFciBreedDetailData()
        {
            // Extract definitive breeds: Groups - Sections - Breeds
            List<ResponseBreedGroup> listBreedGroups = ExtractFciGroups();
            List<ResponseBreedSection> listBreedSections = ExtractFciSections(listBreedGroups);
            List<ResponseBreed> listBreeds = ExtractFciBreeds(listBreedSections.ToList());

            // Extract provisional breeds
            List<ResponseBreed> listBreedsProvisional = ExtractFciBreedsProvisional(listBreedSections);

            // Merge breeds
            listBreeds.AddRange(listBreedsProvisional);

            // Map breeds info and return
            return MapBreedInfoIntoDetailData(listBreeds);
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Extract the list of Fci Breed Groups
        /// </summary>
        /// <returns></returns>
        private static List<ResponseBreedGroup> ExtractFciGroups()
        {
            var baseUrlGroups = "http://fci.be/en/Nomenclature/";
            var web = new HtmlWeb();
            var doc = web.Load(baseUrlGroups);

            // Return data
            return doc
                .DocumentNode
                .SelectNodes("//div[@class='group']")
                .Select(s => new ResponseBreedGroup()
                {
                    GroupIndex = int.Parse(s.Descendants("a").FirstOrDefault()?.InnerHtml.ToUpper().Replace("GROUP", string.Empty).Trim()),
                    OfficialName = s.ParentNode.Descendants("span").FirstOrDefault()?.InnerHtml,
                    DetailPage = "http://fci.be" + s.Descendants("a").FirstOrDefault()?.GetAttributeValue<string>("href", string.Empty)
                })
                .ToList();
        }

        /// <summary>
        /// Extract the list of Fci Breed Sections
        /// </summary>
        /// <param name="listDetailsPageSections"></param>
        /// <returns></returns>
        private static List<ResponseBreedSection> ExtractFciSections(List<ResponseBreedGroup> listBreedGroups)
        {
            List<ResponseBreedSection> listFciBreedSections = new List<ResponseBreedSection>();

            // Iterate 
            listBreedGroups.ForEach(group =>
            {
                // Download section detail pages
                var web = new HtmlWeb();
                var doc = web.Load(group.DetailPage);

                doc.DocumentNode
                    .SelectNodes("//ul[@class='sections']")
                    .Elements("li")
                    .ToList()
                    .ForEach(section =>
                    {
                        ResponseBreedSection breedSection = new ResponseBreedSection()
                        {
                            Group = group,
                            SectionIndex = int.Parse(section.Descendants("span").FirstOrDefault().Descendants("b").FirstOrDefault().InnerText.ToUpper().Replace("SECTION", string.Empty).Replace(":", string.Empty).Trim()),
                            OfficialName = section.Descendants("span").FirstOrDefault().InnerText,
                            Breeds = section
                                .Descendants("a")
                                .Where(w => w.HasClass("nom"))
                                .Select(breedATag =>
                                {
                                    ResponseBreed breed = new ResponseBreed()
                                    {
                                        DetailPage = "http://fci.be" + breedATag.GetAttributeValue<string>("href", string.Empty),
                                        Cacib = breedATag.ParentNode.ParentNode.Descendants("td")?
                                            .Where(w => w.HasClass("racecacib")).FirstOrDefault()?
                                            .Element("span")?
                                            .InnerText == "*"
                                    };

                                    return breed;
                                })
                                .ToList()
                        };

                        // sub-section
                        breedSection.SubSections = section
                            .Descendants("ul")
                            .Where(w => w.HasClass("soussections"))
                            .SelectMany(s => s.Elements("li"))
                            .Select(s =>
                            {
                                ResponseBreedSubSection responseBreedSubSection = new ResponseBreedSubSection()
                                {
                                    Section = breedSection,
                                    OfficialName = s.Descendants("span").FirstOrDefault().InnerText,
                                    SubSectionIndex = 0,
                                };

                                // Clear data                                
                                responseBreedSubSection.SubSectionIndex = int.Parse(
                                    responseBreedSubSection.OfficialName
                                        .Substring(0, responseBreedSubSection.OfficialName.IndexOf(' '))
                                        .Replace(breedSection.SectionIndex + ".", string.Empty)
                                        .Trim()
                                );
                                responseBreedSubSection.OfficialName = responseBreedSubSection.OfficialName.Substring(responseBreedSubSection.OfficialName.IndexOf(' ')).Trim();

                                // Ret data
                                return responseBreedSubSection;
                            })
                            .ToList();

                        // Clear official name
                        breedSection.OfficialName = breedSection.OfficialName.Substring(breedSection.OfficialName.IndexOf(":") + 1).Trim();

                        // Add section
                        listFciBreedSections.Add(breedSection);
                    });
            });

            // Return data
            return listFciBreedSections;
        }

        /// <summary>
        /// Extract the list of Fci Breeds
        /// </summary>
        /// <param name="listDetailsPageBreeds"></param>
        /// <returns></returns>
        private static List<ResponseBreed> ExtractFciBreeds(List<ResponseBreedSection> listBreedSections)
        {
            List<ResponseBreed> listFciBreed = new List<ResponseBreed>();

            // Iterate 
            listBreedSections
                .ForEach(breedSection =>
                {
                    breedSection
                        .Breeds
                        .ForEach(breed =>
                        {
                            // Extract info from detail page
                            ResponseBreed responseBreed = ExtractFciBreedFromDetailPage(
                                detailPage: breed.DetailPage,
                                listBreedSections: listBreedSections,
                                breedCacib: breed.Cacib
                                );

                            // Add response to list
                            listFciBreed.Add(responseBreed);
                        });
                });

            // Return data
            return listFciBreed;
        }

        /// <summary>
        /// Extract the response breed from breed detail page
        /// </summary>
        /// <returns></returns>
        private static ResponseBreed ExtractFciBreedFromDetailPage(string detailPage, List<ResponseBreedSection> listBreedSections, bool breedCacib = false)
        {
            // Download section detail pages
            var web = new HtmlWeb();
            var doc = web.Load(detailPage);

            // Prepare response
            ResponseBreed responseBreed = new ResponseBreed();

            // get detail page
            responseBreed.DetailPage = detailPage;

            // get cacib
            responseBreed.Cacib = breedCacib;

            // get fci code
            responseBreed.FciCode = doc.DocumentNode
                .Descendants("h2")
                .Where(w => w.HasClass("nom"))
                .FirstOrDefault()
                .Descendants("span")
                .ToList()[1]
                .InnerText;

            // get official name
            responseBreed.OfficialName = doc.DocumentNode
                .Descendants("h2")
                .Where(w => w.HasClass("nom"))
                .FirstOrDefault()
                .Descendants("span")
                .ToList()[0]
                .InnerText;

            // get name translations
            responseBreed.TranslationsName = doc.DocumentNode
                .Descendants("table")
                .Where(w => w.HasClass("racesgridview"))
                .FirstOrDefault()
                .Elements("tr")
                .Skip(1)
                .Select(s => new ResponseTranslation(s.Elements("td").ToList()[0].InnerText)
                {
                    Translation = s.Elements("td").ToList()[1].Element("span").InnerText
                })
                .ToList();

            // get publications
            responseBreed.Pubblications = doc.DocumentNode
                .Descendants("table")
                .Where(w => w.HasClass("racesgridview"))
                .FirstOrDefault()
                .Elements("tr")
                .Skip(1)
                .Select(s =>
                {
                    ResponseBreedPublicationStandard responseBreedPublicationStandard = new ResponseBreedPublicationStandard(
                        uriPubblication: s.Elements("td").ToList()[3].Element("a")?.GetAttributeValue<string>("href", string.Empty)
                    )
                    {
                        Translation = new ResponseTranslation(s.Elements("td").ToList()[0].InnerText)
                        {
                            Translation = s.Elements("td").ToList()[1].Element("span").InnerText
                        },
                        DateOfPubblication = null
                    };

                    if (s.Elements("td").ToList()[2].Element("span").InnerText.Trim() != "-")
                    {
                        responseBreedPublicationStandard.DateOfPubblication = DateTime.ParseExact(s.Elements("td").ToList()[2].Element("span").InnerText.Trim(), "M/d/yyyy", CultureInfo.InvariantCulture);
                    }

                    // Ret data
                    return responseBreedPublicationStandard;
                })
                .ToList();

            // get data from table 'racetable' (under pubblications)
            var listOfInfoEntries = doc.DocumentNode
                .Descendants("table")
                .Where(w => w.HasClass("racetable"))
                .SelectMany(s => s.Elements("tr"))
                .ToList();

            // Iterate entries
            listOfInfoEntries.ForEach(fe =>
            {
                string entryName = fe.Elements("td")?.ToList()[0].Element("span")?.InnerText;

                switch (entryName)
                {
                    // get date acceptance
                    case "Date of acceptance on a definitive basis by the FCI":
                        responseBreed.DateOfAcceptanceOnDefinitiveBasisByTheFci = DateTime.ParseExact(
                            fe.Elements("td")
                                .ToList()[1]
                                .Element("span")
                                .InnerText
                                .Trim(),
                            "M/d/yyyy",
                            CultureInfo.InvariantCulture
                        );
                        break;

                    // get official name language
                    case "Official authentic language":
                        responseBreed.OfficialNameLanguage = new ResponseTranslation(
                            fe.Elements("td")
                                .ToList()[1]
                                .Element("span")
                                .InnerText
                                .Trim()
                        );
                        break;

                    // get date acceptance
                    case "Date of publication of the official valid standard":
                        responseBreed.DateOfPubblicationOfTheOfficialValidStandard = DateTime.ParseExact(
                            fe.Elements("td")
                                .ToList()[1]
                                .Element("span")
                                .InnerText
                                .Trim(),
                            "M/d/yyyy",
                            CultureInfo.InvariantCulture
                        );
                        break;

                    // get status
                    case "Breed status":
                        responseBreed.Status = new ResponseBreedStatus(
                            fe.Elements("td")
                                .ToList()[1]
                                .Element("span")
                                .InnerText
                                .Trim()
                        );
                        break;

                    // get origin country
                    case "Country of origin of the breed":
                        responseBreed.OriginCountry = fe
                            .Elements("td")
                            .ToList()[1]
                            .Element("span")
                            .InnerText
                            .Trim();
                        break;

                    // get working trial
                    case "Working trial":
                        responseBreed.WorkingTrial = new ResponseBreedWorkingTrial(
                            fe.Elements("td")
                                .ToList()[1]
                                .Element("span")
                                .InnerText
                                .Trim()
                        );
                        break;

                    // get date of acceptance on provisional basis by the FCI
                    case "Date of acceptance on a provisional basis by the FCI":
                        responseBreed.DateOfAcceptanceOnProvisionalBasisByTheFci = DateTime.ParseExact(
                            fe.Elements("td")
                                .ToList()[1]
                                .Element("span")
                                .InnerText
                                .Trim(),
                            "M/d/yyyy",
                            CultureInfo.InvariantCulture
                        );
                        break;

                    // get sub-section
                    case "Subsection":
                        responseBreed.SubSection = responseBreed
                            .Section
                            .SubSections
                            .Find(f => f.OfficialName == fe.Elements("td").ToList()[1].Element("span").InnerText.Trim());
                        break;

                    // get patronage country
                    case "Country of patronage of the breed":
                        responseBreed.PatronageCountry = fe.Elements("td")
                               .ToList()[1]
                               .Element("span")
                               .InnerText
                               .Trim();
                        break;

                    // get development country
                    case "Country of development of the breed":
                        responseBreed.DevelopmentCountry = fe.Elements("td")
                               .ToList()[1]
                               .Element("span")
                               .InnerText
                               .Trim();
                        break;

                    // Choose to not manage
                    case "Section":
                        string sectionName = fe.Elements("td").ToList()[1].Element("span").InnerText.Trim();
                        responseBreed.Section = listBreedSections.Find(f => f.OfficialName == sectionName);
                        break;

                    default:
                        throw new Exception("Entry info name not managed: " + entryName);
                }
            });

            // get varieties
            responseBreed.Varieties = new List<ResponseBreedVariety>();

            // Iterate varieties
            doc.DocumentNode
                .Descendants("div")
                .Where(w => w.HasClass("varietes"))
                .FirstOrDefault()?
                .Elements("table")?
                .FirstOrDefault()?
                .Elements("tr")?
                .Skip(1)
                .ToList()
                .ForEach(variety =>
                {
                    var findSubVarieties = variety.Descendants("div").ToList().Find(f => f.HasClass("sousvarietes"));

                    if (findSubVarieties == null)
                    {
                        ResponseBreedVariety responseBreedVariety = new ResponseBreedVariety()
                        {
                            Breed = responseBreed,
                            Letter = variety.Elements("td").ToList()[0].Element("span").InnerText,
                            OfficialName = variety.Elements("td").ToList()[0].Element("span").InnerText,
                            Cacib = variety.Elements("td").ToList()[1].Element("span")?.InnerText == "*",
                        };

                        // Correct letter & official name
                        responseBreedVariety.Letter = responseBreedVariety.Letter.Substring(0, responseBreedVariety.Letter.IndexOf(')'));
                        responseBreedVariety.OfficialName = responseBreedVariety.OfficialName.Substring(responseBreedVariety.OfficialName.IndexOf(')') + 1).Trim();

                        // Ret data
                        responseBreed.Varieties.Add(responseBreedVariety);
                    }
                    else
                    {
                        // Sub-variety
                        responseBreed
                            .Varieties
                            .Last()
                            .SubVarietis = findSubVarieties
                                .Descendants("tr")
                                .Select(trSubVariety => new ResponseBreedSubVariety()
                                {
                                    Variety = responseBreed.Varieties.Last(),
                                    OfficialNames = trSubVariety
                                        .Elements("td")
                                        .ToList()[0]
                                        .Element("span")
                                        .InnerHtml
                                        .Split("<br>")
                                        .Where(w => !string.IsNullOrEmpty(w))
                                        .ToList(),
                                    Cacib = trSubVariety.Elements("td").ToList()[1].Element("span")?.InnerText == "*",
                                })
                                .ToList();
                    }
                });

            // get illustrations
            responseBreed.Illustrations = doc.DocumentNode
                .Descendants("ul")
                .Where(w => w.HasClass("illustrations"))
                .FirstOrDefault()
                .Elements("li")
                .Reverse()
                .Skip(1)
                .Reverse()
                .Select(s => new ResponseBreedIllustration(
                    uriIllustration: s.Element("a").GetAttributeValue<string>("href", string.Empty)
                ))
                .ToList();

            // Return data
            return responseBreed;
        }

        /// <summary>
        /// Extract the list of Fci provisional breeds
        /// </summary>
        /// <returns></returns>
        private static List<ResponseBreed> ExtractFciBreedsProvisional(List<ResponseBreedSection> listBreedSections)
        {
            // Init var
            List<ResponseBreed> listFciBreedProvisional = new List<ResponseBreed>();

            // Download pages of provisional breeds to get details page data
            var web = new HtmlWeb();
            var doc = web.Load("http://fci.be/en/nomenclature/provisoire.aspx");

            doc.DocumentNode
                .Descendants("a")
                .Where(w => w.HasClass("nom"))
                .Select(s => "http://fci.be" + s.GetAttributeValue<string>("href", string.Empty))
                .ToList()
                .ForEach(detailPage =>
                {
                    // Extract response breed
                    ResponseBreed responseBreed = ExtractFciBreedFromDetailPage(
                        detailPage: detailPage,
                        listBreedSections: listBreedSections
                    );

                    // Add to provisional list
                    listFciBreedProvisional.Add(responseBreed);                    
                });

            // Return data
            return listFciBreedProvisional;
        }

        /// <summary>
        /// Map the list of fci breeds
        /// </summary>
        /// <param name="listBreeds"></param>
        /// <returns></returns>
        private static List<ResponseBreedDetailData> MapBreedInfoIntoDetailData(List<ResponseBreed> listBreeds)
        {
            return listBreeds
                .Select(s => new ResponseBreedDetailData()
                {
                    GroupIndex = s.Section.Group.GroupIndex,
                    GroupOfficialName = s.Section.Group.OfficialName,
                    GroupFciDetailsPage = s.Section.Group.DetailPage,

                    SectionIndex = s.Section.SectionIndex,
                    SectionOfficialName = s.Section.OfficialName,

                    SubSectionIndex = s.SubSection?.SubSectionIndex,
                    SubSectionOfficialName = s.SubSection?.OfficialName,

                    BreedFciCode = s.FciCode,
                    BreedOfficialName = s.OfficialName,
                    BreedDetailsPage = s.DetailPage,
                    BreedOriginCountry = s.OriginCountry,
                    BreedPatronageCountry = s.PatronageCountry,
                    BreedDevelopmentCountry = s.DevelopmentCountry,
                    BreedStatus = s.Status.Status,
                    BreedDateOfFciAcceptanceDefinitive = s.DateOfAcceptanceOnDefinitiveBasisByTheFci,
                    BreedDateOfFciAccptanceProvisional = s.DateOfAcceptanceOnProvisionalBasisByTheFci,
                    BreedDateOfFciPublicationStandard = s.DateOfPubblicationOfTheOfficialValidStandard,

                })
                .ToList();
        }

        #endregion

    }
}
