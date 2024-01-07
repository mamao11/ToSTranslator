using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToSTranslator
{
    public partial class Setting : Form
    {
        private bool _editing = false;
        private static Properties.Settings MySettings = Properties.Settings.Default;

        public Setting()
        {
            InitializeComponent();
        }

        private void LoadSetting()
        {
            MySettings.Reload();
            minURL.Text = MySettings.MIN_API_URL;
            minKEY.Text = MySettings.MIN_API_KEY;
            minSECRET.Text = MySettings.MIN_API_SECRET;
            minUSER.Text = MySettings.MIN_API_USER;

            deepURL.Text = MySettings.DeepL_API_URL;
            deepKEY.Text = MySettings.DeepL_API_KEY;

            if(MySettings.RENDER_STYLE == "REPLACE")
            {
                cmbMsgReplace.SelectedIndex = 1;
                GlobalV.renderStyle = GlobalV.RenderStyle.REPLACE;
            }
            else
            {
                cmbMsgReplace.SelectedIndex = 0;
                GlobalV.renderStyle = GlobalV.RenderStyle.APPEND;
            }

            cbExitConfirm.Checked = MySettings.EXIT_CONFIRM;

            _editing = false;

        }
        private void SaveSetting()
        {
            MySettings.MIN_API_URL = minURL.Text;
            MySettings.MIN_API_KEY = minKEY.Text;
            MySettings.MIN_API_SECRET = minSECRET.Text;
            MySettings.MIN_API_USER = minUSER.Text;

            MySettings.DeepL_API_URL = deepURL.Text;
            MySettings.DeepL_API_KEY = deepKEY.Text;

            if (cmbMsgReplace.SelectedIndex == 0)
            {
                MySettings.RENDER_STYLE = "APPEND";
                GlobalV.renderStyle = GlobalV.RenderStyle.APPEND;
            }
            else
            {
                MySettings.RENDER_STYLE = "REPLACE";
                GlobalV.renderStyle = GlobalV.RenderStyle.REPLACE;
            }

            MySettings.EXIT_CONFIRM = cbExitConfirm.Checked;

            MySettings.Save();
            _editing = false;
        }

        private void Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_editing)
            {
                if(DialogResult.Yes == MessageBox.Show("保存されていない変更があります。\n保存しますか？", "ToS 翻訳", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SaveSetting();
                }
            }
        }
        private void Setting_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            SaveSetting();
            Close();
        }

        private void modified_Changed(object sender, EventArgs e)
        {
            _editing = true;
            Control c = (Control)sender;
            c.BackColor = Color.Thistle;
        }

        private void minLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(MySettings.MIN_SITE_URL);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(MySettings.DeepL_SITE_URL);
        }
        //URLをブラウザで開く
        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}
