using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HnTraffic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入您的手机号码：");
            string comPhone = Console.ReadLine();
            List<KeyValuePair<string, string>> paramArray = new List<KeyValuePair<string, string>>();
            for (int i = 9010; i > 8999; i--)
            {
                paramArray.Add(new KeyValuePair<string, string>("phone", comPhone.Substring(0, 7) + i + ""));
                paramArray.Add(new KeyValuePair<string, string>("phone", "13700700000"));
                paramArray.Add(new KeyValuePair<string, string>("comPhone", comPhone));
                //string result = HTTPClientHelper.HttpPostRequestAsync("https://h5.ha.chinamobile.com/hnmccClientWap/RecommendedGift2020_12/allrecommendx.action", paramArray, "https://h5.ha.chinamobile.com/hnmccClientWap/2020/12/recommendGift/index.html?jmphone=" + comPhone + "");
                string result = HTTPClientHelper.HttpPostRequestAsync("https://h5.ha.chinamobile.com/hnmccClientWap/RecommendedGift2020_12/allrecommendx.action", paramArray, "https://h5.ha.chinamobile.com/hnmccClientWap/2020/12/recommendGift/index.html?jmphone=" + comPhone + "");
                Console.WriteLine("Result：{0}", result);
                paramArray.RemoveRange(0, 2);
            }
        }
    }
}
