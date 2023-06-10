using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ToSTranslator
{
    public class DeepL
    {
        private static ToSTranslator.Properties.Settings MySettings = ToSTranslator.Properties.Settings.Default;

        public class RequestParam
        {
            public string[] text;
            public string source_lang;
            public string target_lang;
        }
        public class ResponseItem
        {
            public string detected_source_language;
            public string text;
        }
        public class ResponseData
        {
            public List<ResponseItem> translations;
            public string message;
        }

        public string[] get_auto_trans(string[] str)
        {
            RequestParam req = new RequestParam()
            {
                text = str,
                source_lang = "KO", //韓国
                target_lang = "JA"  //日本
            };
            List<string> ret = new List<string>();
            string url = MySettings.DeepL_API_URL;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", "DeepL-Auth-Key " + MySettings.DeepL_API_KEY);

            var json = JsonConvert.SerializeObject(req);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                //5秒タイムアウトにしとこう
                client.Timeout = TimeSpan.FromMilliseconds(5000);

                using (HttpResponseMessage resp = client.SendAsync(request).Result)
                {
                    string responseBody = resp.Content.ReadAsStringAsync().Result;
                    ResponseData data = JsonConvert.DeserializeObject<ResponseData>(responseBody);
                    if (data.message != null)
                    {
                        ret.Add(data.message);
                    }
                    else
                    {
                        ret = data.translations.Select(v => v.text).ToList<string>();
                    }
                }
            }
            return ret.ToArray();
        }
    }
}
