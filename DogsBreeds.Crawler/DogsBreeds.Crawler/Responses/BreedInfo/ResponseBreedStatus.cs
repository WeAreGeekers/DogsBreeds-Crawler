namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response of breed status
    /// </summary>
    public class ResponseBreedStatus
    {

        #region Private Properties

        /// <summary>
        /// Text that we get from web-scraping
        /// </summary>
        private string _text { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// Definitive status of breed
        /// </summary>
        public string Status
        { 
            get
            {
                switch(_text)
                {
                    case "Recognized on a definitive basis": return "definitive";
                    // TODO: Provisional
                    default: return string.Empty;
                }
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Response of breed status
        /// </summary>
        /// <param name="text"></param>
        public ResponseBreedStatus(string text)
        {
            _text = text;
        }

        #endregion

    }

}
