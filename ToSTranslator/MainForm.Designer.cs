
namespace ToSTranslator
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.dics = new System.Windows.Forms.Label();
            this.api = new System.Windows.Forms.ComboBox();
            this.log = new System.Windows.Forms.TreeView();
            this.msg = new System.Windows.Forms.ListBox();
            this.deliverStatus = new System.Windows.Forms.Label();
            this.translateStatus = new System.Windows.Forms.Label();
            this.recieverStatus = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.Button();
            this.lnkDicClear = new System.Windows.Forms.LinkLabel();
            this.minCaution = new System.Windows.Forms.Panel();
            this.btnAgree = new System.Windows.Forms.Button();
            this.btnDisagree = new System.Windows.Forms.Button();
            this.minLicense = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usingCaution = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.deeplLink = new System.Windows.Forms.LinkLabel();
            this.minLink = new System.Windows.Forms.LinkLabel();
            this.usingAgree = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.minPowerdBy = new System.Windows.Forms.Label();
            this.showCaution = new System.Windows.Forms.PictureBox();
            this.minCaution.SuspendLayout();
            this.panel2.SuspendLayout();
            this.usingCaution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showCaution)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(42, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "翻訳API";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(672, 461);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(99, 41);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "終了(&X)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button1_Click);
            // 
            // dics
            // 
            this.dics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dics.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dics.Location = new System.Drawing.Point(669, 6);
            this.dics.Name = "dics";
            this.dics.Size = new System.Drawing.Size(102, 21);
            this.dics.TabIndex = 4;
            this.dics.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // api
            // 
            this.api.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.api.FormattingEnabled = true;
            this.api.Items.AddRange(new object[] {
            "(未設定)",
            "みんなの自動翻訳",
            "DeepL"});
            this.api.Location = new System.Drawing.Point(105, 4);
            this.api.Name = "api";
            this.api.Size = new System.Drawing.Size(121, 20);
            this.api.TabIndex = 5;
            this.api.SelectedIndexChanged += new System.EventHandler(this.api_SelectedIndexChanged);
            // 
            // log
            // 
            this.log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.log.FullRowSelect = true;
            this.log.Indent = 5;
            this.log.Location = new System.Drawing.Point(12, 30);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(760, 374);
            this.log.TabIndex = 7;
            this.log.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.log_BeforeCollapse);
            this.log.KeyDown += new System.Windows.Forms.KeyEventHandler(this.log_KeyDown);
            // 
            // msg
            // 
            this.msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msg.BackColor = System.Drawing.Color.Silver;
            this.msg.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.msg.ForeColor = System.Drawing.Color.Red;
            this.msg.FormattingEnabled = true;
            this.msg.ItemHeight = 15;
            this.msg.Location = new System.Drawing.Point(12, 440);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(654, 64);
            this.msg.TabIndex = 8;
            this.msg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.msg_KeyDown);
            // 
            // deliverStatus
            // 
            this.deliverStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deliverStatus.BackColor = System.Drawing.Color.DimGray;
            this.deliverStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deliverStatus.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.deliverStatus.ForeColor = System.Drawing.Color.White;
            this.deliverStatus.Location = new System.Drawing.Point(191, 410);
            this.deliverStatus.Name = "deliverStatus";
            this.deliverStatus.Size = new System.Drawing.Size(82, 24);
            this.deliverStatus.TabIndex = 9;
            this.deliverStatus.Text = "配信";
            this.deliverStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // translateStatus
            // 
            this.translateStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.translateStatus.BackColor = System.Drawing.Color.DimGray;
            this.translateStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.translateStatus.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.translateStatus.ForeColor = System.Drawing.Color.White;
            this.translateStatus.Location = new System.Drawing.Point(103, 410);
            this.translateStatus.Name = "translateStatus";
            this.translateStatus.Size = new System.Drawing.Size(82, 24);
            this.translateStatus.TabIndex = 10;
            this.translateStatus.Text = "翻訳";
            this.translateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // recieverStatus
            // 
            this.recieverStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recieverStatus.BackColor = System.Drawing.Color.DimGray;
            this.recieverStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.recieverStatus.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.recieverStatus.ForeColor = System.Drawing.Color.White;
            this.recieverStatus.Location = new System.Drawing.Point(15, 410);
            this.recieverStatus.Name = "recieverStatus";
            this.recieverStatus.Size = new System.Drawing.Size(82, 24);
            this.recieverStatus.TabIndex = 11;
            this.recieverStatus.Text = "受信";
            this.recieverStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Location = new System.Drawing.Point(672, 410);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(100, 24);
            this.btnSetting.TabIndex = 12;
            this.btnSetting.Text = "設定(&S)";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // lnkDicClear
            // 
            this.lnkDicClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkDicClear.AutoSize = true;
            this.lnkDicClear.Location = new System.Drawing.Point(598, 11);
            this.lnkDicClear.Name = "lnkDicClear";
            this.lnkDicClear.Size = new System.Drawing.Size(65, 12);
            this.lnkDicClear.TabIndex = 13;
            this.lnkDicClear.TabStop = true;
            this.lnkDicClear.Text = "辞書初期化";
            this.lnkDicClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDicClear_LinkClicked);
            // 
            // minCaution
            // 
            this.minCaution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minCaution.Controls.Add(this.btnAgree);
            this.minCaution.Controls.Add(this.btnDisagree);
            this.minCaution.Controls.Add(this.minLicense);
            this.minCaution.Controls.Add(this.panel2);
            this.minCaution.Location = new System.Drawing.Point(45, 55);
            this.minCaution.Name = "minCaution";
            this.minCaution.Size = new System.Drawing.Size(421, 277);
            this.minCaution.TabIndex = 16;
            this.minCaution.Visible = false;
            // 
            // btnAgree
            // 
            this.btnAgree.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAgree.BackColor = System.Drawing.Color.Thistle;
            this.btnAgree.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAgree.Location = new System.Drawing.Point(225, 233);
            this.btnAgree.Name = "btnAgree";
            this.btnAgree.Size = new System.Drawing.Size(113, 41);
            this.btnAgree.TabIndex = 20;
            this.btnAgree.Text = "わかりました";
            this.btnAgree.UseVisualStyleBackColor = false;
            this.btnAgree.Click += new System.EventHandler(this.btnAgree_Click);
            // 
            // btnDisagree
            // 
            this.btnDisagree.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDisagree.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnDisagree.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDisagree.Location = new System.Drawing.Point(61, 233);
            this.btnDisagree.Name = "btnDisagree";
            this.btnDisagree.Size = new System.Drawing.Size(113, 41);
            this.btnDisagree.TabIndex = 19;
            this.btnDisagree.Text = "やめておく";
            this.btnDisagree.UseVisualStyleBackColor = false;
            this.btnDisagree.Click += new System.EventHandler(this.btnDisagree_Click);
            // 
            // minLicense
            // 
            this.minLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minLicense.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.minLicense.Location = new System.Drawing.Point(0, 106);
            this.minLicense.Name = "minLicense";
            this.minLicense.ReadOnly = true;
            this.minLicense.Size = new System.Drawing.Size(421, 120);
            this.minLicense.TabIndex = 18;
            this.minLicense.Text = resources.GetString("minLicense.Text");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(421, 106);
            this.panel2.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(-55, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(565, 46);
            this.label3.TabIndex = 1;
            this.label3.Text = "みんなの自動翻訳をご利用になる場合は以下事項を必ず最後までご一読ください。\r\n合意いただけない場合は利用を停止してください。";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(67, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "みんなの自動翻訳　利用規約";
            // 
            // usingCaution
            // 
            this.usingCaution.Controls.Add(this.btnBack);
            this.usingCaution.Controls.Add(this.label7);
            this.usingCaution.Controls.Add(this.deeplLink);
            this.usingCaution.Controls.Add(this.minLink);
            this.usingCaution.Controls.Add(this.usingAgree);
            this.usingCaution.Controls.Add(this.label6);
            this.usingCaution.Controls.Add(this.label5);
            this.usingCaution.Controls.Add(this.label4);
            this.usingCaution.Location = new System.Drawing.Point(498, 55);
            this.usingCaution.Name = "usingCaution";
            this.usingCaution.Size = new System.Drawing.Size(380, 277);
            this.usingCaution.TabIndex = 17;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBack.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnBack.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnBack.Location = new System.Drawing.Point(41, 233);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(113, 41);
            this.btnBack.TabIndex = 21;
            this.btnBack.Text = "もどる";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(-176, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 31);
            this.label7.TabIndex = 24;
            this.label7.Text = "はじめに";
            // 
            // deeplLink
            // 
            this.deeplLink.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deeplLink.AutoSize = true;
            this.deeplLink.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.deeplLink.Location = new System.Drawing.Point(-110, 180);
            this.deeplLink.Name = "deeplLink";
            this.deeplLink.Size = new System.Drawing.Size(235, 24);
            this.deeplLink.TabIndex = 23;
            this.deeplLink.TabStop = true;
            this.deeplLink.Text = "DeepL翻訳ツールサイトを開く";
            this.deeplLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.deeplLink_LinkClicked);
            // 
            // minLink
            // 
            this.minLink.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.minLink.AutoSize = true;
            this.minLink.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.minLink.Location = new System.Drawing.Point(-110, 137);
            this.minLink.Name = "minLink";
            this.minLink.Size = new System.Drawing.Size(234, 24);
            this.minLink.TabIndex = 22;
            this.minLink.TabStop = true;
            this.minLink.Text = "みんなの自動翻訳サイトを開く";
            this.minLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.minLink_LinkClicked);
            // 
            // usingAgree
            // 
            this.usingAgree.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.usingAgree.BackColor = System.Drawing.Color.Thistle;
            this.usingAgree.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.usingAgree.Location = new System.Drawing.Point(205, 233);
            this.usingAgree.Name = "usingAgree";
            this.usingAgree.Size = new System.Drawing.Size(113, 41);
            this.usingAgree.TabIndex = 25;
            this.usingAgree.Text = "わかりました";
            this.usingAgree.UseVisualStyleBackColor = false;
            this.usingAgree.Click += new System.EventHandler(this.usingAgree_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(-176, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 31);
            this.label6.TabIndex = 4;
            this.label6.Text = "注意事項！";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(-150, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(670, 161);
            this.label5.TabIndex = 3;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(-144, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(610, 69);
            this.label4.TabIndex = 2;
            this.label4.Text = "本アプリケーションを利用するには翻訳APIの利用登録が必要です。\r\nまず以下サービスのどちらか一方、もしくは両方のサービスへの登録を行ってください。\r\nその後、本" +
    "アプリケーションの設定画面にてサービスの登録情報を入力してください。";
            // 
            // minPowerdBy
            // 
            this.minPowerdBy.AutoSize = true;
            this.minPowerdBy.Location = new System.Drawing.Point(232, 8);
            this.minPowerdBy.Name = "minPowerdBy";
            this.minPowerdBy.Size = new System.Drawing.Size(94, 12);
            this.minPowerdBy.TabIndex = 19;
            this.minPowerdBy.Text = "Powered by NICT";
            this.minPowerdBy.Visible = false;
            // 
            // showCaution
            // 
            this.showCaution.Image = global::ToSTranslator.Properties.Resources.info5;
            this.showCaution.InitialImage = null;
            this.showCaution.Location = new System.Drawing.Point(12, 3);
            this.showCaution.Name = "showCaution";
            this.showCaution.Size = new System.Drawing.Size(24, 24);
            this.showCaution.TabIndex = 20;
            this.showCaution.TabStop = false;
            this.showCaution.Click += new System.EventHandler(this.showCaution_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.showCaution);
            this.Controls.Add(this.minPowerdBy);
            this.Controls.Add(this.usingCaution);
            this.Controls.Add(this.minCaution);
            this.Controls.Add(this.lnkDicClear);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.recieverStatus);
            this.Controls.Add(this.translateStatus);
            this.Controls.Add(this.deliverStatus);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.log);
            this.Controls.Add(this.api);
            this.Controls.Add(this.dics);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(750, 500);
            this.Name = "MainForm";
            this.Text = "ToS 翻訳";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.minCaution.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.usingCaution.ResumeLayout(false);
            this.usingCaution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showCaution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label dics;
        private System.Windows.Forms.ComboBox api;
        private System.Windows.Forms.TreeView log;
        private System.Windows.Forms.ListBox msg;
        private System.Windows.Forms.Label deliverStatus;
        private System.Windows.Forms.Label translateStatus;
        private System.Windows.Forms.Label recieverStatus;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.LinkLabel lnkDicClear;
        private System.Windows.Forms.Panel minCaution;
        private System.Windows.Forms.Button btnAgree;
        private System.Windows.Forms.Button btnDisagree;
        private System.Windows.Forms.RichTextBox minLicense;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel usingCaution;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel minLink;
        private System.Windows.Forms.Button usingAgree;
        private System.Windows.Forms.LinkLabel deeplLink;
        private System.Windows.Forms.Label minPowerdBy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox showCaution;
        private System.Windows.Forms.Button btnBack;
    }
}

