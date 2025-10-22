using System;
using System.Net;
using System.Text;

namespace DP9Connector
{
    public class WIGConnector : IDisposable
    {
        string template = "--{0}--[1}--{2}--{3}--";
        string url = "http://192.168.0.0:10000/";
        string boundary = "aaaaaaaaaaa";
        WebClient client = new WebClient();

        public WIGConnector(string url)
        {
            this.url = url;

            client.Encoding = Encoding.UTF8;
            client.BaseAddress = url;
            client.Headers = GetHeaders();
        }

        public string MessageSend(string msisdn, string message, string link)
        {
            string request = GetRequest(msisdn, message, link);
            var content = client.Encoding.GetBytes(request);
            var result = client.UploadData(url, "POST", content);
            return client.Encoding.GetString(result);            
        }

        private string GetRequest(string msisdn, string text, string url)
        {
            return string.Format(template, msisdn, text, url, boundary);
        }

        private WebHeaderCollection GetHeaders()
        {
            WebHeaderCollection webHeaders = new WebHeaderCollection();
            webHeaders.Add(HttpRequestHeader.Accept, "text/*");
            webHeaders.Add(HttpRequestHeader.AcceptCharset, "iso-8859-1;UTF-8");
            webHeaders.Add(HttpRequestHeader.AcceptEncoding, "identity");
            webHeaders.Add(HttpRequestHeader.AcceptLanguage, string.Empty);
            webHeaders.Add(HttpRequestHeader.UserAgent, "Test Browser/1.1");
            webHeaders.Add(HttpRequestHeader.Host, "1.1.1.1:01");
            webHeaders.Add(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + boundary + "; type=\"application/xml\"");
            return webHeaders;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}

