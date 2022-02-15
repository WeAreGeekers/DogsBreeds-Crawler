using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeAreGeekers.DogsBreeds.Crawler.Spiders.FciExtractor.Responses;

namespace WeAreGeekers.DogsBreeds.Crawler.Spiders.FciExtractor
{

    /// <summary>
    /// Object with methods to extract by web-scraping the data from FCI (www.fci.be)
    /// </summary>
    public class FciSpider    
    {

        #region Private Const

        /// <summary>
        /// 
        /// </summary>
        private static readonly string FCI_BASE_URI = "http://fci.be";

        /// <summary>
        /// 
        /// </summary>
        private static readonly string FCI_URI_PROVISIONAL_PAGE = "http://fci.be/en/nomenclature/provisoire.aspx";

        #endregion


        #region Private Properties

        /// <summary>
        /// 
        /// </summary>
        private bool _cacheData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private List<string> _listDetailPagesDefinitivelBreeds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private List<string> _listDetailPagesProvisionalBreeds { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Object with methods to extract by web-scraping the data from FCI (www.fci.be)
        /// </summary>
        public FciSpider(bool cacheData)
        {
            _cacheData = cacheData;
            ClearCache();
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Method that clear the data caching
        /// </summary>
        public void ClearCache()
        {
            _listDetailPagesDefinitivelBreeds = null;
            _listDetailPagesProvisionalBreeds = null;            
        }

        /// <summary>
        /// Get the list of all breeds recognize from FCI (definitive and provisional)
        /// </summary>
        /// <returns></returns>
        public List<Breed> GetBreeds()
        {
            // Init var
            List<Breed> listBreeds = new List<Breed>();

            // Add definitive breeds
            listBreeds.AddRange(GetDefinitiveBreeds());

            // Add provisional breeds
            listBreeds.AddRange(GetProvisionalBreeds());

            // Return breeds
            return listBreeds;
        }

        /// <summary>
        /// Get the list of all definitive breeds recognize from FCI
        /// </summary>
        /// <returns></returns>
        public List<Breed> GetDefinitiveBreeds()
        {
            // Init var
            List<Breed> listBreeds = new List<Breed>();

            // Extract detail pages of definitive breeds
            List<string> listDetailPagesDefinitiveBreeds = ExtractListOfDetailsPagesOfDefinitiveBreed();

            // Iterate detail pages
            listDetailPagesDefinitiveBreeds.ForEach(detailPage =>
            {
                listBreeds.Add(ExtractDataFromBreedDetailPage(detailPage));
            });

            // Return breeds
            return listBreeds;
        }

        /// <summary>
        /// Get the list of all provisional breeds recognize from FCI
        /// </summary>
        /// <returns></returns>
        public List<Breed> GetProvisionalBreeds()
        {
            // Init var
            List<Breed> listBreeds = new List<Breed>();

            // Extract detail pages of provisional breeds
            List<string> listDetailPagesProvisionalBreeds = ExtractListOfDetailsPagesOfProvisionalBreed();

            // Iterate detail pages
            listDetailPagesProvisionalBreeds.ForEach(detailPage =>
            {
                listBreeds.Add(ExtractDataFromBreedDetailPage(detailPage));
            });

            // Return breeds
            return listBreeds;
        }

        #endregion


        #region Private Methods (Utilities)

        private string GetIsoCodeLang(string labelLang)
        {
            switch (labelLang)
            {

                default:
                    throw new Exception();  // TODO: New type of exception !
            }
        }


        #endregion


        #region Private Methods (Extract)

        /// <summary>
        /// Private method that extract the list of breed detail pages of definitive breeds
        /// </summary>
        /// <returns></returns>
        private List<string> ExtractListOfDetailsPagesOfDefinitiveBreed()
        {
            // Check cache data
            if (_cacheData && _listDetailPagesDefinitivelBreeds != null) return _listDetailPagesDefinitivelBreeds;

            // Init var
            List<string> listDetailPagesDefinitiveBreeds = new List<string>();

            // TODO: 



            // TODO: Extract groups 


            /*
            // Download html page and parse it
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(FCI_URI_PROVISIONAL_PAGE);

            // Return list with uri detail page
            List<string> listDetailPagesProvisionalBreeds = doc.DocumentNode
                .Descendants("a")
                .Where(w => w.HasClass("nom"))
                .Select(s => FCI_BASE_URI + s.GetAttributeValue<string>("href", string.Empty))
                .ToList();

            // Cache data
            if (_cacheData)
            {
                _listDetailPagesDefinitivelBreeds = listDetailPagesProvisionalBreeds;
            }
            */

            // return data
            return listDetailPagesProvisionalBreeds;
        }

        /// <summary>
        /// Private method that extract the list of breed detail pages of provisional breeds
        /// </summary>
        /// <returns></returns>
        private List<string> ExtractListOfDetailsPagesOfProvisionalBreed()
        {
            // Check cache data
            if (_cacheData && _listDetailPagesProvisionalBreeds != null) return _listDetailPagesProvisionalBreeds;

            // Init var
            List<string> listDetailPagesProvisionalBreeds = new List<string>();

            // Download html page and parse it
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(FCI_URI_PROVISIONAL_PAGE);

            // Get list with uri detail page
            listDetailPagesProvisionalBreeds = doc.DocumentNode
                .Descendants("a")
                .Where(w => w.HasClass("nom"))
                .Select(s => FCI_BASE_URI + s.GetAttributeValue<string>("href", string.Empty))
                .ToList();

            // Cache data
            if (_cacheData)
            {
                _listDetailPagesProvisionalBreeds = listDetailPagesProvisionalBreeds;
            }

            // return data
            return listDetailPagesProvisionalBreeds;
        }

        /// <summary>
        /// Private method that extract all data from breed detail page
        /// </summary>
        /// <param name="uriDetailPage"></param>
        /// <returns></returns>
        private Breed ExtractDataFromBreedDetailPage(string uriDetailPage)
        {
            // Init var
            Breed breed = new Breed();

            // Download page
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(uriDetailPage);

            // Get Code
            breed.Code = doc.DocumentNode.Descendants("h2").FirstOrDefault(w => w.HasClass("nom")).Descendants("span").ToList()[1].InnerText.Trim();

            // Get Official Name
            breed.OfficialName = doc.DocumentNode.Descendants("h2").FirstOrDefault(w => w.HasClass("nom")).Descendants("span").ToList()[0].InnerText.Trim();

            // Get translations
            // TODO:

            // Get pubblications
            // TODO: 

            // Get data from info table
            // TODO: Iterate !

            // Get varieties
            // TODO:

            // Get illustrations
            breed.ListImages = doc.DocumentNode.Descendants("ul").FirstOrDefault(w => w.HasClass("illustrations"))
                .Elements("li")
                .Reverse().Skip(1).Reverse()    // Remove basic anatomy image (always last)
                .Select(s => FCI_BASE_URI + s.Element("a").GetAttributeValue<string>("href", string.Empty))
                .ToList();

            // Return data
            return breed;
        }


        #endregion


    }

}
