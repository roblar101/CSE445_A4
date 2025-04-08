using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Program
    {
        public static string xmlURL = "Hotels.xml";
        public static string xmlErrorURL = "HotelsErrors.xml";
        public static string xsdURL = "Hotels.xsd";

        public static void Main(string[] args)
        {
            // 1) verify the valid xml file
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine("verification of valid xml:");
            Console.WriteLine(result);

            // 2) verify the erronous xml file
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine("verification of erronous xml:");
            Console.WriteLine(result);

            // 3) convert the valid xml to json and show the output
            result = Xml2Json(xmlURL);
            Console.WriteLine("xml to json conversion:");
            Console.WriteLine(result);

            // pause the console so u can read the output before it closes
            Console.ReadLine();
        }

        // this method checks an xml file against an xsd file and returns the errors or "no error"
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(null, xsdUrl);
                settings.ValidationType = ValidationType.Schema;

                System.Text.StringBuilder errors = new System.Text.StringBuilder();
                settings.ValidationEventHandler += (sender, e) =>
                {
                    errors.AppendLine($"Error: {e.Message}");
                };

                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) { }
                }

                return errors.Length == 0 ? "No Error" : errors.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // this method converts an xml file to a json string using newtonsoft.json
        // we use the basic serializexmlnode overload to avoid any ambiguous issues
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                return jsonText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}



