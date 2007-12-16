using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EVEPOSMon
{
    public partial class CharacterSelect : Form
    {
        AccountInfo accountInfo;
        public Character selectedCharacter;

        public CharacterSelect(AccountInfo accountInfo)
        {
            InitializeComponent();
            this.accountInfo = accountInfo;
        }

        private void CharacterSelect_Load(object sender, EventArgs e)
        {
            foreach (Character c in accountInfo.characters)
            {
                lbCharacters.Items.Add(c);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lbCharacters.SelectedItems.Count != 1)
            {
                DialogResult = DialogResult.None;
            }

            selectedCharacter = lbCharacters.SelectedItem as Character;
        }
    }
}