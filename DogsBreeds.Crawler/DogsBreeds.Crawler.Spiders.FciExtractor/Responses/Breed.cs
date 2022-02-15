using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeAreGeekers.DogsBreeds.Crawler.Spiders.FciExtractor.Responses
{

    /// <summary>
    /// Response object of Breed
    /// </summary>
    public class Breed
    {
        // TODO: Section (that have group)
        // TODO: Sub section

        public string Code { get; set; }
        public string OfficialName { get; set; }
        public string IsoOfficialLang { get; set; }

        // TODO: Translations

        // TODO: Pubblications

        // TODO: Varieties

        public List<string> ListImages { get; set; }




    }

}
