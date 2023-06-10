using Microsoft.Win32.SafeHandles;
using NLog;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ToSTranslator
{
    public abstract class ThreadBase : IDisposable
    {
        protected Logger _logger = LogManager.GetCurrentClassLogger();

        protected Form _form;
        protected bool _exit = false;

        [DllImport("kernel32.dll")]
        protected static extern bool CancelIo(SafePipeHandle handle);


        public ThreadBase(Form form)
        {
            _form = form;
        }
        ~ThreadBase()
        {
            Dispose();
        }


        public virtual void Dispose()
        {
            _logger.Debug("ThreadBase dispose.");
        }

        //継承時にスレッド処理を実装すべし
        public abstract void Start();
        //継承時にスレッド終了処理を実装すべし
        public abstract void Exit();



        #region メインフォームへの通信
        public enum EventType { TranslateQueue, TranslateStart, TranslateEnd, TranslateReturn };
        public enum StatusType 
        {
            ReciverOn,
            ReciverOff,
            ReciverConnect,
            TranslateOff,
            TranslateConnect,
            DeliverOn,
            DeliverOff,
            DeliverConnect,
        };
        public enum MessageType { INFO, WARN };
        public class MsgEventArgs : EventArgs
        {
            public string Type = "";
            public string Message = "";
            public GlobalV.TranslateItem translateItem = null;
        }
        public delegate void SubThreadRunningEventHandler(object sender, MsgEventArgs e);
        public event SubThreadRunningEventHandler OnSubThreadRunning = null;

        //イベントメッセージをフォームへ送信
        protected void PushTranslateEvent(GlobalV.TranslateItem item, EventType et)
        {
            string typ = "";
            switch(et)
            {
                case EventType.TranslateQueue:
                    typ = "TranslateQueue";
                    break;
                case EventType.TranslateStart:
                    typ = "TranslateStart";
                    break;
                case EventType.TranslateEnd:
                    typ = "TranslateEnd";
                    break;
                case EventType.TranslateReturn:
                    typ = "TranslateReturn";
                    break;
            }

            // Invoke()を使って実行を通知する
            MsgEventArgs e = new MsgEventArgs()
            {
                Type = typ,
                translateItem = item
            };
            if(OnSubThreadRunning != null && !_form.IsDisposed)
                _form.Invoke(OnSubThreadRunning, new object[] { this, e });
        }

        //翻訳履歴件数をフォームへ通知
        protected void PushCount(int cnt)
        {
            // Invoke()を使って実行を通知する
            MsgEventArgs e = new MsgEventArgs()
            {
                Type = "Dictionary",
                Message = cnt.ToString()
            };
            if (OnSubThreadRunning != null && !_form.IsDisposed)
                _form.Invoke(OnSubThreadRunning, new object[] { this, e });
        }

        //メッセージをフォームへ通知
        protected void PushMessage(string msg, MessageType mt)
        {
            // Invoke()を使って実行を通知する
            MsgEventArgs e = new MsgEventArgs()
            {
                Type = (mt==MessageType.INFO)? "Message":"Warning",
                Message = msg
            };
            if (OnSubThreadRunning != null && !_form.IsDisposed)
                _form.Invoke(OnSubThreadRunning, new object[] { this, e });
        }

        //ステータスをフォームへ通知
        protected void PushStatus(StatusType st)
        {
            string typ = "";
            switch (st)
            {
                case StatusType.ReciverOff:
                    typ = "ReciverOff";
                    break;
                case StatusType.ReciverOn:
                    typ = "ReciverOn";
                    break;
                case StatusType.ReciverConnect:
                    typ = "ReciverConnect";
                    break;
                case StatusType.TranslateOff:
                    typ = "TranslateOff";
                    break;
                case StatusType.TranslateConnect:
                    typ = "TranslateConnect";
                    break;
                case StatusType.DeliverOff:
                    typ = "DeliverOff";
                    break;
                case StatusType.DeliverOn:
                    typ = "DeliverOn";
                    break;
                case StatusType.DeliverConnect:
                    typ = "DeliverConnect";
                    break;
            }

            // Invoke()を使って実行を通知する
            MsgEventArgs e = new MsgEventArgs()
            {
                Type = "StatusUpdate",
                Message = typ
            };
            if (OnSubThreadRunning != null && !_form.IsDisposed)
                _form.Invoke(OnSubThreadRunning, new object[] { this, e });
        }
        #endregion

    }
}
