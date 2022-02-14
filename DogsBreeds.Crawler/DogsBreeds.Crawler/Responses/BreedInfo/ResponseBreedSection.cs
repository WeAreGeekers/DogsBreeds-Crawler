namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response of section of breed
    /// </summary>
    public class ResponseBreedSection
    {

        #region Public Properties

        /// <summary>
        /// Ref to breed group
        /// </summary>
        public ResponseBreedGroup Group { get; set; }

        /// <summary>
        /// Index of section inner group
        /// </summary>
        public int SectionIndex { get; set; }

        /// <summary>
        /// Official name of section
        /// </summary>
        public string OfficialName { get; set; }

        /// <summary>
        /// List of details pages of breeds under this breed section
        /// </summary>
        public List<string> BreedDetailsPages { get; set; }

        /// <summary>
        /// List of sub section of this breed section
        /// </summary>
        public List<ResponseBreedSubSection> SubSections { get; set; }

        #endregion
    }

}
