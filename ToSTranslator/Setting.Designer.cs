
namespace ToSTranslator
{
    partial class Setting
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
            this.settingPanel = new System.Windows.Forms.TabControl();
            this.tabMIN = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.minUSER = new System.Windows.Forms.TextBox();
            this.minSECRET = new System.Windows.Forms.TextBox();
            this.minKEY = new System.Windows.Forms.TextBox();
            this.minURL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabDeepL = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deepKEY = new System.Windows.Forms.TextBox();
            this.deepURL = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMsgReplace = new System.Windows.Forms.ComboBox();
            this.cancel = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.minLink = new System.Windows.Forms.LinkLabel();
            this.deeplLink = new System.Windows.Forms.LinkLabel();
            this.settingPanel.SuspendLayout();
            this.tabMIN.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabDeepL.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingPanel
            // 
            this.settingPanel.Controls.Add(this.tabMIN);
            this.settingPanel.Controls.Add(this.tabDeepL);
            this.settingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingPanel.Location = new System.Drawing.Point(0, 0);
            this.settingPanel.Name = "settingPanel";
            this.settingPanel.SelectedIndex = 0;
            this.settingPanel.Size = new System.Drawing.Size(592, 252);
            this.settingPanel.TabIndex = 0;
            // 
            // tabMIN
            // 
            this.tabMIN.BackColor = System.Drawing.SystemColors.Control;
            this.tabMIN.Controls.Add(this.minLink);
            this.tabMIN.Controls.Add(this.groupBox1);
            this.tabMIN.Location = new System.Drawing.Point(4, 22);
            this.tabMIN.Name = "tabMIN";
            this.tabMIN.Padding = new System.Windows.Forms.Padding(3);
            this.tabMIN.Size = new System.Drawing.Size(584, 226);
            this.tabMIN.TabIndex = 0;
            this.tabMIN.Text = "みんなの自動翻訳";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.minUSER);
            this.groupBox1.Controls.Add(this.minSECRET);
            this.groupBox1.Controls.Add(this.minKEY);
            this.groupBox1.Controls.Add(this.minURL);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(11, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本設定";
            // 
            // minUSER
            // 
            this.minUSER.BackColor = System.Drawing.SystemColors.Window;
            this.minUSER.Location = new System.Drawing.Point(98, 111);
            this.minUSER.Name = "minUSER";
            this.minUSER.Size = new System.Drawing.Size(451, 22);
            this.minUSER.TabIndex = 7;
            this.minUSER.ModifiedChanged += new System.EventHandler(this.modified_Changed);
            // 
            // minSECRET
            // 
            this.minSECRET.Location = new System.Drawing.Point(98, 83);
            this.minSECRET.Name = "minSECRET";
            this.minSECRET.Size = new System.Drawing.Size(451, 22);
            this.minSECRET.TabIndex = 6;
            this.minSECRET.ModifiedChanged += new System.EventHandler(this.modified_Changed);
            // 
            // minKEY
            // 
            this.minKEY.Location = new System.Drawing.Point(98, 55);
            this.minKEY.Name = "minKEY";
            this.minKEY.Size = new System.Drawing.Size(451, 22);
            this.minKEY.TabIndex = 5;
            this.minKEY.ModifiedChanged += new System.EventHandler(this.modified_Changed);
            // 
            // minURL
            // 
            this.minURL.Location = new System.Drawing.Point(98, 27);
            this.minURL.Name = "minURL";
            this.minURL.Size = new System.Drawing.Size(451, 22);
            this.minURL.TabIndex = 4;
            this.minURL.ModifiedChanged += new System.EventHandler(this.modified_Changed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "ログインID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "API Secret";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "API KEY";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "API URL";
            // 
            // tabDeepL
            // 
            this.tabDeepL.BackColor = System.Drawing.SystemColors.Control;
            this.tabDeepL.Controls.Add(this.deeplLink);
            this.tabDeepL.Controls.Add(this.groupBox2);
            this.tabDeepL.Location = new System.Drawing.Point(4, 22);
            this.tabDeepL.Name = "tabDeepL";
            this.tabDeepL.Padding = new System.Windows.Forms.Padding(3);
            this.tabDeepL.Size = new System.Drawing.Size(584, 226);
            this.tabDeepL.TabIndex = 1;
            this.tabDeepL.Text = "DeepL";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deepKEY);
            this.groupBox2.Controls.Add(this.deepURL);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox2.Location = new System.Drawing.Point(11, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(568, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "基本設定";
            // 
            // deepKEY
            // 
            this.deepKEY.Location = new System.Drawing.Point(98, 55);
            this.deepKEY.Name = "deepKEY";
            this.deepKEY.Size = new System.Drawing.Size(451, 22);
            this.deepKEY.TabIndex = 5;
            this.deepKEY.ModifiedChanged += new System.EventHandler(this.modified_Changed);
            // 
            // deepURL
            // 
            this.deepURL.Location = new System.Drawing.Point(98, 27);
            this.deepURL.Name = "deepURL";
            this.deepURL.Size = new System.Drawing.Size(451, 22);
            this.deepURL.TabIndex = 4;
            this.deepURL.ModifiedChanged += new System.EventHandler(this.modified_Changed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "認証キー";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "API URL";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbMsgReplace);
            this.panel1.Controls.Add(this.cancel);
            this.panel1.Controls.Add(this.ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 205);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 47);
            this.panel1.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 11.25F);
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "表現";
            // 
            // cmbMsgReplace
            // 
            this.cmbMsgReplace.AutoCompleteCustomSource.AddRange(new string[] {
            "チャット文字に追記する",
            "チャット文字を置き換える"});
            this.cmbMsgReplace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMsgReplace.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbMsgReplace.FormattingEnabled = true;
            this.cmbMsgReplace.Items.AddRange(new object[] {
            "チャット文字に追記する",
            "チャット文字を置き換える"});
            this.cmbMsgReplace.Location = new System.Drawing.Point(55, 12);
            this.cmbMsgReplace.Name = "cmbMsgReplace";
            this.cmbMsgReplace.Size = new System.Drawing.Size(193, 23);
            this.cmbMsgReplace.TabIndex = 2;
            // 
            // cancel
            // 
            this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cancel.Location = new System.Drawing.Point(481, 6);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(104, 34);
            this.cancel.TabIndex = 1;
            this.cancel.Text = "キャンセル(&C)";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // ok
            // 
            this.ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ok.Location = new System.Drawing.Point(360, 6);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(104, 34);
            this.ok.TabIndex = 0;
            this.ok.Text = "保存(&S)";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // minLink
            // 
            this.minLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minLink.AutoSize = true;
            this.minLink.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.minLink.Location = new System.Drawing.Point(443, 10);
            this.minLink.Name = "minLink";
            this.minLink.Size = new System.Drawing.Size(133, 13);
            this.minLink.TabIndex = 8;
            this.minLink.TabStop = true;
            this.minLink.Text = "みんなの自動翻訳サイト";
            this.minLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.minLink_LinkClicked);
            // 
            // deeplLink
            // 
            this.deeplLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deeplLink.AutoSize = true;
            this.deeplLink.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.deeplLink.Location = new System.Drawing.Point(447, 10);
            this.deeplLink.Name = "deeplLink";
            this.deeplLink.Size = new System.Drawing.Size(129, 13);
            this.deeplLink.TabIndex = 9;
            this.deeplLink.TabStop = true;
            this.deeplLink.Text = "DeepL翻訳ツールサイト";
            this.deeplLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 252);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.settingPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Setting_FormClosing);
            this.Load += new System.EventHandler(this.Setting_Load);
            this.settingPanel.ResumeLayout(false);
            this.tabMIN.ResumeLayout(false);
            this.tabMIN.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabDeepL.ResumeLayout(false);
            this.tabDeepL.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingPanel;
        private System.Windows.Forms.TabPage tabMIN;
        private System.Windows.Forms.TabPage tabDeepL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox minKEY;
        private System.Windows.Forms.TextBox minURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox minUSER;
        private System.Windows.Forms.TextBox minSECRET;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox deepKEY;
        private System.Windows.Forms.TextBox deepURL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbMsgReplace;
        private System.Windows.Forms.LinkLabel minLink;
        private System.Windows.Forms.LinkLabel deeplLink;
    }
}