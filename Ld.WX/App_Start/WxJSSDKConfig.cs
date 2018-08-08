using System;

namespace Ld.WX
{
    public class WxJSSDKConfig
    {
        public bool debug { get; set; }
        public int appId { get; set; }
        public long timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
        public string[] jsApiList { get; set; }

        public WxJSSDKConfig(int appid, bool debug, string[] apis)
        {
            this.appId = appid;
            this.debug = debug;
        }
        public static string create_nonce_str(int length)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = "";
            var rand = new Random();
            for (int i = 0; i < length; i++)
            {
                result += chars.Substring(rand.Next(0, chars.Length - 1), 1);
            }
            return result;
            //return Guid.NewGuid().ToString();
            //return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static long create_timestamp(DateTime time)
        {
            //  return (time.Millisecond / 1000);
            long intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (long)(time - startTime).TotalSeconds;
            return intResult;
        }
    }
}