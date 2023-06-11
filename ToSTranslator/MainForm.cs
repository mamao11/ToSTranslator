using NLog;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ToSTranslator.GlobalV;

namespace ToSTranslator
{
    public partial class MainForm : Form
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        //翻訳レシーバスレッド
        TranslateReciever _rc_thread = null;
        Task _rc_task = null;
        //翻訳スレッド
        Translater _tl_thread = null;
        Task _tl_task = null;
        //配信スレッド
        TranslateDeliver _rt_thread = null;
        Task _rt_task = null;

        #region ListBoxItem用
        public class MessageItem
        {
            public string value;
            public Color ForeColor { get; set; }
            public Color BackColor { get; set; }

            //constructor
            public MessageItem(string text)
            {
                this.value = text;
            }
            public override string ToString()
            {
                return value;
            }
        }
        #endregion

        //設定
        Properties.Settings MySettings = Properties.Settings.Default;

        public MainForm()
        {
            InitializeComponent();

            //警告パネルは消し初期化
            minCaution.Visible = false;
            minCaution.Dock = DockStyle.None;
            usingCaution.Visible = false;
            usingCaution.Dock = DockStyle.None;

            //描画イベントハンドル
            msg.DrawMode = DrawMode.OwnerDrawFixed;
            msg.DrawItem += new DrawItemEventHandler(listBox_DrawItem);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //メッセージ表現モード設置
            if (MySettings.RENDER_STYLE == "REPLACE")
            {
                GlobalV.renderStyle = GlobalV.RenderStyle.REPLACE;
            }
            else
            {
                GlobalV.renderStyle = GlobalV.RenderStyle.APPEND;
            }


            if (MySettings.CAUTION == "agree")
            {
                //注意事項了承済み
                minCaution.Visible = false;
                usingCaution.Visible = false;

                //翻訳API初期設定
                if (MySettings.API_TYPE == "MIN")
                {
                    GlobalV.API = GlobalV.TranslateAPI.DeepL;
                    api.SelectedIndex = 1;
                }
                else if (MySettings.API_TYPE == "DeepL")
                {
                    GlobalV.API = GlobalV.TranslateAPI.MinTranslate;
                    api.SelectedIndex = 2;
                }
                else
                {
                    GlobalV.API = GlobalV.TranslateAPI.None;
                    api.SelectedIndex = 0;
                }

                //開始前に設定チェック
                //CheckSettings();
                //スレッド初期化
                InitThreads();
            }
            else
            {
                //警告画面表示
                api.SelectedIndex = 0;
                showMinCaution();
            }
        }
        //各スレッドの初期化
        private void InitThreads()
        {
            //翻訳レシーバスレッド作成
            if (_rc_thread == null)
            {

                _rc_thread = new TranslateReciever(this);
                _rc_thread.OnSubThreadRunning += SubThreadRunning;
                _rc_task = Task.Run(() =>
                {
                    _rc_thread.Start();
                });
            }

            //翻訳スレッド作成
            if (_tl_thread == null)
            {
                _tl_thread = new Translater(this);
                _tl_thread.OnSubThreadRunning += SubThreadRunning;
                _tl_task = Task.Run(() =>
                {
                    _tl_thread.Start();
                });
            }

            //配信スレッド作成
            if (_rt_thread == null)
            {
                _rt_thread = new TranslateDeliver(this);
                _rt_thread.OnSubThreadRunning += SubThreadRunning;
                _rt_task = Task.Run(() =>
                {
                    _rt_thread.Start();
                });
            }
        }
        //各スレッドからフォームの更新依頼受信
        public void SubThreadRunning(object sender, ThreadBase.MsgEventArgs arg)
        {
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //翻訳キューに入った
            if (arg.Type == "TranslateQueue")
            {
                _logger.Debug("TranslateQueue ok");
                TranslateItem item = arg.translateItem;
                string txt = " " + dt + " [" + item.chat_id + "]：" + item.source_name + ">" + item.source_text;
                TreeNode node = log.Nodes.Add(txt);
                node.Tag = item.id;
                //キューに入ったら青
                node.ForeColor = Color.Blue;
            }
            else
            //翻訳開始
            if (arg.Type == "TranslateStart")
            {
                _logger.Debug("TranslateStart ok");
                TranslateItem item = arg.translateItem;
                //idから対象のnodeを探し出す
                TreeNode nd = log.Nodes.OfType<TreeNode>().FirstOrDefault(node => node.Tag.Equals(item.id));
                if (nd == null)
                {
                    //対象がみつからない
                    _logger.Debug("node not found...");
                }
                else
                {
                    TreeNode ch = nd.Nodes.Add("⇒");
                    nd.Expand();
                    //翻訳はじめたら緑
                    nd.ForeColor = Color.Green;
                    ch.ForeColor = Color.Green;
                }
            }
            else
            //翻訳結果を返した
            if (arg.Type == "TranslateEnd")
            {
                _logger.Debug("TranslateEnd ok");
                TranslateItem item = arg.translateItem;

                string mark = (item.on_dic)? "＃":"⇒";

                string txt = dt + " [" + item.chat_id + "]" + mark + item.translated_name + ">" + item.translated_text;
                //idから対象のnodeを探し出す
                TreeNode nd = log.Nodes.OfType<TreeNode>().FirstOrDefault(node => node.Tag.Equals(item.id));
                if (nd == null)
                {
                    //対象がみつからない
                    _logger.Debug("node not found...");
                }
                else
                {
                    //翻訳成功したら黒に
                    nd.ForeColor = Color.Black;
                    if (nd.Nodes.Count > 0)
                    {
                        nd.Nodes[0].ForeColor = Color.Black;
                        nd.Nodes[0].Text = txt;
                    }
                }
            }
            else
            //メッセージ
            if (arg.Type == "Message")
            {
                _logger.Debug("Message ok");
                int idx = msg.Items.Add(new MessageItem(dt + " | " + arg.Message));
                MessageItem item = (MessageItem)msg.Items[idx];
                item.ForeColor = Color.Blue;
                msg.TopIndex = idx;
            }
            else
            //警告
            if (arg.Type == "Warning")
            {
                _logger.Debug("Warning ok");
                int idx = msg.Items.Add(new MessageItem(dt + " | " + arg.Message));
                MessageItem item = (MessageItem)msg.Items[idx];
                item.ForeColor = Color.DarkRed;
                msg.TopIndex = idx;
            }
            else
            //ステータス更新
            if (arg.Type == "StatusUpdate")
            {
                Color color = Color.Gray;
                _logger.Debug("status " + arg.Message);
                //状態ごとに色決定
                if (arg.Message.EndsWith("Off")) { color = Color.DimGray; }
                if (arg.Message.EndsWith("On")) { color = Color.MediumBlue; }
                if (arg.Message.EndsWith("Connect")) { color = Color.DarkGreen; }

                //状態表示箇所ごとに色付け
                if (arg.Message.StartsWith("Reciver")) { recieverStatus.BackColor = color; }
                if (arg.Message.StartsWith("Translate")) { translateStatus.BackColor = color; }
                if (arg.Message.StartsWith("Deliver")) { deliverStatus.BackColor = color; }
            }
            else
            if (arg.Type == "Dictionary")
            {
                dics.Text = "辞書数: " + arg.Message;

            }
        }

        //閉じる処理
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("終了します。よろしいですか？", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                //キャンセル
                e.Cancel = true;
                return;
            }

            try
            {
                //受信スレッド終了指示
                if (_rc_thread != null) _rc_thread.OnSubThreadRunning -= SubThreadRunning;
                if (_rc_thread != null) _rc_thread.Exit();
                if (_rc_task != null) _rc_task.Wait(3000);
            }
            catch (Exception){}

            try
            {
                //翻訳スレッド終了
                if (_tl_thread != null) _tl_thread.OnSubThreadRunning -= SubThreadRunning;
                if (_tl_thread != null) _tl_thread.Exit();
                if (_tl_task != null) _tl_task.Wait(3000);
            }
            catch (Exception) { }

            try
            {
                //配信スレッド終了
                if (_rt_thread != null) _rt_thread.OnSubThreadRunning -= SubThreadRunning;
                if (_rt_thread != null) _rt_thread.Exit();
                if (_rt_task != null) _rt_task.Wait(3000);
            }
            catch (Exception) { }
        }

        //終了ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //コピー
        private void log_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.C)
            {
                string txt = log.SelectedNode.Text;
                Clipboard.SetText(txt);
            }
        }


        //設定ボタン
        private void btnSetting_Click(object sender, EventArgs e)
        {
            Setting stg = new Setting();
            DialogResult rslt = stg.ShowDialog();
            stg.Dispose();

            //翻訳不可を再判定
            if (rslt == DialogResult.OK) MakeSettings();

            //スレッド初期化
            InitThreads();

        }

        //翻訳API選択
        private void api_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(api.SelectedIndex==1)
            {
                MySettings.API_TYPE = "MIN";
                MySettings.Save();
                minPowerdBy.Visible = true;
            }
            else if (api.SelectedIndex == 2)
            {
                MySettings.API_TYPE = "DeepL";
                MySettings.Save();
                minPowerdBy.Visible = false;
            }
            else
            {
                MySettings.API_TYPE = "";
                MySettings.Save();
                minPowerdBy.Visible = false;
            }
            //翻訳不可を再判定して定義生成
            MakeSettings();
        }

        //翻訳API指定不可判定した上で設定
        private void MakeSettings()
        {
            //みんなの自動翻訳選択
            if (api.SelectedIndex == 1)
            {
                if (MySettings.MIN_API_KEY == "" || MySettings.MIN_API_SECRET == "" || MySettings.MIN_API_USER == "")
                {
                    //設定不足
                    GlobalV.API = GlobalV.TranslateAPI.None;
                    SubThreadRunning(this, new ThreadBase.MsgEventArgs() { Message = "!!みんなの自動翻訳は設定が不完全です", Type = "Warning" });
                }
                else
                {
                    GlobalV.API = GlobalV.TranslateAPI.MinTranslate;
                }
            }
            //DeepL選択
            else if (api.SelectedIndex == 2)
            {
                if (MySettings.DeepL_API_KEY == "" || MySettings.DeepL_API_URL == "")
                {
                    //設定不足
                    GlobalV.API = GlobalV.TranslateAPI.None;
                    SubThreadRunning(this, new ThreadBase.MsgEventArgs() { Message = "!!DeepL翻訳ツールは設定が不完全です", Type = "Warning" });
                }
                else
                {
                    GlobalV.API = GlobalV.TranslateAPI.DeepL;
                }
            }
            else
            {
                //翻訳なし
                GlobalV.API = GlobalV.TranslateAPI.None;
            }

            switch (GlobalV.API)
            {
                case TranslateAPI.MinTranslate:
                    SubThreadRunning(this, new ThreadBase.MsgEventArgs() { Message = "--〔みんなの自動翻訳〕で翻訳します", Type = "Message" });
                    SubThreadRunning(this, new ThreadBase.MsgEventArgs() { Message = "TranslateConnect", Type = "StatusUpdate" });
                    break;
                case TranslateAPI.DeepL:
                    SubThreadRunning(this, new ThreadBase.MsgEventArgs() { Message = "--〔DeepL翻訳ツール〕で翻訳します", Type = "Message" });
                    SubThreadRunning(this, new ThreadBase.MsgEventArgs() { Message = "TranslateConnect", Type = "StatusUpdate" });
                    break;
                case TranslateAPI.None:
                    SubThreadRunning(this, new ThreadBase.MsgEventArgs() { Message = "--API指定なし。翻訳しません", Type = "Warning" });
                    SubThreadRunning(this, new ThreadBase.MsgEventArgs() { Message = "TranslateOff", Type = "StatusUpdate" });
                    break;
            }
        }

        //色つきでListBox描画
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox)sender;
            if (e.Index > -1)
            {
                MessageItem item = (MessageItem)list.Items[e.Index];
                e.DrawBackground();
                e.DrawFocusRectangle();
                Brush brush = new SolidBrush(item.ForeColor);
                SizeF size = e.Graphics.MeasureString(item.ToString(), e.Font);
                e.Graphics.DrawString(item.ToString(), e.Font, brush, e.Bounds.Left, e.Bounds.Top);
                brush.Dispose();
            }
        }

        //ツリーの閉じるは無効
        private void log_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        //メッセージ欄のコピー操作
        private void msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string txt = msg.SelectedItem.ToString();
                Clipboard.SetText(txt);
            }

        }

        //辞書の初期化リンク
        private void lnkDicClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (MessageBox.Show("辞書を初期化します。よろしいですか？", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _tl_thread.ClearDictionary();
            }
        }

        #region 注意事項関係

        //注意事項再表示
        private void showCaution_Click(object sender, EventArgs e)
        {
            MySettings.CAUTION = "";
            MySettings.Save();
            showMinCaution();
        }

        //みんなの自動翻訳注意事項表示
        private void showMinCaution()
        {
            //注意事項パネルは消す
            usingCaution.Dock = DockStyle.None;
            usingCaution.Visible = false;
            //みんなの利用規約表示
            minCaution.Dock = DockStyle.Fill;
            minCaution.Visible = true;
            minCaution.BringToFront();
        }

        //合意しない
        private void btnDisagree_Click(object sender, EventArgs e)
        {
            Close();
        }

        //合意する
        private void btnAgree_Click(object sender, EventArgs e)
        {
            //注意事項パネルを閉じる
            minCaution.Dock = DockStyle.None;
            minCaution.Visible = false;

            //説明パネルを開く
            showUsingCaution();
        }

        //説明および注意事項
        private void showUsingCaution()
        {
            usingCaution.Dock = DockStyle.Fill;
            usingCaution.Visible = true;
            usingCaution.BringToFront();
        }
        //説明および注意事項合意
        private void usingAgree_Click(object sender, EventArgs e)
        {
            usingCaution.Dock = DockStyle.None;
            usingCaution.Visible = false;
            //api.SelectedIndex = 0;

            //了承済み
            MySettings.CAUTION = "agree";
            MySettings.Save();

            //設定ボタンを押させる
            //btnSetting_Click(this, null);
        }
        //戻るボタン
        private void btnBack_Click(object sender, EventArgs e)
        {
            showMinCaution();
        }

        //みんなの自動翻訳サイトを開く
        private void minLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(MySettings.MIN_SITE_URL);
        }
        //DeepL翻訳ツールサイトを開く
        private void deeplLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
        #endregion

    }
}
