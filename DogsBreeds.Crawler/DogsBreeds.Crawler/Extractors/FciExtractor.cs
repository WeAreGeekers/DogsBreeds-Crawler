using HtmlAgilityPack;
using System.Globalization;
using WeAreGeekers.DogsBreeds.Crawler.Responses;

namespace WeAreGeekers.DogsBreeds.Crawler.Extractors
{
    public static class FciExtractor
    {

        #region Public Methods

        public static List<ResponseBreedDetailData> ExtractFromFciBreedDetailData()
        {
            // Extract definitive breeds: Groups - Sections - Breeds
            List<ResponseBreedGroup> listBreedGroups = ExtractFciGroups();
            List<ResponseBreedSection> listBreedSections = ExtractFciSections(listBreedGroups);
            List<ResponseBreed> listBreeds = ExtractFciBreeds(listBreedSections.ToList());

            // TODO:
            // 1. Check all data in every pages
            // 2. Check cabib from sections
            // 3. Extract provisional breeds
            // 4. Map breeds into 'DetailData' (create its folder under 'responses')
            // 5. Manager all data (need to be clear) and translations (need iso lang)

            return null;
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
                            BreedDetailsPages = section
                                .Descendants("a")
                                .Where(w => w.HasClass("nom"))
                                .Select(breedATag => "http://fci.be" + breedATag.GetAttributeValue<string>("href", string.Empty))
                                .ToList()
                        };

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
                        .BreedDetailsPages
                        .ForEach(detailPage =>
                        {
                            // Download section detail pages
                            var web = new HtmlWeb();
                            var doc = web.Load(detailPage);

                            // Prepare response
                            ResponseBreed responseBreed = new ResponseBreed();

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

                            // get date acceptance
                            var findDateAcceptance = doc.DocumentNode
                                .Descendants("table")
                                .Where(w => w.HasClass("racetable"))
                                .SelectMany(s => s.Elements("tr"))
                                .ToList()
                                .Find(w => w.Elements("td")?.ToList()[0].Element("span")?.InnerText == "Date of acceptance on a definitive basis by the FCI");
                            if (findDateAcceptance != null)
                            {
                                responseBreed.DateOfAcceptanceOnDefinitiveBasisByTheFci = DateTime.ParseExact(
                                    findDateAcceptance
                                        .Elements("td")
                                        .ToList()[1]
                                        .Element("span")
                                        .InnerText
                                        .Trim(),
                                    "M/d/yyyy",
                                    CultureInfo.InvariantCulture
                                );
                            }

                            // get official name language
                            var findOfficialNameLanguage = doc.DocumentNode
                                .Descendants("table")
                                .Where(w => w.HasClass("racetable"))
                                .SelectMany(s => s.Elements("tr"))
                                .ToList()
                                .Find(w => w.Elements("td")?.ToList()[0].Element("span")?.InnerText == "Official authentic language");
                            if (findOfficialNameLanguage != null)
                            {
                                responseBreed.OfficialNameLanguage = new ResponseTranslation(
                                    findOfficialNameLanguage
                                        .Elements("td")
                                        .ToList()[1]
                                        .Element("span")
                                        .InnerText
                                        .Trim()
                                );
                            }

                            // get date acceptance
                            var findDatePublication = doc.DocumentNode
                                .Descendants("table")
                                .Where(w => w.HasClass("racetable"))
                                .SelectMany(s => s.Elements("tr"))
                                .ToList()
                                .Find(w => w.Elements("td")?.ToList()[0].Element("span")?.InnerText == "Date of publication of the official valid standard");
                            if (findDatePublication != null)
                            {
                                responseBreed.DateOfPubblicationOfTheOfficialValidStandard = DateTime.ParseExact(
                                    findDatePublication
                                        .Elements("td")
                                        .ToList()[1]
                                        .Element("span")
                                        .InnerText
                                        .Trim(),
                                    "M/d/yyyy",
                                    CultureInfo.InvariantCulture
                                );
                            }

                            // get status
                            var findStatus = doc.DocumentNode
                                .Descendants("table")
                                .Where(w => w.HasClass("racetable"))
                                .SelectMany(s => s.Elements("tr"))
                                .ToList()
                                .Find(w => w.Elements("td")?.ToList()[0].Element("span")?.InnerText == "Breed status");
                            if (findStatus != null)
                            {
                                responseBreed.Status = new ResponseBreedStatus(
                                    findStatus
                                        .Elements("td")
                                        .ToList()[1]
                                        .Element("span")
                                        .InnerText
                                        .Trim()
                                );
                            }

                            // get origin country
                            var findOriginCountry = doc.DocumentNode
                                .Descendants("table")
                                .Where(w => w.HasClass("racetable"))
                                .SelectMany(s => s.Elements("tr"))
                                .ToList()
                                .Find(w => w.Elements("td")?.ToList()[0].Element("span")?.InnerText == "Country of origin of the breed");
                            if (findOriginCountry != null)
                            {
                                responseBreed.OriginCountry = findOriginCountry
                                    .Elements("td")
                                    .ToList()[1]
                                    .Element("span")
                                    .InnerText
                                    .Trim();
                            }

                            // get working trial
                            var findWotkingTrial = doc.DocumentNode
                                .Descendants("table")
                                .Where(w => w.HasClass("racetable"))
                                .SelectMany(s => s.Elements("tr"))
                                .ToList()
                                .Find(w => w.Elements("td")?.ToList()[0].Element("span")?.InnerText == "Working trial");
                            if (findWotkingTrial != null)
                            {
                                responseBreed.WorkingTrial = new ResponseBreedWorkingTrial(
                                    findWotkingTrial
                                        .Elements("td")
                                        .ToList()[1]
                                        .Element("span")
                                        .InnerText
                                        .Trim()
                                );
                            }

                            // get date of acceptance on provisional basis by the FCI
                            var findDateAcceptanceOnProvisionalBasis = doc.DocumentNode
                                .Descendants("table")
                                .Where(w => w.HasClass("racetable"))
                                .SelectMany(s => s.Elements("tr"))
                                .ToList()
                                .Find(w => w.Elements("td")?.ToList()[0].Element("span")?.InnerText == "Date of acceptance on a provisional basis by the FCI");
                            if (findDateAcceptanceOnProvisionalBasis != null)
                            {
                                responseBreed.DateOfAcceptanceOnProvisionalBasisByTheFci = DateTime.ParseExact(
                                    findDateAcceptanceOnProvisionalBasis
                                        .Elements("td")
                                        .ToList()[1]
                                        .Element("span")
                                        .InnerText
                                        .Trim(),
                                    "M/d/yyyy",
                                    CultureInfo.InvariantCulture
                                );
                            }

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

                            // Add response to list
                            listFciBreed.Add(responseBreed);
                        });
                });

            // Return data
            return listFciBreed;
        }

        #endregion

    }
}
