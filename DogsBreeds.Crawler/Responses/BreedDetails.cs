using System.Text.Json.Serialization;
using WeAreGeekers.DogsBreeds.Crawler.Enums;

namespace WeAreGeekers.DogsBreeds.Crawler.Responses
{

    /// <summary>
    /// Response class with details of the breed
    /// </summary>
    public class BreedDetails
    {

        /// <summary>
        /// Group index set by fci
        /// </summary>
        public int FciGroupIndex { get; set; }

        /// <summary>
        /// Group official name set by fci
        /// </summary>
        public string FciGroupOfficialName { get; set; }

        /// <summary>
        /// Group detail page set by fci
        /// </summary>
        public string FciGroupUriDetailPage { get; set; }

        /// <summary>
        /// Section index set by fci
        /// </summary>
        public int? FciSectionIndex { get; set; }

        /// <summary>
        /// Section official name set by fci
        /// </summary>
        public string FciSectionOfficialName { get; set; }

        /// <summary>
        /// Sub section index set by fci
        /// </summary>
        public int? FciSubSectionIndex { get; set; }

        /// <summary>
        /// Sub section official name set by fci
        /// </summary>
        public string FciSubSectionOfficialName { get; set; }

        /// <summary>
        /// Code assegnated from fci
        /// </summary>
        public int FciCode { get; set; }

        /// <summary>
        /// Official name set by fci
        /// </summary>
        public string FciOfficialName { get; set; }

        /// <summary>
        /// Iso lang of official name set by fci
        /// </summary>
        public string FciIsoOfficialName { get; set; }

        /// <summary>
        /// Uri detail page set by fci
        /// </summary>
        public string FciUriDetailPage { get; set; }

        /// <summary>
        /// List of translation of official name by fci
        /// </summary>
        public List<TranslationDetails> FciOfficialNameTranslations { get; set; }

        /// <summary>
        /// Date of acceptance by fci in provisional way of the breed
        /// </summary>
        public DateTime? FciDateOfAcceptanceOnProvisionalBasis { get; set; }

        /// <summary>
        /// Date of pubblication of official standard by fci
        /// </summary>
        public DateTime? FciDateOfPubblicationOfTheOfficialValidStandard { get; set; }

        /// <summary>
        /// Date of acceptance by fci in definitive way of the breed
        /// </summary>
        public DateTime? FciDateOfAcceptanceOnDefinitiveBasis { get; set; }

        /// <summary>
        /// The fci status of the breed
        /// </summary>
        public FciBreedStatus FciStatus { get; set; }

        /// <summary>
        /// Fci working trial status of the breed
        /// </summary>
        public FciBreedWorkingTrial FciWorkingTrial { get; set; }

        /// <summary>
        /// Origin country of the breed by fci
        /// </summary>
        public string[] FciOriginCountries { get; set; }

        /// <summary>
        /// Patronage country of the breed by fci
        /// </summary>
        public string[] FciPatronageCountries { get; set; }

        /// <summary>
        /// Devlopment country of the breed by fci
        /// </summary>
        public string[] FciDevelopmentCountries { get; set; }

        /// <summary>
        /// Define if the breed is 'cacib' or not for fci. (Can be false and true in varieties and sub-varieties)
        /// </summary>
        public bool FciCacib { get; set; }

        /// <summary>
        /// List of publication by fci
        /// </summary>
        public List<PublicationDetails> FciPublications { get; set; }

        /// <summary>
        /// List of varieties of the breed
        /// </summary>
        public List<BreedVarietyDetails> FciVarieties { get; set; }

        /// <summary>
        /// List of illustration of the breed by the fci
        /// </summary>
        public List<string> FciUriIllustrations { get; set; }

        /// <summary>
        /// List of education resources generate by fci
        /// </summary>
        public List<ResourceDetails> FciEducationResources { get; set; }

    }

}
