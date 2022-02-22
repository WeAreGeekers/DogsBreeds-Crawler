namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response class of sub variety of breed
    /// </summary>
    public class BreedSubVarietyDetails
    {

        /// <summary>
        /// Official names of sub-variety by fci
        /// </summary>
        public string[] FciOfficialNames { get; set; }

        /// <summary>
        /// Sub-variety is CACIB by fci
        /// </summary>
        public bool FciCacib { get; set; }

    }

}
