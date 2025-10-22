using System;
using System.Net;
using System.Text;

namespace DP9Connector
{
    public class WIGConnector : IDisposable
    {
        string url = "http://192.168.167.53:10110/";
        string boundary = "asdlfkjiurwghasf";
        WebClient client = new WebClient();

        public WIGConnector(string url)
        {
            this.url = url;

            client.Encoding = Encoding.UTF8;
            client.BaseAddress = url;
            client.Headers = GetHeaders();
        }

        public string DSTKSend(string msisdn, string message, string link)
        {
            string request = GetRequest(msisdn, message, link);
            var content = client.Encoding.GetBytes(request);
            var result = client.UploadData(url, "POST", content);
            return client.Encoding.GetString(result);            
        }

        private string GetRequest(string msisdn, string text, string url)
        {
            return string.Format(Resources.DSTKTemplate, msisdn, text, url, boundary);
        }

        private WebHeaderCollection GetHeaders()
        {
            WebHeaderCollection webHeaders = new WebHeaderCollection();
            webHeaders.Add(HttpRequestHeader.Accept, "text/*");
            webHeaders.Add(HttpRequestHeader.AcceptCharset, "iso-8859-1;UTF-8");
            webHeaders.Add(HttpRequestHeader.AcceptEncoding, "identity");
            webHeaders.Add(HttpRequestHeader.AcceptLanguage, string.Empty);
            webHeaders.Add(HttpRequestHeader.UserAgent, "WIG Browser/1.1");
            webHeaders.Add(HttpRequestHeader.Host, "10.77.1.7:5008");
            webHeaders.Add(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + boundary + "; type=\"application/xml\"");
            return webHeaders;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
