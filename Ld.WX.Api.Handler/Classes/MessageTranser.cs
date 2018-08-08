using System.Xml;

namespace Ld.WX.Api.Handler
{
    /// <summary>
    /// MessageTranser 的摘要说明
    /// </summary>
    public class MessageTranser
    {
        public string _PostData = "";
        public MessageTranser(string posStr)
        {
            _PostData = posStr;

        }
        public string DealMessage()
        {
            string responseContent = "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(_PostData);
            XmlNode MsgType = xmldoc.SelectSingleNode("/xml/MsgType");
            if (MsgType != null)
            {
                switch (MsgType.InnerText.ToLower())
                {
                    case "event":
                        responseContent = DealEventMessage(xmldoc);//事件处理
                        break;
                    case "text":
                        responseContent = DealTextMessage(xmldoc);//接受文本消息处理
                        break;
                    case "voice":
                        //考虑语音识别.
                        break;
                    case "image":
                        break;
                    case "video":
                        break;
                    case "shortvideo":
                        break;
                    case "location":
                        break;
                    default:
                        break;
                }
            }
            return responseContent;
        }
        private string DealEventMessage(XmlDocument doc)
        {
            TextMessage tm = new TextMessage(doc);

            return tm.Deal();
        }
        private string DealTextMessage(XmlDocument doc)
        {
            TextMessage tm = new TextMessage(doc);

            return tm.Deal();
        }
    }
}