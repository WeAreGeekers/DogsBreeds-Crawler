namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response of breed sub variety
    /// </summary>
    public class ResponseBreedSubVariety
    {

        #region Public Properties

        /// <summary>
        /// Ref to breed variety
        /// </summary>
        public ResponseBreedVariety Variety { get; set; }

        /// <summary>
        /// List of official names of sub-varieties
        /// </summary>
        public List<string> OfficialNames { get; set; }

        /// <summary>
        /// Is cabib
        /// </summary>
        public bool Cacib { get; set; }

        #endregion

    }

}
