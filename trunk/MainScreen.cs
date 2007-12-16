using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEPOSMon
{
    public partial class MainScreen : Form
    {
        Settings m_settings = Settings.GetInstance();
        SelectStarbases starbaseWindow;

        public MainScreen()
        {
            InitializeComponent();
            starbaseWindow = new SelectStarbases(this);
        }

        public void AddTab(Starbase starbase)
        {

            TabPage tp = new TabPage(starbase.StarbaseSystem.locationID);
            AddTab(tp);
            tp.Text = starbase.Moon.moonName;
            StarbaseMonitor sm = new StarbaseMonitor(starbase);
            sm.Parent = tp;
            sm.Dock = DockStyle.Fill;
        }

        public void AddTab(TabPage tp)
        {
            tabControl1.TabPages.Add(tp);
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
            clearTabs();
        }

        public void clearTabs()
        {
            tabControl1.TabPages.Clear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void aboutEvePOSMonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutDialog = new AboutBox();
            aboutDialog.ShowDialog();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void fuelCalculatorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FuelCalculator fuelCalculator = new FuelCalculator();
            fuelCalculator.Visible = true;
        }

        private void aPIKeysToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            LoginCharacterSelect lcs = new LoginCharacterSelect();
            if (lcs.ShowDialog() == DialogResult.OK)
            {
                m_settings.accountInfo = lcs.accountInfo;
            }
        }

        private void starbaseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            starbaseWindow.Visible = true;
        }
    }
}