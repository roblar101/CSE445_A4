using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

namespace ConsoleApp1
{
    public class Program
    {
        // Updated with your exact GitHub Pages URL
        public static string xmlURL = "https://roblar101.github.io/CSE445_A4/Hotels.xml";
        public static string xmlErrorURL = "https://roblar101.github.io/CSE445_A4/HotelsErrors.xml";
        public static string xsdURL = "https://roblar101.github.io/CSE445_A4/Hotels.xsd";

        public static void Main(string[] args)
        {
            // Test the methods based on the assignment requirements.
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine("Valid XML Verification:");
            Console.WriteLine(result);

            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine("Erroneous XML Verification:");
            Console.WriteLine(result);

            result = Xml2Json(xmlURL);
            Console.WriteLine("XML to JSON Conversion:");
            Console.WriteLine(result);
        }

        // Validates the XML against the XSD
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

        // Converts valid XML to JSON
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);
                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);
                return jsonText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
