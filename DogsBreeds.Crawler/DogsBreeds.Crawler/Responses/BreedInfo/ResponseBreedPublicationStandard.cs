namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response of pubblication of breed
    /// </summary>
    public class ResponseBreedPublicationStandard
    {

        #region Private Properties

        /// <summary>
        /// Url getted from web-scraping
        /// </summary>
        private string _uriPubblication { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// Translation info of pubblication
        /// </summary>
        public ResponseTranslation Translation { get; set; }

        /// <summary>
        /// Date of pubblication
        /// </summary>
        public DateTime? DateOfPubblication { get; set; }

        /// <summary>
        /// Definitive uri odd pubblication
        /// </summary>
        public string UriPubblication
        {
            get
            {
                if (!string.IsNullOrEmpty(_uriPubblication.Trim()))
                {
                    return
                        "http://fci.be/" +
                        _uriPubblication
                        .Replace("../", string.Empty)
                    ;
                }

                return string.Empty;
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Response of pubblication of breed
        /// </summary>
        /// <param name="uriPubblication"></param>
        public ResponseBreedPublicationStandard(string uriPubblication)
        {
            _uriPubblication = uriPubblication;
        }

        #endregion

    }

}
