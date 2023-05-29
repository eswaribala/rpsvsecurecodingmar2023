using BankAPIV7.Models;
using System.Xml.Linq;
using System.Xml;

namespace BankAPIV7.Services
{
    public class AuthorService : IAuthorService
    {
        public List<Author> Search(string input)
        {
            List<Author> searchResult = new List<Author>();
           
            var file = System.IO.Path.Combine("author.xml");

            XmlReaderSettings settings = new XmlReaderSettings();
            //DTD processing
            settings.DtdProcessing = DtdProcessing.Prohibit;
            settings.MaxCharactersFromEntities = 1024;
            settings.MaxCharactersInDocument = 2048;

            XmlReader reader = XmlReader.Create(file,settings);
            XDocument xmlDoc = XDocument.Load(reader);

            var query = from i in xmlDoc.Element("Authors").Elements("author")
                        where
                          i.Element("name").ToString().Contains(input) == true
                        select new
                        {
                            Data= i.Value
                        };

            foreach (var data in query)
            {
                searchResult.Add(new Author() { Data=data.ToString() });
            }

            return searchResult;
        }
    }
}
