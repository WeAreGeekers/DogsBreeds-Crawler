namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response with breed group info
    /// </summary>
    public class ResponseBreedGroup    
    {

        #region Public Properties

        /// <summary>
        /// Number of index group
        /// </summary>
        public int GroupIndex { get; set; }

        /// <summary>
        /// Official name of breed group
        /// </summary>
        public string OfficialName { get; set; }

        /// <summary>
        /// Detail page of FCI of group
        /// </summary>
        public string DetailPage { get; set; }

        #endregion

    }

}
