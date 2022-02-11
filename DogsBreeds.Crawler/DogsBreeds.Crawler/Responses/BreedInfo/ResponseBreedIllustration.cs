namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response of illustration of breed
    /// </summary>
    public class ResponseBreedIllustration
    {

        #region Private Properties

        /// <summary>
        /// Url getted from web-scraping
        /// </summary>
        private string _uriIllustration { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// Definitire uri of illustration 
        /// </summary>
        public string Uri
        {
            get
            {
                return
                    "http://fci.be" +
                    _uriIllustration
                ;
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Response of illustration of breed
        /// </summary>
        /// <param name="uriIllustration"></param>
        public ResponseBreedIllustration(string uriIllustration)
        {
            _uriIllustration = uriIllustration;
        }

        #endregion

    }

}
