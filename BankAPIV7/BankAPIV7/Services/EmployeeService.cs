using BankAPIV7.Models;
using Microsoft.AspNetCore;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace BankAPIV7.Services
{
    public class EmployeeService : IEmployeeService
    {
        public List<Employee> Search(string input)
        {
            string sanitizedInput = Sanitize(input);
            List<Employee> employees = new List<Employee>();
            XmlSchemaSet? schemaSet = new XmlSchemaSet();
            //  var xsdFile = System.IO.Path.Combine("employee.xsd");
            // FileStream stream = File.OpenRead(xsdFile);
            using (var stream = System.IO.File.Open("employee.xsd", FileMode.Open))
            {
                XmlSchema? sreader = XmlSchema.Read(stream, (s, e) =>
                {
                    var x = e.Message;
                });
                schemaSet.Add(sreader);

            }





             XmlReaderSettings settings = new XmlReaderSettings();
             settings.ValidationType = ValidationType.Schema;
             settings.Schemas = schemaSet;
             settings.DtdProcessing = DtdProcessing.Parse;

            var file = System.IO.Path.Combine("employee.xml");
            XmlReader reader = XmlReader.Create(file,settings);
            XmlDocument XmlDoc = new XmlDocument()
            {
                XmlResolver = null
            };
            XmlDoc.Load(reader);
            //vulnerability test
            XPathNavigator? nav = XmlDoc.CreateNavigator();
            XPathExpression? expr = nav.Compile(@"//Employee[Name[contains(text(),'" + sanitizedInput + "')]]");

            var matchedNodes = nav.Select(expr);
          

            foreach (XPathNavigator node in matchedNodes)
            {

                var employee = new Employee()
                {
                    Name = node.SelectSingleNode(nav.Compile("Name")).Value,
                    SSN = Convert.ToInt64(node.SelectSingleNode(nav.Compile("SSN")).Value),
                    DOB = DateTime.Parse(node.SelectSingleNode(nav.Compile("DateOfBirth")).Value) ,
                    EmployeeType = node.SelectSingleNode(nav.Compile("EmployeeType")).Value,
                    Salary=Convert.ToInt64(node.SelectSingleNode(nav.Compile("Salary")).Value)
                };
                employees.Add(employee);


            }

            return employees;
        }

        private string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input", "input cannot be null");
            }
            HashSet<char> whitelist = new HashSet<char>(@"-1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ");
            return string.Concat(input.Where(i => whitelist.Contains(i))); ;
        }
    }
}
