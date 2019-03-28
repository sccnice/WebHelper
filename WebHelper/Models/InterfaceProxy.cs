using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebHelper.Models
{
    public class InterfaceProxy
    {
       // private static readonly string _rdUrl = Config.Get("NXinQlwService");
        public static string GetResult(HttpRequest request, string url, string accept, string contentType, string requestType, string parms, bool recordResuklt = false)
        {
            requestType = requestType.ToUpperInvariant();
            var result = string.Empty;
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = 300000;//100秒超时时间较短，需要调整成为300秒
                if (!string.IsNullOrEmpty(accept))
                {
                    myRequest.Accept = accept;
                }
                if (!string.IsNullOrEmpty(contentType))
                {
                    myRequest.ContentType = contentType;
                }

                if (!string.IsNullOrEmpty(GetReferrer(request)))
                {
                    myRequest.Referer = GetReferrer(request);
                }

                myRequest.Method = requestType;
                if (myRequest.Method == "POST")
                {
                    var reqStream = myRequest.GetRequestStream();
                    var encoding = Encoding.GetEncoding("utf-8");
                    var inData = encoding.GetBytes(parms);
                    reqStream.Write(inData, 0, inData.Length);
                    reqStream.Close();
                }
                //发送post请求到服务器并读取服务器返回信息    
                var res = (HttpWebResponse)myRequest.GetResponse();

                if (res.StatusCode != HttpStatusCode.OK) return result;

                var receiveStream = res.GetResponseStream();
                var encode = Encoding.GetEncoding("utf-8");
                if (receiveStream != null)
                {
                    var readStream = new StreamReader(receiveStream, encode);
                    var oResponseMessage = readStream.ReadToEnd();
                    res.Close();
                    readStream.Close();
                    result = oResponseMessage;
                }
                stopWatch.Stop();

                //if (!recordResuklt && !string.IsNullOrWhiteSpace(url))
                //{
                //    if (url.IndexOf(_rdUrl) < 0)
                //    {
                //        recordResuklt = true;
                //    }
                //}
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetReferrer(HttpRequest request)
        {
                 try
                {
                    return request.GetAbsoluteUri();
                }
                catch
                {
                    return string.Empty;
                }
        }

    }

}
namespace Microsoft.AspNetCore.Http
{
    public static class HttpRequestExtensions
    {
        public static string GetAbsoluteUri(this HttpRequest request)
        {
            return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }
    }
}
