using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EVEPOSMon
{
    public class MoonInfo
    {
        public string moonId;
        public string moonName;
    }

    public class MoonData
    {
        public Hashtable Moons = new Hashtable();

        public static MoonData Load(string filename)
        {
            using (Stream s = new FileStream(filename, FileMode.Open))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(s);
                MoonData moonData = new MoonData();
                XmlNodeList rowsNodeList = doc.GetElementsByTagName("moon");
                foreach (XmlNode r in rowsNodeList)
                {
                    MoonInfo moon = new MoonInfo();
                    XmlAttributeCollection attrs = r.Attributes;
                    moon.moonId = attrs["moonID"].InnerText;
                    moon.moonName = attrs["moonName"].InnerText;
                    moonData.Moons.Add(moon.moonId, moon);
                }

                return moonData;
            }
        }

        public MoonInfo GetMoonInfo(string moonId)
        {
            if (Moons.ContainsKey(moonId))
            {
                return Moons[moonId] as MoonInfo;
            }
            return null;
        }
    }
}
