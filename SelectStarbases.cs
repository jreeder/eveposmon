using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEPOSMon
{
    public partial class SelectStarbases : Form
    {
        List<Starbase> m_starbasesList = new List<Starbase>();

        public SelectStarbases()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLoadStations_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = EVEMonWebRequest.LoadXml(@"http://www.exa-nation.com/corp/StarbaseList.xml.aspx");

            string starbaseListError;
            DateTime cachedUntil;

            XmlNode error = xdoc.DocumentElement.SelectSingleNode("descendant::error");
            if (error != null)
            {
                starbaseListError = error.InnerText;
                throw new InvalidDataException(starbaseListError);
            }
            else
            {
                cachedUntil = EveSession.GetCacheExpiryUTC(xdoc);
                XmlNodeList starbases = xdoc.DocumentElement.SelectNodes("descendant::rowset/row");
                foreach (XmlNode starbaseNode in starbases)
                {
                    XmlAttributeCollection atts = starbaseNode.Attributes;
                    Starbase starbase = new Starbase();
                    starbase.itemId = atts["itemID"].InnerText;
                    starbase.typeId = atts["typeID"].InnerText;
                    starbase.locationId = atts["locationID"].InnerText;
                    starbase.moonId = atts["moonID"].InnerText;
                    starbase.state = atts["state"].InnerText;
                    starbase.stateTimestamp = EveSession.ConvertCCPTimeStringToDateTime(atts["stateTimestamp"].InnerText);
                    starbase.onlineTimeStamp = EveSession.ConvertCCPTimeStringToDateTime(atts["onlineTimestamp"].InnerText);
                    m_starbasesList.Add(starbase);
                }
            }

            foreach (Starbase s in m_starbasesList)
            {
                lbStations.Items.Add(s);
            }
        }

        private void btnGetStationInfo_Click(object sender, EventArgs e)
        {
            Starbase starbase = lbStations.SelectedItem as Starbase;
            XmlDocument xdoc = EVEMonWebRequest.LoadXml(@"http://www.exa-nation.com/corp/StarbaseDetail.xml.aspx?itemId=" + starbase.itemId);

            string starbaseError;
            DateTime cachedUntil;

            XmlNode error = xdoc.DocumentElement.SelectSingleNode("descendant::error");
            if (error != null)
            {
                starbaseError = error.InnerText;
                throw new InvalidDataException(starbaseError);
            }
            else
            {
                cachedUntil = EveSession.GetCacheExpiryUTC(xdoc);
                starbase.lastDownloaded = DateTime.Now;
                starbase.usageFlags = xdoc.GetElementsByTagName("usageFlags")[0].InnerText;
                starbase.deployFlags = xdoc.GetElementsByTagName("deployFlags")[0].InnerText;
                starbase.allowAllianceMembers = xdoc.GetElementsByTagName("allowAllianceMembers")[0].InnerText;
                starbase.allowCorporationMembers = xdoc.GetElementsByTagName("allowCorporationMembers")[0].InnerText;
                starbase.claimSovereignty = xdoc.GetElementsByTagName("claimSovereignty")[0].InnerText;

                XmlAttributeCollection attrs;

                attrs = xdoc.GetElementsByTagName("onStandingDrop")[0].Attributes;
                starbase.onStandingDrop.enabled = attrs["enabled"].InnerText;
                starbase.onStandingDrop.standing = attrs["standing"].InnerText;

                attrs = xdoc.GetElementsByTagName("onStatusDrop")[0].Attributes;
                starbase.onStatusDrop.enabled = attrs["enabled"].InnerText;
                starbase.onStatusDrop.standing = attrs["standing"].InnerText;

                attrs = xdoc.GetElementsByTagName("onAggression")[0].Attributes;
                starbase.onAgression.enabled = attrs["enabled"].InnerText;


                attrs = xdoc.GetElementsByTagName("onCorporationWar")[0].Attributes;
                starbase.onCorporationWar.enabled = attrs["enabled"].InnerText;

                XmlNodeList fuelNodeList = xdoc.DocumentElement.SelectNodes("descendant::rowset/row");
                foreach (XmlNode fuelNode in fuelNodeList)
                {
                    XmlAttributeCollection atts = fuelNode.Attributes;
                    Fuel fuel = new Fuel();
                    fuel.typeId = atts["typeID"].InnerText;
                    fuel.quantity = atts["quantity"].InnerText;
                    starbase.FuelList.Add(fuel);
                }
            }

            StarbaseInfo starbaseInfo = new StarbaseInfo(starbase);
            starbaseInfo.Show();
        }
    }
}