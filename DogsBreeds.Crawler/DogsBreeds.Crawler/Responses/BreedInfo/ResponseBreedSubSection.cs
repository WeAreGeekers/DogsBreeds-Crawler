namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response of sub-section of breed
    /// </summary>
    public class ResponseBreedSubSection
    {

        #region Public Properties

        /// <summary>
        /// Ref to breed section
        /// </summary>
        public ResponseBreedSection Section { get; set; }

        /// <summary>
        /// Index of sub section inner section
        /// </summary>
        public int SubSectionIndex { get; set; }

        /// <summary>
        /// Official name of sub section
        /// </summary>
        public string OfficialName { get; set; }

        #endregion
    }

}
