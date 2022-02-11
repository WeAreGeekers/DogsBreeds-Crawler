namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response of breed working trial
    /// </summary>
    public class ResponseBreedWorkingTrial
    {

        #region Private Properties

        /// <summary>
        /// Text that we received from web-scraping
        /// </summary>
        private string _text { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// Definite if is working trial
        /// </summary>
        public bool? WorkingTrial
        { 
            get
            {
                switch(_text)
                {
                    case "Subject to a working trial according to the FCI Breeds Nomenclature": return true;
                    // TODO:
                    default: return null;
                }
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Response of breed working trial
        /// </summary>
        /// <param name="text"></param>
        public ResponseBreedWorkingTrial(string text)
        {
            _text = text;
        }

        #endregion

    }

}
