using Microsoft.Win32.SafeHandles;
using System;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToSTranslator
{
    class TranslateDeliver : ThreadBase
    {
        private string _pipe_nm = "tos_pipe2";
        private NamedPipeServerStream send_p = null;

        private Task _pipe_mon = null;
        private bool _pipe_connect = false;

        ManualResetEvent signal = new ManualResetEvent(false);  //pipeのブロックをこっちでコントロール
        ToSStream ss = null;    //read stream（中断処理を送るためクラス変数）

        public TranslateDeliver(Form form) : base(form)
        {
        }

        public override void Start()
        {
            PushMessage("<---- 配信スレッド起動 ---->", MessageType.INFO);

            while (!_exit)
            {
                try
                {
                    //名前付きパイプを開始
                    //recv_p = new NamedPipeServerStream(_pipe_nm, PipeDirection.InOut, 2);
                    send_p = new NamedPipeServerStream(_pipe_nm, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
               }
                catch (Exception ex)
                {
                    //パイプ作成エラー時は再作成で対処してみる
                    PushMessage("配信パイプ生成エラー（再生成試行）:" + ex.Message, MessageType.INFO);
                    Thread.Sleep(500);
                }

                if(send_p != null)
                {
                    //パイプ作成成功なら開始
                    signal.Reset();

                    PushMessage("-- 配信接続待ち --", MessageType.INFO);
                    //send_p.WaitForConnection();
                    PushStatus(StatusType.DeliverOn);

                    //接続待ち開始
                    IAsyncResult ar = send_p.BeginWaitForConnection((a) => { signal.Set(); }, null);
                    //接続シグナル待ち（ブロック）
                    signal.WaitOne();
                    signal.Reset();

                    if (_exit)
                    {
                        //終了指示のためここでループ抜け
                        _logger.Debug("配信スレッド終了指示確認1");
                        break;
                    }

                    if (ar.IsCompleted)
                    {
                        send_p.EndWaitForConnection(ar);
                        if (send_p.IsConnected)
                        {
                            //接続完了
                            PushMessage("-- 配信スレッド接続 --", MessageType.INFO);
                            PushStatus(StatusType.DeliverConnect);

                            try
                            {
                                //配信stream生成
                                ss = new ToSStream(send_p);
                            }catch(Exception ex)
                            {
                                PushMessage("配信ストリーム接続失敗。再試行：" + ex.Message, MessageType.WARN);
                                //stream生成失敗ならパイプから作り直し
                                if (ss != null) { ss = null; }
                                if (send_p != null) { send_p.Dispose(); send_p = null; }
                                PushStatus(StatusType.DeliverOff);
                                Thread.Sleep(500);
                            }
                            //ストリーム成功なら配信待ち開始
                            if (ss != null)
                            {
                                _logger.Debug("配信キュー待ち開始");

                                _pipe_mon = Task.Run(() => PipeMonitor());

                                //接続が来たら配信キューから送信
                                while (!_exit)
                                {
                                    try
                                    {
                                        //_logger.Debug("配信キュー数 {0}", GlobalV.deliverQueue.Count);

                                        GlobalV.TranslateItem item = null;
                                        //翻訳キューから取り出し
                                        if (GlobalV.deliverQueue.TryDequeue(out item))
                                        {
                                            _logger.Debug("配信キュー取り出しOK");

                                            //streamへ書き込み
                                            var write = ss.Write(new ToSStream.Parameters() {
                                                chat_id = item.chat_id,
                                                name = item.translated_name,
                                                text = item.translated_text,
                                                render = GlobalV.renderStyle 
                                            });
                                            _logger.Debug("配信処理:{0} :{1}", item.chat_id, item.translated_text);

                                            //配信をフォームへ
                                            PushTranslateEvent(item, EventType.TranslateReturn);
                                        }
                                        //念のため0.1秒待ち
                                        Thread.Sleep(100);

                                        //モニタースレッドを頼りに切断確認
                                        if (!_pipe_connect)
                                        {
                                            //切断されているのでstreamとpipe閉じ
                                            if (ss != null) { ss.Exit(); ss = null; }
                                            if (send_p != null) { send_p.Dispose(); send_p = null; }
                                            if (_pipe_mon != null) { _pipe_mon.Dispose(); _pipe_mon = null; }
                                            _logger.Debug("配信スレッド切断");
                                            PushMessage("-- 配信スレッド切断 --", MessageType.WARN);
                                            PushStatus(StatusType.DeliverOff);
                                            break;
                                        }
                                        //IAsyncResult kar = send_p.BeginRead(ka, 0, 1, null, null);
                                        //Thread.Sleep(50);
                                        //send_p.EndRead(kar);
                                    }
                                    catch (Exception ex)
                                    {
                                        //pipe切断
                                        PushMessage("-- 配信スレッドERROR -- : " + ex.Message, MessageType.WARN);
                                        _logger.Debug("配信スレッドERROR " + ex.Message);
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }

        private void PipeMonitor()
        {
            byte[] ka = new byte[1];   //疎通確認用
            //常時read状態で待機。切断されたら例外が出て終了する
            try
            {
                IAsyncResult ar = send_p.BeginRead(ka, 0, 0, null, null);

                _logger.Debug("配信パイプモニタ開始");
                _pipe_connect = true;

                ar.AsyncWaitHandle.WaitOne();
                //Task task = send_p.ReadAsync(ka, 0, 0);
                //task.Wait();
            }
            catch (Exception ex)
            {
                _logger.Debug("例外：" + ex.Message);
            }
            _pipe_connect = false;
            _logger.Debug("配信パイプ切断:");
        }
        private void CancelPipe(SafePipeHandle handle)
        {
            try
            {
                CancelIo(handle);
            }
            catch (ObjectDisposedException) { }
        }

        public override void Exit()
        {
            _exit = true;
            if (send_p != null)
            {
                //stream読み取り状態ならそれを終了させる
                if (ss != null) { ss.Exit(); }
                if (_pipe_mon != null && _pipe_mon.Status == TaskStatus.Running) { CancelPipe(send_p.SafePipeHandle); }
                Thread.Sleep(100);
                //接続待ちかもしれないのでシグナルをセットして終了させる
                signal.Set();
            }
        }
    }
}
