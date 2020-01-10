using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HnydWeb.Hubs
{

    [HubName("myHub")]
    public class MyHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public async Task SendMessage(string msg)
        {
            string comPhone = msg.Split(';')[0];
            string numArea = msg.Split(';')[1];
            int rangeLeft = int.Parse(msg.Split(';')[2]);
            int rangeRight = int.Parse(msg.Split(';')[3]);
            List<KeyValuePair<string, string>> paramArray = new List<KeyValuePair<string, string>>();
            for (int i = rangeRight; i > rangeLeft; i--)
            {
                paramArray.Add(new KeyValuePair<string, string>("phone", numArea + i));
                paramArray.Add(new KeyValuePair<string, string>("comPhone", comPhone));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                //string result = HTTPClientHelper.HttpPostRequestAsync("https://h5.ha.chinamobile.com/hnmccClientWap/RecommendedGift2019_9/allrecommendx.action", paramArray, "https://h5.ha.chinamobile.com/hnmccClientWap/2019/09/recommendGift/index.html?jmphone=" + comPhone + "");
                string result = HTTPClientHelper.HttpPostRequestAsync("https://h5.ha.chinamobile.com/hnmccClientWap/RecommendedGift2019_12/allrecommendx.action", paramArray, "https://h5.ha.chinamobile.com/hnmccClientWap/2019/12/recommendGift/index.html?jmphone=" + comPhone + "");
                paramArray.RemoveRange(0, 2);
                await Clients.All.SendMessage(result);
            }
        }
    }
}
