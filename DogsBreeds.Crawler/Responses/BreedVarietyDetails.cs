namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response class of variety of breed
    /// </summary>
    public class BreedVarietyDetails
    {

        /// <summary>
        /// Index letter of variety by fci
        /// </summary>
        public string FciIndexLetter { get; set; }

        /// <summary>
        /// Official name of variety by fci
        /// </summary>
        public string FciOfficialName { get; set; }

        /// <summary>
        /// Variety is CACIB by fci
        /// </summary>
        public bool FciCacib { get; set; }

        /// <summary>
        /// List of sub varieties by fci
        /// </summary>
        public List<BreedSubVarietyDetails> FciSubVarieties { get; set; }

    }

}
