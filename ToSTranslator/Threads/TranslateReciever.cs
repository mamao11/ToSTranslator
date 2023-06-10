using System;
using System.IO.Pipes;
using System.Threading;
using System.Windows.Forms;

namespace ToSTranslator
{
    class TranslateReciever : ThreadBase
    {
        private string _pipe_nm = "tos_pipe1";
        private NamedPipeServerStream recv_p = null;
        private int _cur_id = 0;

        ManualResetEvent signal = new ManualResetEvent(false);  //pipeのブロックをこっちでコントロール
        ToSStream ss = null;    //read stream（中断処理を送るためクラス変数）

        public TranslateReciever(Form form) : base(form)
        {

        }

        //受信スレッド
        public override void Start()
        {
            PushMessage("<---- 受信スレッド起動 ---->", MessageType.INFO);

            while (!_exit)
            {
                //名前付きパイプを開始
                //recv_p = new NamedPipeServerStream(_pipe_nm, PipeDirection.InOut, 2);
                recv_p = new NamedPipeServerStream(_pipe_nm, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                signal.Reset();

                PushMessage("-- 受信接続待ち --", MessageType.INFO);
                PushStatus(StatusType.ReciverOn);
                //recv_p.WaitForConnection();

                //接続待ち開始
                IAsyncResult ar = recv_p.BeginWaitForConnection((a) => { signal.Set(); }, null);
                //接続シグナル待ち（ブロック）
                signal.WaitOne();
                signal.Reset();

                if (_exit)
                {
                    //終了指示のためここでループ抜け
                    _logger.Debug("受信スレッド終了指示確認1");
                    break;
                }

                if (ar.IsCompleted)
                {
                    recv_p.EndWaitForConnection(ar);
                    //接続完了

                    try
                    {
                        if (recv_p.IsConnected)
                        {
                            PushMessage("-- 受信スレッド接続 --", MessageType.INFO);
                            PushStatus(StatusType.ReciverConnect);

                            //受信stream生成
                            ss = new ToSStream(recv_p);

                            //接続が来たらデータ待ちに移行
                            while (!_exit)
                            {
                                _logger.Debug("受信スレッド依頼待ち");
                                // 受信待ち
                                ToSStream.Parameters recv = ss.Read();  //データが来ていない場合はここでブロック状態に入る

                                if (_exit || (recv != null && recv.exit))
                                {
                                    _logger.Debug("受信スレッド終了指示確認2");
                                    PushMessage("-- 受信スレッド切断 --", MessageType.WARN);
                                    PushStatus(StatusType.ReciverOn);
                                    break;
                                }

                                //データが来た
                                GlobalV.TranslateItem item = new GlobalV.TranslateItem()
                                {
                                    id = ++_cur_id,    //+1してからセット
                                    chat_id = recv.chat_id,
                                    source_name = recv.name,
                                    source_text = recv.text
                                };
                                //翻訳キューへ追加
                                GlobalV.sourceQueue.Enqueue(item);
                                //翻訳キューへ追加成功
                                _logger.Debug("ID:{0} 翻訳キュー:{1}", item.chat_id, item.source_text);
                                PushTranslateEvent(item, EventType.TranslateQueue);
                            }

                            //streamは使い終わったら処理
                            if (ss != null) { ss.Exit(); ss = null; }
                        }
                    }
                    catch(Exception ex)
                    {
                        //pipe切断
                        PushMessage("-- 受信スレッドERROR -- : " + ex.Message, MessageType.WARN);
                        _logger.Debug("受信スレッドERROR " + ex.Message);
                    }
                }
                //接続が切られるとpipeが破棄されるので終了処理
                recv_p.Dispose();
                recv_p = null;
            }
            //pipe開放
            if (recv_p != null) { recv_p.Dispose(); recv_p = null; }
        }
        //受信スレッド終了
        public override void Exit()
        {
            _exit = true;
            if (recv_p != null)
            {
                //stream読み取り状態ならそれを終了させる
                if (ss != null) { ss.Exit(); }
                //接続待ちかもしれないのでシグナルをセットして終了させる
                signal.Set();
            }
        }
    }
}
