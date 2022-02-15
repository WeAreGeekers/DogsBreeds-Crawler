namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response breed detailed data
    /// </summary>
    public class ResponseBreedDetailData
    {

        //
        // Group info
        //

        public int GroupIndex { get; set; }
        public string GroupOfficialName { get; set; }
        public string GroupFciDetailsPage { get; set; }

        //
        // Section info
        //

        public int SectionIndex { get; set; }
        public string SectionOfficialName { get; set; }

        //
        // Sub section info
        //

        public int? SubSectionIndex { get; set; }
        public string SubSectionOfficialName { get; set; }

        //
        // Breed info
        //

        public string BreedFciCode { get; set; }
        public string BreedOfficialName { get; set; }

        // TODO: Official Breed Language
        // TODO: Official name translations
        // TODO: List of official publications

        public string BreedDetailsPage { get; set; }
        public string BreedOriginCountry { get; set; }
        public string BreedPatronageCountry { get; set; }
        public string BreedDevelopmentCountry { get; set; }

        // TODO: Breed Cacib
        // TODO: Breed Working Trial
        
        public string BreedStatus { get; set; }
        public DateTime? BreedDateOfFciAcceptanceDefinitive { get; set; }
        public DateTime? BreedDateOfFciAccptanceProvisional { get; set; }
        public DateTime? BreedDateOfFciPublicationStandard { get; set; }
        
        // TODO: Breed Varieties
        // TODO: Breed illustrations

    }

}
