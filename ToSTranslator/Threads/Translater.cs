using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace ToSTranslator
{
    public class Translater : ThreadBase
    {
        DeepL dp_api = new DeepL();

        //翻訳辞書（これまでの翻訳を溜め込んでおく）
        private ConcurrentDictionary<string, string> dic = new ConcurrentDictionary<string, string>();
        //システムメッセージパターンおよび数値無変換指定
        private static List<KeyValuePair<string, string>> sys_pat = new List<KeyValuePair<string, string>>();

        //翻訳辞書件数
        public int DictionaryCount { get { return dic.Count; } }

        //メッセージを分解しパーツごとに翻訳要否を判断するために使う
        public class MessageParts
        {
            public int index;
            public string msg;
            public bool pickup;
            public MessageParts(int p0, string p1, bool p2)
            {
                this.index = p0;
                this.msg = p1;
                this.pickup = p2;
            }
        };


        public Translater(Form form) : base(form)
        {
            //保存済み辞書をロード
            LoadDictionary();
            //文字省略パターンをロード
            LoadSplitPattern();
        }

        public override void Dispose()
        {
            base.Dispose();
            SaveDictionary();
        }

        #region 翻訳履歴
        //翻訳履歴読み込み
        private void LoadDictionary()
        {
            string path = Application.ExecutablePath.Replace(".exe", "_dic.txt");

            //翻訳履歴ファイルがなかったら何もしない
            if (!File.Exists(path)) return;

            try
            {
                //ロード
                Encoding encode = new UTF8Encoding(false);
                var lines = File.ReadAllLines(path, encode);
                foreach (var line in lines)
                {
                    // keyとvalueの区切り目を見つける
                    int pos = line.IndexOf("[@=@]");
                    if (pos < 0)
                    {
                        continue;
                    }

                    // key-valueを一覧に追加
                    string key = line.Substring(0, pos);
                    string val = line.Substring(pos + 5).Replace("\t","\n");    //タブ文字は改行に戻して取り込む
                    if (val.Length == 0) val = " ";
                    dic[key] = val;
                }

            }
            catch (Exception)
            {
                //ロード失敗は何もしない
            }
        }
        
        //変換履歴書き込み
        private void SaveDictionary()
        {
            string path = Application.ExecutablePath.Replace(".exe", "_dic.txt");

            Encoding encode = new UTF8Encoding(false);
            using (StreamWriter writer = new StreamWriter(path, false, encode))
            {

                foreach (string key in dic.Keys)
                {
                    // keyに"="が入っていたら例外を投げる
                    if (key.IndexOf("[@=@]") > 0)
                    {
                        throw new ArgumentException(@"invalid key[{key}]");
                    }

                    var value = dic[key];
                    writer.WriteLine(key + "[@=@]" + value.Replace("\n","\t")); //改行はタブ文字に置き換えて保存
                }
            }
        }
        public bool ClearDictionary()
        {
            bool ret = true;
            try
            {
                string path = Application.ExecutablePath.Replace(".exe", "_dic.txt");
                File.Delete(path);
                dic.Clear();
                PushCount(dic.Count);
            }
            catch (Exception ex)
            {
                PushMessage("辞書初期化に失敗：" + ex.Message, MessageType.WARN);
                ret = false;
            }
            return ret;
        }
        #endregion


        //翻訳スレッド処理
        public override void Start()
        {
            if (GlobalV.sourceQueue == null)
            {
                throw new Exception("翻訳キューが作成されていません");
            }
            if (GlobalV.deliverQueue == null)
            {
                throw new Exception("配信キューが作成されていません");
            }

            PushMessage("<---- 翻訳スレッド起動 ---->", MessageType.INFO);
            if (GlobalV.API == GlobalV.TranslateAPI.None)
            {
                PushStatus(StatusType.TranslateOff);
                PushMessage("翻訳スレッドより通知：翻訳APIの指定が無いため翻訳しません", MessageType.WARN);
            }
            else
            {
                PushStatus(StatusType.TranslateConnect);
            }

            //翻訳スレッド
            while (!_exit)
            {
                GlobalV.TranslateItem item = null;
                //翻訳キューから取り出し
                if (GlobalV.sourceQueue.TryDequeue(out item))
                {
                    _logger.Debug("翻訳処理:{0} :{1}", item.chat_id, item.source_text);

                    try
                    {
                        if (GlobalV.API == GlobalV.TranslateAPI.None)
                        {
                            //翻訳APIなしなら変換なしで終わり
                            item.translated_name = item.source_name;
                            item.translated_text = item.source_text;
                            _logger.Debug("翻訳APIなし");
                        }
                        else
                        {
                            //翻訳詳細を実行
                            item = TranslateProc(item);
                            _logger.Debug("翻訳完了:{0} {1}", item.chat_id, item.translated_text);
                        }

                        //変化がない場合は破棄
                        if (item.source_name == item.translated_name && item.source_text == item.translated_text)
                        {
                            //配信完了扱いにする
                            PushTranslateEvent(item, EventType.TranslateEnd);
                        }
                        else
                        {
                            //変換後は配信キューへ
                            GlobalV.deliverQueue.Enqueue(item);
                            //キュー追加完了
                            _logger.Debug("配信キューへ:{0} {1}", item.chat_id, item.translated_text);

                            //翻訳完了をフォームへ
                            PushTranslateEvent(item, EventType.TranslateEnd);
                        }
                    }
                    catch (Exception ex)
                    {
                        PushMessage("-- 翻訳スレッドERROR -- : " + ex.Message, MessageType.WARN);
                        _logger.Debug("翻訳スレッドERROR " + ex.Message);
                    }
                }
                //スレッド内は0.5秒おきに処理
                Thread.Sleep(500);
                //_logger.Debug("翻訳スレッド稼働中");
            }
            PushStatus(StatusType.TranslateOff);
        }

        //翻訳スレット終了処理
        public override void Exit()
        {
            //フラグ設定のみ。戻った後にスレッドオブジェクトの終了を待つ
            _exit = true;
        }

        //細かい翻訳プロセス
        private GlobalV.TranslateItem TranslateProc(GlobalV.TranslateItem item)
        {
            //元文字取得
            string src = item.source_text;

            //メッセージ分解用
            List<MessageParts> mpt = null;  //分解後（全体）
            List<MessageParts> trans = null;    //分解後（対象のみ）
            List<string> msga = null;   //翻訳対象文字配列
            bool with_name = false; //名前も翻訳対象フラグ

            //チャットへの表現方法モードにより翻訳処理を切り替える
            if (GlobalV.renderStyle == GlobalV.RenderStyle.REPLACE)
            {
                //REPLACEモード

                //メッセージをパーツに分解して必要な部分のみ翻訳する
                _logger.Debug(item.chat_id + ":");
                mpt = SplitMessage(src);

                //翻訳対象パーツのみを抽出
                trans = mpt.FindAll(mp => mp.pickup == true);
                _logger.Debug(item.chat_id + ":pickup array" + trans.Count.ToString());

                //メッセージ値だけを抽出
                msga = trans.Select(mp => mp.msg).ToList();
                _logger.Debug(item.chat_id + ":msg array " + msga.Count.ToString());
            }
            else
            {
                //APPENDモード

                //ガチャ等メッセージを省き、チャット内容は必要な部分のみ翻訳
                _logger.Debug(item.chat_id + ":adjust");
                msga = AdjustMessage(src);
            }

            //キャラ名にハングルが入っているなら翻訳対象に追加
            if (WithHangle(item.source_name))
            {
                msga.Insert(0, item.source_name);
                with_name = true;
                _logger.Debug(item.chat_id + ":with name");
            }

            //最終文字列を保存用ハッシュ値に変換
            src = string.Join("\n", msga.ToArray());
            string hash = GetHash(src);

            //翻訳開始をフォームへ通知
            PushTranslateEvent(item, EventType.TranslateStart);

            List<string> rslt = null;

            //辞書確認
            bool on_dic = dic.ContainsKey(hash);
            if (on_dic)
            {
                //既存なので辞書から取得
                rslt = new List<string>(dic[hash].Split('\n'));
                item.on_dic = true;
                _logger.Debug("辞書あり:{0} {1}", item.chat_id, src);
            }

            //辞書に無いか辞書内の配列数が異なるなら翻訳
            if (!on_dic || (on_dic && rslt.Count != msga.Count))
            {
                if (msga.Count == 0)
                {
                    //翻訳対象なし
                    _logger.Debug(item.chat_id + ":zero");
                    rslt = new List<string>();
                }
                else
                {
                    //もし辞書異常なら一旦消す
                    string tmp;
                    if (on_dic) dic.TryRemove(hash, out tmp);

                    //翻訳実行
                    _logger.Debug("翻訳開始:{0} {1}", item.chat_id, string.Join("/", msga));
                    rslt = Translate(msga.ToArray());
                }
            }

            //翻訳要求あるのに結果なしか、翻訳前後で配列数が違ったら何かおかしい
            if ((rslt.Count == 0 && msga.Count > 0) || rslt.Count != msga.Count)
            {
                _logger.Debug("Transration fail...");
                item.translated_name = "";
                item.translated_text = "";
            }
            else
            {
                //名前翻訳ありなら取り出しておく
                if (with_name)
                {
                    item.translated_name = rslt[0];
                    rslt.RemoveAt(0);
                    _logger.Debug(item.chat_id + ":after pop name count " + rslt.Count.ToString());
                }
                else
                {
                    //キャラ名変換不要ならそのまま返す
                    item.translated_name = item.source_name;
                }

                item.translated_text = "";
                //モードごとに返送文字を生成
                if (GlobalV.renderStyle == GlobalV.RenderStyle.REPLACE)
                {
                    //REPLACEモード

                    _logger.Debug(item.chat_id + ":rslt");
                    //メッセージを翻訳後に置き換える（transの件数=msgaの件数=rsltの件数。になっているハズ）
                    for (int i = 0; i < trans.Count; i++)
                    {

                        //置き換え先のインデックスを取り、
                        int j = trans[i].index;

                        _logger.Debug(item.chat_id + ":rep " + j.ToString());
                        //置き換え
                        if (j >= 0 && j < mpt.Count) mpt[j].msg = rslt[i];
                    }
                    //置き換えたらすべての文字を結合
                    item.translated_text = string.Join("", mpt.Select(mp => mp.msg));

                    _logger.Debug(item.chat_id + ":→ " + string.Join("", mpt.Select(mp => mp.msg)));
                }
                else
                {
                    //APPENDモード

                    //複数返ってくるのでブランクで繋げる
                    item.translated_text = string.Join("　", rslt);
                }
                //item.translated_text = item.source_text;    //実験的にソースそのまま返す


                //最終判断
                if (item.translated_text.Length == 0 && !with_name)
                {
                    //名前変換がなく文字列も空なら翻訳失敗
                    item.translated_text = "翻訳失敗";
                }
                else
                {
                    //翻訳成功していたら辞書へ（名前はカット済みなので戻す）
                    dic.TryAdd(hash, ((with_name)? item.translated_name+"\n": "") + string.Join("\n", rslt));
                    PushCount(dic.Count);
                }
            }

            return item;
        }


        #region 翻訳文字列調整

        private void LoadSplitPattern()
        {
            string path = Application.ExecutablePath.Replace(".exe", "_msg.pat");

            //翻訳履歴ファイルがなかったら何もしない（望ましくないけど）
            if (!File.Exists(path)) return;

            try
            {
                //ロード
                Encoding encode = new UTF8Encoding(false);
                var lines = File.ReadAllLines(path, encode);
                foreach (var line in lines)
                {
                    //コメントは読み飛ばし
                    if (line.StartsWith("#")) continue;

                    // keyとvalueの区切り目を見つける
                    var kv = line.Split('\t');
                    if (kv.Length < 2)
                    {
                        continue;
                    }

                    // key-valueを一覧に追加
                    var key = kv[0];
                    var val = kv[1];
                    if (val.Length == 0) val = " ";
                    sys_pat.Add(new KeyValuePair<string, string>(key, val));
                }
            }
            catch (Exception)
            {
                //ロード失敗は何もしない
            }
        }

        private List<MessageParts> SplitMessage(string msg)
        {
            List<MessageParts> mpt = new List<MessageParts>();
            //まず分解パターンを特定
            string pat = "";
            string pick = "";
            foreach (KeyValuePair<string, string> kv in sys_pat)
            {
                if (Regex.IsMatch(msg, kv.Key))
                {
                    pat = kv.Key;
                    pick = kv.Value;
                    _logger.Debug("msg pattern match. " + kv.Key);
                    //ret = Regex.Replace(ret, p.Key, p.Value);
                    break;
                }
                else
                {
                    _logger.Debug("msg pattern not found.");
                }
            }

            //パターンを見つけたら分解開始
            if (pat != "" && pick != "")
            {
                //ピックアップ対象定義を配列へ（$2 $4 → [0]=2,[1]=4）
                List<int> indexes = new List<int>();
                MatchCollection ms = Regex.Matches(pick, @"\$\d{1,3}");
                foreach (Match m1 in ms)
                {
                    int vi;
                    if (int.TryParse(m1.Value.Replace("$", ""), out vi))
                    {
                        indexes.Add(vi);
                    }
                }
                _logger.Debug("indexes: " + string.Join("/", indexes.ToArray()));

                //メッセージを正規表現でパーツに分割
                Match m2 = Regex.Match(msg, pat);
                if (m2.Success)
                {
                    int i = 0;
                    foreach (Group g in m2.Groups)
                    {
                        if (i > 0)  //index 0 は全文なので利用しない
                        {
                            //ピックアップ対象かつハングルなら翻訳対象
                            bool flg = false;
                            if (indexes.Contains(i) && WithHangle(g.Value)) flg = true;

                            //パーツno、パーツ文字列、ピックアップ対象フラグ
                            //indexは1から始まっているので-1して0からにする
                            mpt.Add(new MessageParts(i-1, g.Value, flg));
                            //_logger.Debug(i + ":" + g.Value);
                        }
                        i++;
                    }
                }
            }

            //パターンに合致しない場合は汎用分解
            if (mpt.Count == 0)
            {
                mpt = WordPickup(msg);
            }

            //分解できたかな？
            _logger.Debug("MessageParts");
            foreach (MessageParts kv in mpt)
            {
                _logger.Debug(kv.index + ":" + kv.pickup + ":" + kv.msg);
            }

            /*
            string[] wa = mpt.Select(kv => kv.Value).ToArray();
            string v = string.Join("\n", wa);
            _logger.Debug(v);
            */

            return mpt;
        }


        //不要なメッセージをカットして短縮
        private List<string> AdjustMessage(string src)
        {
            //システムメッセージ系は翻訳が必要な部分に絞る
            List<string> msga = new List<string>();
            foreach (KeyValuePair<string, string> p in sys_pat)
            {
                if (Regex.IsMatch(src, p.Key))
                {
                    //パターンマッチ
                    _logger.Debug("Adjust match." + p.Key);
                    msga.Add(Regex.Replace(src, p.Key, p.Value));
                }
            }

            //パターンに一致するものがなかったら汎用分解
            if(msga.Count==0)
            {
                List<MessageParts> mpt = WordPickup(src);
                List<MessageParts> trans = mpt.FindAll(mp => mp.pickup == true);
                msga = trans.Select(mp => mp.msg).ToList();
            }

            return msga;
        }

        //文字列から翻訳対象をピックアップする（汎用分解）
        private List<MessageParts> WordPickup(string src)
        {
            List<MessageParts> mpt = new List<MessageParts>();

            string pat = @"(\{.*?\})|(@dicID_\^\*\$.*?\$\^)";
            string[] m3 = Regex.Split(src, pat);

            bool pre_pt_tag = false;
            for (int i = 0, j = 0; i < m3.Length; i++)
            {
                if (m3[i] == "") continue;  //空っぽなら無視

                bool trans_flg = WithHangle(m3[i]);
                if (pre_pt_tag) trans_flg = false;    //パーティ名は翻訳しない

                //パーツno、パーツ文字列、ピックアップ対象フラグ
                mpt.Add(new MessageParts(j++, m3[i], trans_flg));
                //_logger.Debug(i + ":" + g.Value);

                //パーティ名タグだったら次のパーツはパーティ名
                pre_pt_tag = false;
                if (m3[i].StartsWith("{img link_party")) pre_pt_tag = true;
            }
            return mpt;

            /*
            //文字を分解して文字装飾やアイコンなどの制御文字を分類
            string pat = @"(\{.*?\})|(@dicID_\^\*\$.*?\$\^)";
            string[] src_a = Regex.Split(src, pat);

            //分解したものからハングルを探して対象をリストへ
            List<KeyValuePair<int, string>> wl = new List<KeyValuePair<int, string>>();
            for (int i = 0; i < src_a.Length; i++)
            {
                if (WithHangle(src_a[i]))
                {
                    wl.Add(new KeyValuePair<int, string>(i, src_a[i]));
                }
            }
            return wl;
            */
        }
        #endregion

        //翻訳
        private List<string> Translate(string[] src)
        {
            List<string> ret = new List<string>();

            if (src.Length == 0) return ret;

            if (GlobalV.API == GlobalV.TranslateAPI.MinTranslate)
            {
                _logger.Debug("MIN");
                //みんなの自動翻訳
                string v = string.Join("\n", src);  //ワードリストを改行で結合して渡す

                //数値とカラーコードは変換しない（この特殊な括弧↓は翻訳しない指定の意味）
                v = Regex.Replace(v, @"([0-9]+)|(\{#.*?\})", "｟$0｠");

                try
                {
                    APIAccessor_ja.APIResponseBean res = new APIAccessor_ja.APIResponseBean();
                    List<APIAccessor_ja.APIResponseBean> resp = APIAccessor_ja.get_auto_trans(v, APIAccessor_ja.Language.ko, APIAccessor_ja.Language.ja, out res);
                    foreach (APIAccessor_ja.APIResponseBean r in resp)
                    {
                        if (r.code != "0")
                        {
                            PushMessage("みんなの自動翻訳Error! " + r.code + "." + r.error_message, MessageType.WARN);
                            ret.Add("翻訳失敗");
                        }
                        else
                        {
                            XmlNode n = r.result.SelectSingleNode("/result/text");
                            if (n != null)
                            {
                                string[] txs = n.InnerText.Split('\n');
                                ret.AddRange(txs);
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    _logger.Debug(ex.StackTrace);
                    PushMessage("みんなの自動翻訳Error! " + ex.Message, MessageType.WARN);
                }
            }
            else if (GlobalV.API == GlobalV.TranslateAPI.DeepL)
            {
                _logger.Debug("DeepL");
                //DeepL
                try
                {
                    ret.AddRange(dp_api.get_auto_trans(src));
                }
                catch (Exception ex)
                {
                    _logger.Debug(ex.StackTrace);
                    PushMessage("DeepL翻訳Error! " + ex.Message, MessageType.WARN);
                }
            }
            else
            {
                ret.Add("翻訳API指定なし");
                //_logger.Debug("Unknown");
            }
            return ret;
        }

        //ハッシュ値取得
        private string GetHash(string str)
        {
            // ハッシュ値を計算する
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] beforeByteArray = Encoding.UTF8.GetBytes(str);
            byte[] afterByteArray = sha256.ComputeHash(beforeByteArray);
            sha256.Clear();

            // バイト配列を16進数文字列に変換
            StringBuilder sb = new StringBuilder();
            foreach (byte b in afterByteArray)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        private bool WithHangle(string str)
        {
            byte byt = 0;   //byt
            int code = 0;   //文字コード
            var inc = 1;    //1文字のバイト数
            var i = 0;
            UTF8Encoding streamEncoding = new UTF8Encoding();

            byte[] buf = streamEncoding.GetBytes(str);
            int size = buf.Length;

            if (size == 0) return false;

            //_logger.Debug("-----str:{0}", str);

            byt = buf[i];
            while (i < size)
            {
                if ((byt & 0x80) == 0x00)
                    inc = 1;
                else if ((byt & 0xE0) == 0xC0)
                    inc = 2;
                else if ((byt & 0xF0) == 0xE0)
                    inc = 3;
                else if ((byt & 0xF8) == 0xF0)
                    inc = 4;
                else if ((byt & 0xFC) == 0xF8)
                    inc = 5;
                else if ((byt & 0xFE) == 0xFC)
                    inc = 6;
                //_logger.Debug("inc:{0}.0x{1:x2}", inc, byt);

                for (int j = 1; j <= inc; j++)
                {
                    code = code + (byt << (8 * (inc - j)));
                    //_logger.Debug("code:{0}.0x{1:x6}", j, code);
                    i = i + 1;
                    if (i >= size) break;
                    byt = buf[i];
                }
                //_logger.Debug("{0:x6}", code);

                if (code >= 0xEAB080 && code <= 0xED9EA3)
                {
                    //ハングル
                    return true;
                }
                code = 0;
            }
            return false;
        }
    }
}
