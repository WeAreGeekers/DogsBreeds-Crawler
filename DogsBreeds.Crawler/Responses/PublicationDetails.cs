namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response class of detail of publication
    /// </summary>
    public class PublicationDetails
    {

        /// <summary>
        /// Iso lang of pubblication
        /// </summary>
        public string IsoLang { get; set; }

        /// <summary>
        /// Date of pubblication
        /// </summary>
        public DateTime DatePublication { get; set; }

        /// <summary>
        /// Uri of detail page of publication
        /// </summary>
        public string UriDetailPage { get; set; }

    }

}
