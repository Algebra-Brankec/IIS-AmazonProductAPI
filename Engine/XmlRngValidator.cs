using Commons.Xml.Relaxng;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace IIS_AmazonProductAPI.Engine
{
    public class XmlRngValidator
    {
        public (bool, string) IsValid(Stream fileStream)
        {
            string result = "";
            //using (StringReader sr = new StringReader(content))
            //{
            XmlTextReader xtrXml = new XmlTextReader(fileStream);
            XmlTextReader grammar = new XmlTextReader(Directory.GetCurrentDirectory() + "/res/product_review.rng");

            using (RelaxngValidatingReader vr = new RelaxngValidatingReader(xtrXml, grammar))
            {
                try
                {
                    while (vr.Read()) { }

                    return (true, "");
                }
                catch (RelaxngException rngex)
                {
                    result =  rngex.Message;
                }

                return (false, result);
            }
            //}
        }
    }
}
