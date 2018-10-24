using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Web_App_Master.App_Start
{
    [Serializable]
    public class MasterLog:Serializers.Serializer<MasterLog>
    {
        public  string LastEntry()
        {
            return Entries.Last();
        }
        public  void Add(string entry)
        {
            Entries.Add(entry);
        }
        [XmlElement]
        public  List<string> Entries = new List<string>();
    }
}