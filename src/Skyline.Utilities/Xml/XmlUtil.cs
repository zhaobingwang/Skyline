using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Skyline.Utilities
{
    public static class XmlUtil
    {
        /// <summary>
        /// Xml转对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="xml">Xml字符串</param>
        /// <returns>目标对象</returns>
        public static T ToObject<T>(string xml)
        {
            if (xml.IsNullOrWhiteSpace())
                return default;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader stringReader = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }

        /// <summary>
        /// 对象转Xml
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <returns>Xml字符串</returns>
        public static string ToXml(object target, XmlSerializeOptions options = null)
        {
            if (target == null)
                return string.Empty;

            XmlSerializer xmlSerializer = new XmlSerializer(target.GetType());
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            if (options == null)    // 设置默认值
            {
                xmlWriterSettings.OmitXmlDeclaration = true;
                ns.Add("", "");
            }
            else
            {
                xmlWriterSettings.OmitXmlDeclaration = options.OmitXmlDeclaration;
                if (options.OmitXmlSerializerNamespaces)
                {
                    ns.Add("", "");
                }
            }

            using (StringWriter stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
            {
                xmlSerializer.Serialize(xmlWriter, target, ns);
                return stringWriter.ToString();
            }
        }
    }

    public class XmlSerializeOptions
    {
        public bool OmitXmlDeclaration { get; set; } = true;
        public bool OmitXmlSerializerNamespaces { get; set; } = true;
    }
}
