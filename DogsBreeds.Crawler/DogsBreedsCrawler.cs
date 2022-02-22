using WeAreGeekers.DogsBreeds.Crawler.Extensions;
using WeAreGeekers.DogsBreeds.Crawler.Responses;
using WeAreGeekers.DogsBreeds.Crawler.Spiders.FCI;

namespace WeAreGeekers.DogsBreeds.Crawler
{

    /// <summary>
    /// DogsBreeds crawler object
    /// </summary>
    public class DogsBreedsCrawler
    {

        #region Private Properties (Spiders)

        /// <summary>
        /// Spider of FCI (Federation Cynologique Internationale)
        /// </summary>
        private FCISpider _fciSpider { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// DogsBreeds crawler object
        /// </summary>
        public DogsBreedsCrawler()
        {
            _fciSpider = new FCISpider(cacheData: true);
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Gte the complete list of breeds
        /// </summary>
        /// <returns></returns>
        public List<BreedDetails> GetBreeds()
        {
            // Init var
            List<BreedDetails> listBreeds = new List<BreedDetails>();

            // Extract fci data from spider
            List<Spiders.FCI.Responses.Breed> fciBreeds = _fciSpider.GetBreeds();

            // Map fci data into breed details
            listBreeds = MapFciBreedsIntoList(listBreeds, fciBreeds);

            // Return data
            return listBreeds;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Private method that map fci breed into breed details liest
        /// </summary>
        /// <param name="listBreeds"></param>
        /// <param name="fciBreeds"></param>
        /// <returns></returns>
        private List<BreedDetails> MapFciBreedsIntoList(List<BreedDetails> listBreeds, List<Spiders.FCI.Responses.Breed> fciBreeds)
        {
            // Iterate fci breeds
            fciBreeds.ForEach(fciBreed =>
            {
                // Check if breed exist
                var findBreed = listBreeds.Find(f => f.FciCode == fciBreed.Code);
                if (findBreed == null)
                {
                    // Add it to list
                    BreedDetails breedDetails = new BreedDetails()
                    {
                        FciGroupIndex = fciBreed.Group.Index,
                        FciGroupOfficialName = fciBreed.Group.OfficialName,
                        FciGroupUriDetailPage = fciBreed.Group.DetailPage,
                        FciSectionIndex = fciBreed.Section?.Index,
                        FciSectionOfficialName = fciBreed.Section?.OfficialName,
                        FciSubSectionIndex = fciBreed.SubSection?.Index,
                        FciSubSectionOfficialName = fciBreed.SubSection?.OfficialName,
                        FciCode = fciBreed.Code,
                        FciOfficialName = fciBreed.OfficialName,
                        FciIsoOfficialName = fciBreed.IsoOfficialLang,
                        FciUriDetailPage = fciBreed.DetailPage,
                        FciOfficialNameTranslations = fciBreed.OfficialNameTranslations.Select(s => new TranslationDetails()
                        {
                            IsoLang = s.Item1,
                            Translation = s.Item2
                        }).ToList(),
                        FciDateOfAcceptanceOnProvisionalBasis = fciBreed.DateOfAcceptanceOnProvisionalBasisByTheFci,
                        FciDateOfPubblicationOfTheOfficialValidStandard = fciBreed.DateOfPubblicationOfTheOfficialValidStandard,
                        FciDateOfAcceptanceOnDefinitiveBasis = fciBreed.DateOfAcceptanceOnDefinitiveBasisByTheFci,
                        FciStatus = fciBreed.Status.ToFciBreedStatus(),
                        FciWorkingTrial = fciBreed.WorkingTrial.ToFciWorkingTrial(),
                        FciOriginCountries = fciBreed.OriginCountries,
                        FciPatronageCountries = fciBreed.PatronageCountries,
                        FciDevelopmentCountries = fciBreed.DevelopmentCountries,
                        FciCacib = fciBreed.Cacib,
                        FciPublications = fciBreed.Pubblications.Select(s => new PublicationDetails()
                        {
                            IsoLang = s.Item1,
                            DatePublication = s.Item2,
                            UriDetailPage = s.Item3
                        }).ToList(),
                        FciVarieties = fciBreed.Varieties.Select(s => new BreedVarietyDetails()
                        {
                            FciIndexLetter = s.IndexLetter,
                            FciOfficialName = s.OfficialName,
                            FciCacib = s.Cacib,
                            FciSubVarieties = s.SubVarieties.Select(ss => new BreedSubVarietyDetails()
                            {
                                FciOfficialNames = ss.OfficialNames,
                                FciCacib = ss.Cacib
                            }).ToList()
                        }).ToList(),
                        FciUriIllustrations = fciBreed.ListImages,
                        FciEducationResources = fciBreed.EducationResources?.Select(s => new ResourceDetails()
                        {
                            FileExtesion = s.Item1,
                            FileType = s.Item2,
                            Uri = s.Item3
                        })
                        .ToList(),
                    };

                    // Add breed details to list
                    listBreeds.Add(breedDetails);
                }
            });

            // Return data
            return listBreeds;
        }

        #endregion

    }

}
