using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Program
    {
        // My GitHub URL's
        public static string xmlURL = "https://roblar101.github.io/CSE445_A4/Hotels.xml";
        public static string xmlErrorURL = "https://roblar101.github.io/CSE445_A4/HotelsErrors.xml";
        public static string xsdURL = "https://roblar101.github.io/CSE445_A4/Hotels.xsd";

        public static void Main(string[] args)
        {
            // Testing for valid xml file
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine("Valid XML Verification:");
            Console.WriteLine(result);

            // Test of the erroneous xml file
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine("Erroneous XML Verification:");
            Console.WriteLine(result);

            // Convert valid xml to json and display  output
            result = Xml2Json(xmlURL);
            Console.WriteLine("XML to JSON Conversion:");
            Console.WriteLine(result);
        }

        //  Validate the xml file against the  xsd file
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

        // Convert the xml file to json.
        // removed the ambiguous perameter
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);
                //
                // indicates whether to omit the root object.
                string jsonText = JsonConvert.SerializeXmlNode(doc, true);
                return jsonText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

