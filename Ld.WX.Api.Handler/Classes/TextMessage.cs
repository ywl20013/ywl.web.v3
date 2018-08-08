using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Ld.WX.Api.Handler
{
    /// <summary>
    /// TextMessage 的摘要说明
    /// </summary>
    public class TextMessage
    {
        public string ToUserNmae { set; get; }
        public string FromUserName { set; get; }
        public int CreateTime { set; get; }
        public string MsgType { set; get; }
        public string Content { set; get; }
        public string Event { set; get; }
        public string EventKey { set; get; }
        public Int64 MsgID { set; get; }
        public TextMessage(XmlDocument doc)
        {
            XmlNode xToUserName = doc.SelectSingleNode("/xml/ToUserName");
            XmlNode xFromUserName = doc.SelectSingleNode("/xml/FromUserName");
            XmlNode xContent = doc.SelectSingleNode("/xml/Content");
            XmlNode xMsgType = doc.SelectSingleNode("/xml/MsgType");
            XmlNode xCreateTime = doc.SelectSingleNode("/xml/CreateTime");
            XmlNode xMsgID = doc.SelectSingleNode("/xml/MsgId");
            XmlNode xEvent = doc.SelectSingleNode("/xml/Event");
            XmlNode xEventKey = doc.SelectSingleNode("/xml/EventKey");
            ToUserNmae = xToUserName.InnerText;
            FromUserName = xFromUserName.InnerText;
            Content = xContent == null ? "" : xContent.InnerText;
            MsgType = xMsgType.InnerText;
            CreateTime = Convert.ToInt32(xCreateTime.InnerText);
            MsgID = xMsgID == null ? -1 : Convert.ToInt64(xMsgID.InnerText);
            Event = xEvent == null ? "" : xEvent.InnerText;
            EventKey = xEventKey == null ? "" : xEventKey.InnerText;
        }
        public string Deal()
        {
            RetTextMessage rtm = new RetTextMessage();
            rtm.ToUserName = FromUserName;
            rtm.FromUserName = ToUserNmae;
            if (EventKey == "aqscgl" || Content.IndexOf("安全审核") >= 0)
            {
                rtm.Content = "点击下面地址创建“安全审核”数据：\r\n<a href=\"http://www.bjdflld.com/services\">点击进入</a>";
            }
            else if (EventKey == "default" || Content.IndexOf("官方网站") >= 0)
            {
                rtm.Content = "欢迎访问官方网站：\r\n<a href=\"http://www.bjdflld.com/\">点击进入</a>";
            }
            else
            {
                rtm.Content = "欢迎使用微信公共账号，您输入的内容为：" + Content + "\r\n<a href=\"http://www.baidu.com\">点击进入</a>";
            }
            return rtm.ToXmlString();

        }
    }


    public class RetTextMessage
    {
        public string ToUserName { set; get; }
        public string FromUserName { set; get; }
        public long CreateTime { set; get; }
        public string MsgType { set; get; }
        public string Content { set; get; }
        public RetTextMessage()
        {
            ToUserName = "";
            FromUserName = "";
            CreateTime = DateTime.Now.Ticks;
            MsgType = "";
            Content = "";
        }
        public string ToXmlString()
        {
            string responseContent = "";

            responseContent = string.Format(ReplyType.Message_Text,
                ToUserName,
                FromUserName,
                DateTime.Now.Ticks,
                Content);
            return responseContent;
        }
    }
    public class ReplyType
    {
        /// <summary>
        /// 普通文本消息
        /// </summary>
        public static string Message_Text
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[text]]></MsgType>
                            <Content><![CDATA[{3}]]></Content>
                            </xml>";
            }
        }
        /// <summary>
        /// 图文消息主体
        /// </summary>
        public static string Message_News_Main
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[news]]></MsgType>
                            <ArticleCount>{3}</ArticleCount>
                            <Articles>
                            {4}
                            </Articles>
                            </xml> ";
            }
        }
        /// <summary>
        /// 图文消息项
        /// </summary>
        public static string Message_News_Item
        {
            get
            {
                return @"<item>
                            <Title><![CDATA[{0}]]></Title> 
                            <Description><![CDATA[{1}]]></Description>
                            <PicUrl><![CDATA[{2}]]></PicUrl>
                            <Url><![CDATA[{3}]]></Url>
                            </item>";
            }
        }
    }
}