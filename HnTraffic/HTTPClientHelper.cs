
/****************************************************************

 * 作    者：ZhangYu

 * 命名空间 ：HnTraffic

 * 类 名 称 ：HTTPClientHelper

 * 机器名称 ：YUMASTER 

 * CLR 版本：4.0.30319.42000

 * 创建时间：2019/11/28 8:41:15

*****************************************************************

 * Copyright @ ZhangYu 2019 All rights reserved

*****************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HnTraffic
{
    public class HTTPClientHelper
    {
        private static readonly HttpClient HttpClient;
        static HTTPClientHelper()
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };
            HttpClient = new HttpClient(handler);
        }
        public static string HttpPostRequestAsync(string Url, List<KeyValuePair<string, string>> paramArray, string RefererUrl, string ContentType = "application/x-www-form-urlencoded")
        {
            string result = "";
            var postData = BuildParam(paramArray);
            var data = Encoding.ASCII.GetBytes(postData);
            try
            {
                using (HttpClient http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)");
                    http.DefaultRequestHeaders.Add("Accept", @"text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                    http.DefaultRequestHeaders.Add("Referer", RefererUrl);

                    HttpResponseMessage message = null;
                    using (Stream dataStream = new MemoryStream(data ?? new byte[0]))
                    {
                        using (HttpContent content = new StreamContent(dataStream))
                        {
                            content.Headers.Add("Content-Type", ContentType);
                            var task = http.PostAsync(Url, content);
                            message = task.Result;
                        }
                    }
                    if (message != null && message.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        using (message)
                        {
                            result = message.Content.ReadAsStringAsync().Result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private static string Encode(string content, Encoding encode = null)
        {
            if (encode == null) return content;

            return System.Web.HttpUtility.UrlEncode(content, Encoding.UTF8);

        }

        private static string BuildParam(List<KeyValuePair<string, string>> paramArray, Encoding encode = null)
        {
            string url = "";

            if (encode == null) encode = Encoding.UTF8;

            if (paramArray != null && paramArray.Count > 0)
            {
                var parms = "";
                foreach (var item in paramArray)
                {
                    parms += string.Format("{0}={1}&", Encode(item.Key, encode), Encode(item.Value, encode));
                }
                if (parms != "")
                {
                    parms = parms.TrimEnd('&');
                }
                url += parms;

            }
            return url;
        }
    }
}
