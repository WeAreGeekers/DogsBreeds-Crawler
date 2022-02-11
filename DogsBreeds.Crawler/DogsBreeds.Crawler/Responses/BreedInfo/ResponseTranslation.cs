namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response of translation with ISO
    /// </summary>
    public class ResponseTranslation
    {

        #region Private Properties

        /// <summary>
        /// Lang that we received from web-scraping
        /// </summary>
        private string _lang { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// Iso of lang from web-scraping
        /// </summary>
        public string IsoLang
        {
            get
            {
                return _lang;
            }
        }

        /// <summary>
        /// The translation text
        /// </summary>
        public string Translation { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Response of translation with ISO
        /// </summary>
        /// <param name="lang"></param>
        public ResponseTranslation(string lang)
        {
            _lang = lang.Trim();
        }

        #endregion

    }
}
