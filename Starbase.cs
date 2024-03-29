using System;
using System.Collections.Generic;
using System.Text;
using EVEMon.Common;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace EVEPOSMon
{
    public class Fuel
    {
        [XmlElement]
        public string typeId;

        [XmlElement]
        public string quantity;

        [XmlElement]
        public string quantityUsedPerHour;

        [XmlElement]
        public TimeSpan timeRemaining;

        [XmlElement]
        public string name;

        [XmlElement]
        public string volume;
    }

    public class CompareFuelByTimeLeft : IComparer<Fuel>
    {
        public int Compare(Fuel x, Fuel y)
        {
            if (x.timeRemaining > y.timeRemaining)
            {
                return -1;
            }
            if (x.timeRemaining < y.timeRemaining)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public class Starbase
    {
        [XmlElement]
        public string itemId;

        [XmlElement]
        public string typeId;

        [XmlElement]
        public string locationId;

        [XmlElement]
        public string moonId;

        [XmlElement]
        public string state;

        [XmlElement]
        public DateTime stateTimestamp;

        [XmlElement]
        public DateTime onlineTimeStamp;

        [XmlElement]
        public DateTime cachedUntil;

        [XmlElement]
        public DateTime lastDownloaded;

        [XmlElement]
        public bool monitored;

        [XmlElement]
        public string nickname;

        [XmlElement]
        public double totalFuelVolume;

        [XmlElement]
        public double totalStrontiumVolume;

        [XmlElement]
        public int observedOzonePerHour;

        [XmlElement]
        public int observedWaterPerHour;

        #region map data

        private MapSystem starbaseSystem;
        public MapSystem StarbaseSystem
        {
            get
            {
                if (starbaseSystem == null)
                {
                    this.starbaseSystem = m_settings.mapData.GetSystemInfo(this.locationId);
                }
                return this.starbaseSystem;
            }
        }

        private ControlTower tower;
        public ControlTower Tower
        {
            get
            {
                if (tower == null)
                {
                    tower = m_settings.controlTowerTypes.GetTowerInfo(this.typeId);
                }
                return tower;
            }
        }

        private MoonInfo moon;
        public MoonInfo Moon
        {
            get
            {
                if (moon == null)
                {
                    moon = m_settings.moonData.GetMoonInfo(this.moonId);
                }
                return moon;
            }
        }

        #endregion

        #region General Settings

        [XmlElement]
        public string usageFlags;

        [XmlElement]
        public string deployFlags;

        [XmlElement]
        public string allowCorporationMembers;

        [XmlElement]
        public string allowAllianceMembers;

        [XmlElement]
        public string claimSovereignty;

        #endregion

        #region Combat Settings

        public class OnStandingDrop
        {
            public string enabled;
            public string standing;
        }

        public class OnStatusDrop
        {
            public string enabled;
            public string standing;
        }

        public class OnAgression
        {
            public string enabled;
        }

        public class OnCorporationWar
        {
            public string enabled;
        }

        [XmlElement]
        public OnStandingDrop onStandingDrop = new OnStandingDrop();

        [XmlElement]
        public OnStatusDrop onStatusDrop = new OnStatusDrop();

        [XmlElement]
        public OnAgression onAgression = new OnAgression();

        [XmlElement]
        public OnCorporationWar onCorporationWar = new OnCorporationWar();

        #endregion

        [XmlArray("fuelList")]
        public List<Fuel> FuelList = new List<Fuel>();

        private Settings m_settings = Settings.GetInstance();

        public override string ToString()
        {
            //MapSystem ms = m_settings.mapData.GetSystemInfo(locationId);
            ControlTower ct = m_settings.controlTowerTypes.GetTowerInfo(typeId);
            return ct.typeName + " -- " + StarbaseSystem.systemName + " -- " + StarbaseSystem.security + " -- " + ct.description;
        }

        /// <summary>
        /// Get the starbase list from the API, add the starbases to the availableStarbases list in Settings
        /// </summary>
        public static void LoadStarbaseListFromApi()
        {
            Settings settings = Settings.GetInstance();

            if (settings.starbaseList.cachedUntil != DateTime.MinValue &&
                DateTime.Now < settings.starbaseList.cachedUntil.ToLocalTime())
            {
                return;
            }

            if (settings.accountInfo.userId == null)
            {
                System.Windows.Forms.MessageBox.Show("API Keys have not been entered, please enter them.");
                return;
            }

            XmlDocument xdoc = EveSession.GetStarbaseList(settings.accountInfo.userId, settings.accountInfo.apiKey, settings.accountInfo.SelectedCharacter.characterId);
            XmlNode error = xdoc.DocumentElement.SelectSingleNode("descendant::error");
            if (error != null)
            {
                System.Windows.Forms.MessageBox.Show("There was an error: " + error.InnerText);
            }
            else
            {
                List<Starbase> newStarbasesList = new List<Starbase>();
                DateTime cachedUntil = EveSession.GetCacheExpiryUTC(xdoc);
                XmlNodeList starbases = xdoc.DocumentElement.SelectNodes("descendant::rowset/row");
                foreach (XmlNode starbaseNode in starbases)
                {
                    Starbase starbase = new Starbase();
                    starbase.LoadFromListApiXml(starbaseNode, cachedUntil);
                    newStarbasesList.Add(starbase);
                }

                for (int i = 0; i < newStarbasesList.Count; i++)
                {
                    // Here we are going to check each of the stabases on the new list
                    // If we have an unexpired cached version then we will use that instead
                    Starbase cachedStarbase = getCachedStarbase(newStarbasesList[i].itemId);
                    if (cachedStarbase != null)
                    {
                        if (isExpired(cachedStarbase))
                        {
                            // If the cached starbase is expired use the new one
                            // and make sure if it was checked it stays that way
                            newStarbasesList[i].monitored = cachedStarbase.monitored;
                        }
                        else
                        {
                            // The cached version isn't expired so use it instead of the
                            // new one
                            newStarbasesList[i] = cachedStarbase;
                        }
                    }
                }

                // The new list contains the cached and new items so lets use it
                // as our available list from now on
                settings.availableStarBases = newStarbasesList;

                settings.starbaseList.LastUpdated = DateTime.Now;
                settings.starbaseList.cachedUntil = cachedUntil;
                settings.starbaseList.SaveStarbaseListTo(settings.SerializeStarbaseListFilename);
            }
        }

        /// <summary>
        /// If we have a cached starbase with the specified itemId return it, otherwise
        /// return null
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private static Starbase getCachedStarbase(string itemId)
        {
            foreach (Starbase s in Settings.GetInstance().availableStarBases)
            {
                if (s.itemId == itemId)
                {
                    return s;
                }
            }

            return null;
        }

        /// <summary>
        /// A starbase is expired if we've passed it's cachedUntil date. If it has never had its
        /// details loaded then lastDownloaded will be DateTime.MinValue - we consider it expired
        /// because we need to get its details
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isExpired(Starbase s)
        {
            if (DateTime.Now.ToUniversalTime() >= s.cachedUntil || s.lastDownloaded == DateTime.MinValue)
            {
                return true;
            }

            return false;
        }

        public void LoadFromListApiXml(XmlNode starbaseNode, DateTime cachedUntil)
        {
            XmlAttributeCollection attrs = starbaseNode.Attributes;
            this.itemId = attrs["itemID"].InnerText;
            this.typeId = attrs["typeID"].InnerText;
            this.locationId = attrs["locationID"].InnerText;
            this.moonId = attrs["moonID"].InnerText;
            this.state = attrs["state"].InnerText;
            this.stateTimestamp = EveSession.ConvertCCPTimeStringToDateTime(attrs["stateTimestamp"].InnerText);
            this.onlineTimeStamp = EveSession.ConvertCCPTimeStringToDateTime(attrs["onlineTimestamp"].InnerText);
            this.cachedUntil = cachedUntil;
        }

        public void LoadStarbaseDetailsFromApi()
        {
            // Don't send a request to the api unless we're expired
            if (!isExpired(this))
            {
                return;
            }

            if (m_settings.accountInfo.userId == null || m_settings.accountInfo.apiKey == null || m_settings.accountInfo.SelectedCharacter.characterId == null)
            {
                return;
            }

            try
            {
                XmlDocument detailsXmlDoc = EveSession.GetStarbaseDetail(m_settings.accountInfo.userId, m_settings.accountInfo.apiKey, m_settings.accountInfo.SelectedCharacter.characterId, this.itemId);
                XmlNode error = detailsXmlDoc.DocumentElement.SelectSingleNode("descendant::error");
                if (error != null)
                {
                    throw new InvalidDataException(error.InnerText);
                }
                else
                {
                    LoadDetailsFromXml(detailsXmlDoc);
                }
            }
            catch
            {

            }


        }

        public void LoadDetailsFromXml(System.Xml.XmlDocument detailsXmlDoc)
        {
            this.cachedUntil = EveSession.GetCacheExpiryUTC(detailsXmlDoc);
            lastDownloaded = DateTime.Now;
            usageFlags = detailsXmlDoc.GetElementsByTagName("usageFlags")[0].InnerText;
            deployFlags = detailsXmlDoc.GetElementsByTagName("deployFlags")[0].InnerText;
            allowAllianceMembers = detailsXmlDoc.GetElementsByTagName("allowAllianceMembers")[0].InnerText;
            allowCorporationMembers = detailsXmlDoc.GetElementsByTagName("allowCorporationMembers")[0].InnerText;
            claimSovereignty = detailsXmlDoc.GetElementsByTagName("claimSovereignty")[0].InnerText;

            XmlAttributeCollection attrs;

            attrs = detailsXmlDoc.GetElementsByTagName("onStandingDrop")[0].Attributes;
            onStandingDrop.enabled = attrs["enabled"].InnerText;
            onStandingDrop.standing = attrs["standing"].InnerText;

            attrs = detailsXmlDoc.GetElementsByTagName("onStatusDrop")[0].Attributes;
            onStatusDrop.enabled = attrs["enabled"].InnerText;
            onStatusDrop.standing = attrs["standing"].InnerText;

            attrs = detailsXmlDoc.GetElementsByTagName("onAggression")[0].Attributes;
            onAgression.enabled = attrs["enabled"].InnerText;

            attrs = detailsXmlDoc.GetElementsByTagName("onCorporationWar")[0].Attributes;
            onCorporationWar.enabled = attrs["enabled"].InnerText;

            FuelList.Clear();
            XmlNodeList fuelNodeList = detailsXmlDoc.DocumentElement.SelectNodes("descendant::rowset/row");
            foreach (XmlNode fuelNode in fuelNodeList)
            {
                XmlAttributeCollection atts = fuelNode.Attributes;
                Fuel fuel = new Fuel();
                fuel.typeId = atts["typeID"].InnerText;
                fuel.quantity = atts["quantity"].InnerText;

                this.FuelList.Add(fuel);
            }
        }

        public static void SerializeStarbasesToFile(string filename, List<Starbase> starbaseList)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Starbase>));
            using (Stream s = new FileStream(filename, FileMode.Create))
            {
                serializer.Serialize(s, starbaseList);
            }
        }

    }
}
