namespace EVEPOSMon
{
    partial class LoginCharacterSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUserId = new System.Windows.Forms.Label();
            this.lblApiKey = new System.Windows.Forms.Label();
            this.tbUserId = new System.Windows.Forms.TextBox();
            this.tbApiKey = new System.Windows.Forms.TextBox();
            this.lblCharacter = new System.Windows.Forms.Label();
            this.tbCharacter = new System.Windows.Forms.TextBox();
            this.btnGetCharacters = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(12, 83);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(46, 13);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "User ID:";
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(12, 110);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(48, 13);
            this.lblApiKey.TabIndex = 0;
            this.lblApiKey.Text = "API Key:";
            // 
            // tbUserId
            // 
            this.tbUserId.Location = new System.Drawing.Point(65, 83);
            this.tbUserId.Name = "tbUserId";
            this.tbUserId.Size = new System.Drawing.Size(155, 20);
            this.tbUserId.TabIndex = 1;
            // 
            // tbApiKey
            // 
            this.tbApiKey.Location = new System.Drawing.Point(66, 107);
            this.tbApiKey.Name = "tbApiKey";
            this.tbApiKey.Size = new System.Drawing.Size(338, 20);
            this.tbApiKey.TabIndex = 1;
            // 
            // lblCharacter
            // 
            this.lblCharacter.AutoSize = true;
            this.lblCharacter.Location = new System.Drawing.Point(12, 166);
            this.lblCharacter.Name = "lblCharacter";
            this.lblCharacter.Size = new System.Drawing.Size(56, 13);
            this.lblCharacter.TabIndex = 0;
            this.lblCharacter.Text = "Character:";
            // 
            // tbCharacter
            // 
            this.tbCharacter.Enabled = false;
            this.tbCharacter.Location = new System.Drawing.Point(75, 166);
            this.tbCharacter.Name = "tbCharacter";
            this.tbCharacter.Size = new System.Drawing.Size(145, 20);
            this.tbCharacter.TabIndex = 2;
            // 
            // btnGetCharacters
            // 
            this.btnGetCharacters.Location = new System.Drawing.Point(226, 166);
            this.btnGetCharacters.Name = "btnGetCharacters";
            this.btnGetCharacters.Size = new System.Drawing.Size(31, 20);
            this.btnGetCharacters.TabIndex = 3;
            this.btnGetCharacters.Text = "...";
            this.btnGetCharacters.UseVisualStyleBackColor = true;
            this.btnGetCharacters.Click += new System.EventHandler(this.btnGetCharacters_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(267, 208);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(73, 24);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(356, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 39);
            this.label1.TabIndex = 5;
            this.label1.Text = "Please enter your API key information here\r\nThis information can be found on the " +
                "eve website.  \r\nYour FULL key must be used to access information about starbases" +
                ".";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(165, 61);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(220, 13);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://myeve.eve-online.com/api/default.asp";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Your keys can be found here:";
            // 
            // LoginCharacterSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 235);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnGetCharacters);
            this.Controls.Add(this.tbCharacter);
            this.Controls.Add(this.tbApiKey);
            this.Controls.Add(this.tbUserId);
            this.Controls.Add(this.lblCharacter);
            this.Controls.Add(this.lblApiKey);
            this.Controls.Add(this.lblUserId);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginCharacterSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginCharacterSelect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.TextBox tbUserId;
        private System.Windows.Forms.TextBox tbApiKey;
        private System.Windows.Forms.Label lblCharacter;
        private System.Windows.Forms.TextBox tbCharacter;
        private System.Windows.Forms.Button btnGetCharacters;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
    }
}