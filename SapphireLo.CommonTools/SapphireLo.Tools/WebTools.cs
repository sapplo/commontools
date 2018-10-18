using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SapphireLo.Tools
{
    public enum TMethod
    {
        GET = 0,
        POST = 1,
    }

    public enum TContentType
    {
        None = 999,
        FormUrl = 0,
        FormData = 1,
        Json = 2,
        TextXml = 3,
        TextHtml = 4,
        TextPlain = 5,
    }
    public class WebTools
    {
        protected static readonly Dictionary<TContentType, string> TCONTENTTYPE = new Dictionary<TContentType, string>();
        static WebTools()
        {
            TCONTENTTYPE.Add(TContentType.FormUrl, "application/x-www-form-urlencoded");
            TCONTENTTYPE.Add(TContentType.FormData, "multipart/form-data");
            TCONTENTTYPE.Add(TContentType.Json, "application/json");
            TCONTENTTYPE.Add(TContentType.TextXml, "text/xml");
            TCONTENTTYPE.Add(TContentType.TextHtml, "text/html");
            TCONTENTTYPE.Add(TContentType.TextPlain, "text/plain");
        }

        public static string GetRequest(string url, TContentType tcntype = TContentType.TextHtml, string charset = "utf-8")
        {
            Stream stream = null;
            WebResponse wbResponse = null;
            StreamReader streamReader = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.KeepAlive = false;
                request.Timeout = 180000;// 3分钟
                request.ServicePoint.Expect100Continue = false;
                if (tcntype != TContentType.None)
                {
                    request.ContentType = $"{TCONTENTTYPE[tcntype]};charset={charset}";
                }
                request.Method = "GET";
                wbResponse = request.GetResponse();
                stream = wbResponse.GetResponseStream();
                streamReader = new StreamReader(stream);

                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    streamReader.Close();
                }
                catch { }
                try
                {
                    stream.Close();
                }
                catch { }
                try
                {
                    wbResponse.Close();
                }
                catch { }
            }
        }

        public static string PostFormData(string url, byte[] args = null, TContentType tcntype = TContentType.FormData, string charset = "utf-8")
        {
            Stream stream = null;
            WebResponse wbResponse = null;
            StreamReader streamReader = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.KeepAlive = false;
                request.Timeout = 180000;// 3分钟
                request.ServicePoint.Expect100Continue = false;
                if (tcntype != TContentType.None)
                {
                    request.ContentType = $"{TCONTENTTYPE[tcntype]};charset={charset}";
                }
                request.Method = "POST";

                stream = request.GetRequestStream();
                if (args != null)
                {
                    stream.Write(args, 0, args.Length);
                }

                wbResponse = request.GetResponse();
                stream = wbResponse.GetResponseStream();
                streamReader = new StreamReader(stream);

                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    streamReader.Close();
                }
                catch { }
                try
                {
                    stream.Close();
                }
                catch { }
                try
                {
                    wbResponse.Close();
                }
                catch { }
            }
        }

        public static string PostJson(string url, string json, string charset = "utf-8")
        {
            Stream stream = null;
            WebResponse wbResponse = null;
            StreamReader streamReader = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.KeepAlive = false;
                request.Timeout = 180000;// 3分钟
                request.ServicePoint.Expect100Continue = false;
                request.ContentType = $"application/json;charset={charset}";
                request.Method = "POST";

                stream = request.GetRequestStream();
                if (json.IsNotNullOrEmpty())
                {
                    var args = Encoding.UTF8.GetBytes(json);
                    stream.Write(args, 0, args.Length);
                }

                wbResponse = request.GetResponse();
                stream = wbResponse.GetResponseStream();
                streamReader = new StreamReader(stream);

                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    streamReader.Close();
                }
                catch { }
                try
                {
                    stream.Close();
                }
                catch { }
                try
                {
                    wbResponse.Close();
                }
                catch { }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookies">key:domain,value:.5173abc=hhhhhhh</param>
        /// <param name="method"></param>
        /// <param name="tcntype"></param>
        /// <param name="charset"></param>
        /// <param name="args"></param>
        /// <param name="postString"></param>
        /// <returns></returns>
        public static string RequestWithCookies(string url, List<KeyValuePair<string, KeyValuePair<string, string>>> cookies, TMethod method = TMethod.GET, TContentType tcntype = TContentType.FormUrl, string charset = "utf-8", byte[] args = null, string postString = "")
        {
            Stream stream = null;
            WebResponse wbResponse = null;
            StreamReader streamReader = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.KeepAlive = false;
                request.Timeout = 180000;// 3分钟
                request.ServicePoint.Expect100Continue = false;
                request.ContentType = $"{TCONTENTTYPE[tcntype]};charset={charset}";
                request.Method = method.ToString();

                if (method == TMethod.POST)
                {
                    stream = request.GetRequestStream();

                    if (postString.IsNotNullOrEmpty())
                    {
                        args = Encoding.UTF8.GetBytes(postString);
                    }
                    if (args != null)
                    {
                        stream.Write(args, 0, args.Length);
                    }
                }
                if (cookies != null && cookies.Count > 0)
                {
                    CookieContainer cc = new CookieContainer();
                    //cc.MaxCookieSize = 65535;
                    foreach (KeyValuePair<string, KeyValuePair<string, string>> kvp in cookies)
                    {
                        var item = kvp.Key;
                        var vle = kvp.Value.Value;
                        if (vle.Contains(","))
                        {
                            vle = System.Web.HttpUtility.UrlEncode(vle);
                        }
                        cc.Add(new Cookie()
                        {
                            CommentUri = new Uri(url),
                            Domain = kvp.Key,
                            Name = kvp.Value.Key,
                            Value = vle
                        });
                    }
                    //request.CookieContainer.MaxCookieSize = 65535;
                    request.CookieContainer = cc;
                }

                wbResponse = request.GetResponse();
                stream = wbResponse.GetResponseStream();
                streamReader = new StreamReader(stream);

                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    streamReader.Close();
                }
                catch { }
                try
                {
                    stream.Close();
                }
                catch { }
                try
                {
                    wbResponse.Close();
                }
                catch { }
            }
        }


        public static string Alert(string msg)
        {
            return $"<script>alert(\"{msg}\");</script>";
        }
    }
}
