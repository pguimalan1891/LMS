using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace CommonClasses
{
    public class JsonGen
    {
        static JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        public static string ReturnJSONSerialized(List<Dictionary<string, object>> lstInfo)
        {

            StringBuilder sb = new StringBuilder();
            jsSerializer.Serialize(lstInfo, sb);
            return sb.ToString();
        }

        public static string ReturnJSONSerialized(object obj)
        {
            StringBuilder sb = new StringBuilder();
            jsSerializer.Serialize(obj, sb);
            return sb.ToString();
        }
    }

    public class HttpContextHelper
    {
        public static HttpContextBase HttpContext
        {
            get
            {
                HttpContextWrapper context =
                    new HttpContextWrapper(System.Web.HttpContext.Current);
                return (HttpContextBase)context;
            }
        }
    }

    public class XMLHelper
    {
        public static string ToXML(object obj)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, obj);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            T result;
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (TextReader tr = new StringReader(xml))
            {
                result = (T)ser.Deserialize(tr);
            }
            return result;
        }
    }

    public class StringHelper
    {
        public static string IsStrNull(string strInput)
        {
            return strInput == null ? string.Empty : strInput.Trim();
            
        }
    }
}
