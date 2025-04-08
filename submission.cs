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
            // verify the valid xml file
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine("verification of valid xml:");
            Console.WriteLine(result);

            // verify the erronous xml file
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine("verification of erronous xml:");
            Console.WriteLine(result);

            // convert the valid xml to json and display the outpt
            result = Xml2Json(xmlURL);
            Console.WriteLine("xml to json conversion:");
            Console.WriteLine(result);

            // pause the console so u can see the output befor it closes.
            Console.ReadLine();
        }

        // this method validates an xml file against an xsd.
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

        // this method converts an xml file to a json string using newtonsoft.json.
        // I use the serializexmlnode method with no extra parameters to avoid any confusing issues.
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


