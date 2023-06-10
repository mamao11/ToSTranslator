using NLog;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace ToSTranslator
{
    public class ToSStream
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private bool _exit = false;
        private Stream stream;
        private UTF8Encoding streamEncoding;
        ManualResetEvent signal = new ManualResetEvent(false);  //read/writeブロックをこっちでコントロール

        //受領パラメータ用
        public class Parameters
        {
            public string chat_id = "";
            public string name = "";
            public string text = "";
            public bool exit = false;
            public GlobalV.RenderStyle render = GlobalV.RenderStyle.APPEND;
        }


        public ToSStream(Stream ioStream)
        {
            stream = ioStream;
            streamEncoding = new UTF8Encoding();
        }

        //streamのReadWriteを強制終了
        public void Exit()
        {
            _exit = true;
            signal.Set();
        }

        public Parameters Read()
        {
            int len = 0;
            Parameters p = new Parameters();
            IAsyncResult ar = null;
            signal.Reset();

            //4バイトで文字数が来る（文字列型で来る）
            byte[] lb1 = new byte[4];
            //stream.Read(lb1, 0, 4);
            ar = stream.BeginRead(lb1, 0, 4, (a) => { signal.Set(); }, null);
            signal.WaitOne();
            signal.Reset();
            _logger.Debug("read 4 bytes");
            if (!ar.IsCompleted || _exit) { p.exit = true;  return p; }
            stream.EndRead(ar);

            //相手ストリームが閉じていたら0x0が来るので強制終了
            if (lb1[0] == 0x0)
            {
                p.exit = true;
                p.text = "_end";
                return p;
            }
            //文字数を数値に変換
            string buf = Encoding.ASCII.GetString(lb1);
            len = int.Parse(buf);
            _logger.Debug("read msg length");

            //次に8バイトでCHAT_IDが来る
            byte[] lb2 = new byte[8];
            //stream.Read(lb2, 0, 8);
            ar = stream.BeginRead(lb2, 0, 8, (a) => { signal.Set(); }, null);
            signal.WaitOne();
            signal.Reset();
            if (!ar.IsCompleted || _exit) { p.exit = true; return p; }
            stream.EndRead(ar);

            //相手ストリームが閉じていたら0x0が来るので強制終了
            if (lb2[0] == 0x0)
            {
                p.exit = true;
                p.text = "_end";
                return p;
            }
            p.chat_id = Encoding.ASCII.GetString(lb2);
            _logger.Debug("read 8 bytes");

            //最後に文字列が来る
            byte[] lb3 = new byte[len];
            //stream.Read(inBuffer, 0, len);
            ar = stream.BeginRead(lb3, 0, len, (ac) => { signal.Set(); }, null);
            signal.WaitOne();
            signal.Reset();
            if (!ar.IsCompleted || _exit) { p.exit = true; return p; }
            stream.EndRead(ar);

            _logger.Debug("read {0} bytes strings", lb3.Length);

            //タブ文字で分割して名前とメッセージに分ける
            string tmp = streamEncoding.GetString(lb3);
            string[] ary = tmp.Split('\t');
            if(ary.Length < 2)
            {
                //タブ文字なければぜんぶメッセージ
                p.name = "";
                p.text = tmp;
            }
            else
            {
                p.name = ary[0];
                p.text = ary[1];
            }
            _logger.Debug("name {0} msg {1}", p.name, p.text);

            return p;
        }

        public int Write(Parameters p)
        {
            _logger.Debug("write in");
            //まず文字列をバイト配列にしてみてサイズを計る
            byte[] testBuffer = streamEncoding.GetBytes(p.name + "\t" + p.text);
            int len = testBuffer.Length;

            //msg置き換えモード
            string mode = (p.render == GlobalV.RenderStyle.APPEND) ? "0" : "1";

            //サイズ、CHAT_ID、文字列の順に組み立ててサイズを再計測
            string buf = string.Format("{0}{1:D4}{2}{3}\t{4}", mode, len, p.chat_id, p.name, p.text);
            byte[] outBuffer = streamEncoding.GetBytes(buf);
            len = outBuffer.Length;

            _logger.Debug("write start {0} byte", len);

            //バイト配列を書き込む
            signal.Reset();
            IAsyncResult ar = stream.BeginWrite(outBuffer, 0, len, (ac)=> { signal.Set(); }, null);
            ar.AsyncWaitHandle.WaitOne();
            signal.WaitOne();
            signal.Reset();
            if (!ar.IsCompleted || _exit) { p.exit = true; return 0; }
            stream.EndWrite(ar);
            //確定
            stream.Flush();
            _logger.Debug("write end");

            return len + 13;
        }
    }
}
