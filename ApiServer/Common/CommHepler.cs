using System;
using System.IO;
using System.Text;
using System.Net;

namespace ApiServer.Common
{
    public static class CommHepler
    {
        public static T HttpPost<T>(string url, object obj)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html,appliction/xhtml+xml,*/*";
            request.ContentType = "application/json";
            byte[] buffer = encoding.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }
    }
}