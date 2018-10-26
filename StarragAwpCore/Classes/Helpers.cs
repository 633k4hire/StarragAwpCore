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
        [XmlElement]
        public byte[] Timestamp { get; set; }
    }

    [Serializable]//xml
    public class Asset : Serializers.Serializer<Asset>
    {
        public Asset Deserialize(string xml)
        {
            return this.DeserializeFromXmlString<Asset>(xml);
        }
        public string Serialize()
        {
            return this.SerializeToXmlString(this);
        }
        public static Asset Create(string assetName = "", string assetNumber = "")
        {

            Asset ass = new Asset().Init();
            ass.AssetName = assetName;
            ass.AssetNumber = assetNumber;
            return ass;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Asset()
        {
            Init();
        }
        private Asset Init()
        {
            Images = "";
            IsOut = false;
            weight = 1;
            History = new AssetHistory();
            AssetNumber = "0000";
            OrderNumber = "0";
            AssetName = PackingSlip = UpsLabel = ReturnReport = ShipTo = ServiceEngineer = PersonShipping = Barcode = Description = BarcodeImage = CalibrationCompany = CalibrationPeriod = "";
            IsOut = IsDamaged = OnHold = false;
            CalibrationHistory = new CalibrationLibrary();
            Documents = new List<string>();
            return this;
        }
        [XmlElement]
        public string FirstImage
        {
            get
            {
                if (this.Images == "")
                {
                    return "/Images/transparent.png";
                }
                var img = Images.Split(',')[0].Replace(",,,", "<@#$>").Replace(",", "").Replace("<@#$>", ",").Replace("Images", "").Replace("\\", "");
                img = "/Account/Images/" + AssetNumber + "/" + img;
                return img;
            }
        }
        [XmlElement]
        public string Images { get; set; }
        [XmlElement]
        public string AssetName { get; set; }
        [XmlElement]
        public string OrderNumber { get; set; }
        [XmlElement]
        public string ShipTo { get; set; }
        [XmlElement]
        public DateTime DateShipped { get; set; }
        [XmlElement]
        public DateTime LastCalibrated { get; set; }
        [XmlElement]
        public DateTime DateRecieved { get; set; }
        [XmlElement]
        public string DateRecievedString { get { return DateRecieved.ToString(); } }
        [XmlElement]
        public string LastCalibratedString { get { return LastCalibrated.ToString(); } }
        [XmlElement]
        public string DateShippedString { get { return DateShipped.ToString(); } }
        [XmlIgnore]
        public string DateShippedTicks { get { return DateShipped.Ticks.ToString(); } }
        [XmlElement]
        public string ServiceEngineer { get; set; }
        [XmlElement]
        public string PersonShipping { get; set; }
        [XmlElement]
        public string Barcode { get; set; }
        [XmlElement]
        public string AssetNumber { get; set; }
        [XmlElement]
        public string Description { get; set; }
        [XmlElement("BarcodeImage")]
        public string BarcodeImage { get; set; }
        [XmlElement("IsOut")]
        public bool IsOut { get; set; }
        [XmlElement("IsDamaged")]
        public bool IsDamaged { get; set; }
        [XmlElement("IsCalibrated")]
        public bool IsCalibrated { get; set; }
        [XmlElement("CalibrationCompany")]
        public string CalibrationCompany { get; set; }
        [XmlElement("CalibrationPeriod")]
        public string CalibrationPeriod { get; set; }
        [XmlElement("CalibrationHistory")]
        public CalibrationLibrary CalibrationHistory { get; set; }
        [XmlElement("OnHold")]
        public bool OnHold { get; set; }
        [XmlElement("weight")]
        public decimal weight { get; set; }
        [XmlElement("PackingSlip")]
        public string PackingSlip { get; set; }
        [XmlElement("UpsLabel")]
        public string UpsLabel { get; set; }
        [XmlElement("ReturnReport")]
        public string ReturnReport { get; set; }
        [XmlElement("History")]
        public AssetHistory History { get; set; }
        [XmlElement]
        public bool IsHistoryItem { get; set; }
        [XmlIgnore]
        public string Color
        {
            get
            {
                if (IsOut)
                    return "bg-sg-amber";
                if (IsDamaged)
                    return "bg-sg-red";
                if (OnHold)
                    return "bg-sg-violet";

                return "bg-sg-blue";
            }
        }
        [XmlElement]
        public List<string> Documents { get; set; }
        [XmlIgnore]
        public List<string> ImageList
        {
            get
            {
                return Images.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            set
            {
                Images = "";
                foreach (var img in value)
                {
                    Images += img + ",";
                }
            }
        }

    }

    [Serializable]
    public class AssetHistory : Serializers.Serializer<AssetHistory>
    {
        public AssetHistory Deserialize(string xml)
        {
            return this.DeserializeFromXmlString<AssetHistory>(xml);
        }
        public string Serialize()
        {
            return this.SerializeToXmlString(this);
        }
        public AssetHistory()
        {
            History = new List<Asset>();
        }
        [XmlElement]
        public List<Asset> History { get; set; }
    }

    [Serializable]
    public class CalibrationLibrary: Serializers.Serializer<CalibrationLibrary>
    {
        public string Xml { get; set; }
    }

    [Serializable]
    public class LockedCache:Serializers.Serializer<LockedCache>
    {
        private static readonly object CacheLock = new object();

        private static List<object> _Cache = new List<object>();

        public static List<object> Cache
        {
            get
            {
                lock (CacheLock)
                {
                    return _Cache;
                }
            }
            set
            {
                lock (CacheLock)
                {
                    _Cache = value;
                }
            }
        }

        public static void Push<T>(T obj)
        {

        }
    }
}
