using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StarragAwpCore.Helpers
{
    [Serializable]
    public class SettingsDBData : Serializers.Serializer<SettingsDBData>
    {
        public SettingsDBData()
        {
            Appname = XmlData = XmlData2 = XmlData3 = XmlData4 = XmlData5 = "";
        }
        [XmlElement]
        public string Appname { get; set; }
        [XmlElement]
        public string XmlData { get; set; }
        [XmlElement]
        public string XmlData2 { get; set; }
        [XmlElement]
        public string XmlData3 { get; set; }
        [XmlElement]
        public string XmlData4 { get; set; }
        [XmlElement]
        public string XmlData5 { get; set; }
    }


}
