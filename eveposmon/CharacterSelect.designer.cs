namespace eveposmon
{
    partial class CharacterSelect
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
            this.lbCharacters = new System.Windows.Forms.ListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbCharacters
            // 
            this.lbCharacters.FormattingEnabled = true;
            this.lbCharacters.Location = new System.Drawing.Point(13, 13);
            this.lbCharacters.Name = "lbCharacters";
            this.lbCharacters.Size = new System.Drawing.Size(203, 69);
            this.lbCharacters.TabIndex = 0;
            this.lbCharacters.DoubleClick += new System.EventHandler(this.lbCharacters_DoubleClick);
            // 
            // btnSelect
            // 
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelect.Location = new System.Drawing.Point(66, 88);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // CharacterSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 121);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lbCharacters);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharacterSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CharacterSelect";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbCharacters;
        private System.Windows.Forms.Button btnSelect;
    }
}