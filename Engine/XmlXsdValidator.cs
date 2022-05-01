using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace IIS_AmazonProductAPI.Engine
{
    public class XmlXsdValidator
    {
        public (bool, string) IsValid(Stream fileStream)
        {
            XmlReader xmlReader = null;

            try
            {
                XmlTextReader reader = new XmlTextReader(Directory.GetCurrentDirectory() + "/res/product_review.xsd");
                XmlSchema myschema = XmlSchema.Read(reader, null);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(myschema);
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += new ValidationEventHandler(xmlReaderSettingsValidationEventHandler);

                xmlReader = XmlReader.Create(fileStream, settings);

                while (xmlReader.Read()) { }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
            }

            return (true, "");
        }

        static void xmlReaderSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
