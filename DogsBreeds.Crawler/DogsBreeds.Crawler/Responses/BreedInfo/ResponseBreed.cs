namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response with breed info
    /// </summary>
    public class ResponseBreed
    {

        #region Public Properties

        /// <summary>
        /// Ref to breed section
        /// </summary>
        public ResponseBreedSection Section { get; set; }

        /// <summary>
        /// The unique fci code
        /// </summary>
        public string FciCode { get;set; }

        /// <summary>
        /// Official name of breed (in official language)
        /// </summary>
        public string OfficialName { get; set; }

        /// <summary>
        /// Official lang of official breed name
        /// </summary>
        public ResponseTranslation OfficialNameLanguage { get; set; }

        /// <summary>
        /// Breed name translations
        /// </summary>
        public List<ResponseTranslation> TranslationsName { get; set; }

        /// <summary>
        /// Fci publications
        /// </summary>
        public List<ResponseBreedPublicationStandard> Pubblications { get; set; }

        /// <summary>
        /// Date of acceptance on definitive basis by fci
        /// </summary>
        public DateTime? DateOfAcceptanceOnDefinitiveBasisByTheFci { get; set; }

        /// <summary>
        /// Date of acceptance on provisional basis by fci
        /// </summary>
        public DateTime? DateOfAcceptanceOnProvisionalBasisByTheFci { get; set; }

        /// <summary>
        /// Date of pubblication of official standard
        /// </summary>
        public DateTime? DateOfPubblicationOfTheOfficialValidStandard { get; set; }

        /// <summary>
        /// Breed status
        /// </summary>
        public ResponseBreedStatus Status { get; set; }

        /// <summary>
        /// Origin country of breed
        /// </summary>
        public string OriginCountry { get; set; }

        /// <summary>
        /// Breed is enable for working trial
        /// </summary>
        public ResponseBreedWorkingTrial WorkingTrial { get; set; }

        /// <summary>
        /// List of varieties
        /// </summary>
        public List<ResponseBreedVariety> Varieties { get; set; }

        /// <summary>
        /// List of illustration
        /// </summary>
        public List<ResponseBreedIllustration> Illustrations { get; set; }

        #endregion

    }

}
