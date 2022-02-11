namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response of breed variety
    /// </summary>
    public class ResponseBreedVariety
    {

        #region Public Properties

        /// <summary>
        /// Ref to breed 
        /// </summary>
        public ResponseBreed Breed { get; set; }

        /// <summary>
        /// Index letter of breed variety
        /// </summary>
        public string Letter { get; set; }

        /// <summary>
        /// Official name of breed variety
        /// </summary>
        public string OfficialName { get; set; }

        /// <summary>
        /// Is cabib
        /// </summary>
        public bool Cacib { get; set; }

        /// <summary>
        /// List of sub varieties
        /// </summary>
        public List<ResponseBreedSubVariety> SubVarietis { get; set; }

        #endregion

    }

}
