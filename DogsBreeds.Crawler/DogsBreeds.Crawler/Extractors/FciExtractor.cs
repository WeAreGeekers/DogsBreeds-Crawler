using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeAreGeekers.DogsBreeds.Crawler.Responses;

namespace WeAreGeekers.DogsBreeds.Crawler.Extractors
{
    public static class FciExtractor
    {

        public static List<ResponseBreedGroup> ExtractFciGroup()
        {
            var baseUrlGroups = "http://fci.be/en/Nomenclature/";
            var web = new HtmlWeb();
            var doc = web.Load(baseUrlGroups);

            // Return data
            return doc
                .DocumentNode
                .SelectNodes("//div[@class='group']")
                .Select(s => new ResponseBreedGroup()
                {
                    GroupIndex = int.Parse(s.Descendants("a").FirstOrDefault()?.InnerHtml.ToUpper().Replace("GROUP", string.Empty).Trim()),
                    OfficialName = s.ParentNode.Descendants("span").FirstOrDefault()?.InnerHtml,
                    DetailPage = "http://fci.be" + s.Descendants("a").FirstOrDefault()?.GetAttributeValue<string>("href", string.Empty)
                })
                .ToList();
        }

        public static List<ResponseBreed> ExtractFciBreed(List<ResponseBreedGroup> listFciBreedGroup)
        {
            List<ResponseBreed> listFciBreed = new List<ResponseBreed>();

            // Iterate 
            listFciBreedGroup.ForEach(group =>
            {
                // Download section detail pages
                var web = new HtmlWeb();
                var doc = web.Load(group.DetailPage);

                doc.DocumentNode
                    .SelectNodes("//ul[@class='sections']")
                    .Elements("li")
                    .ToList()
                    .ForEach(section =>
                    {
                        ResponseBreedSection breedSection = new ResponseBreedSection()
                        {
                            SectionIndex = int.Parse(section.Descendants("span").FirstOrDefault().Descendants("b").FirstOrDefault().InnerText.ToUpper().Replace("SECTION", string.Empty).Replace(":", string.Empty).Trim()),
                            OfficialName = section.Descendants("span").FirstOrDefault().InnerText
                        };

                        // Clear official name
                        breedSection.OfficialName = breedSection.OfficialName.Substring(breedSection.OfficialName.IndexOf(":") + 1).Trim();

                        // TODO: Iterate origin country and post the breed 


                        string s = "";


                    });
            });

            // Return data
            return listFciBreed;
        }



    }
}
