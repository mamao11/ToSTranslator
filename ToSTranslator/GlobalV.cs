using System.Collections.Concurrent;

namespace ToSTranslator
{
    public static class GlobalV
    {
        public class TranslateItem
        {
            public int id = 0;
            public string chat_id = "";
            public string source_name = ""; //キャラ名
            public string source_text = ""; //メッセージ
            public string translated_name = "";
            public string translated_text = "";
            public bool on_dic = false; //辞書にあり
        }

        public const int MAX_QUEUE = 100;
        public static ConcurrentQueue<TranslateItem> sourceQueue = new ConcurrentQueue<TranslateItem>();
        public static ConcurrentQueue<TranslateItem> deliverQueue = new ConcurrentQueue<TranslateItem>();

        public enum TranslateAPI { None, DeepL, MinTranslate };
        public static TranslateAPI API = TranslateAPI.MinTranslate;    //デフォみんなの自動翻訳

        public enum RenderStyle { APPEND, REPLACE };
        public static RenderStyle renderStyle = RenderStyle.APPEND;
    }
}
