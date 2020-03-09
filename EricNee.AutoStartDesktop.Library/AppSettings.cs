using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace EricNee.AutoStartDesktop.Library
{
    [DataContract]
    public class AppSettings
    {
        [DataMember]
        public bool DisabledAltF4 { get; set; }
        [DataMember]
        public bool DisabledWindowsKey { get; set; }

        [DataMember]
        public bool HiddenTaskbar { get; set; }

        [DataMember]
        public string CultureName { get; set; }

        public string ToFormatString()
        {
            return ToString(this);
        }

        public static string ToString(AppSettings appSettings)
        {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(AppSettings));
            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream, Encoding.UTF8))
                {
                    xmlSerializer.Serialize(streamWriter, appSettings);
                    var buffer = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(buffer, 0, (int)stream.Length);
                    return Encoding.UTF8.GetString(buffer);
                }
            }
        }

        public static AppSettings ToObject(string source)
        {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(AppSettings));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(source)))
            {
                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    return (AppSettings)xmlSerializer.Deserialize(streamReader);
                }
            }
        }
    }
}
