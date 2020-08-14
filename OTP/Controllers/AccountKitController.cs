using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OTP.Controllers
{
    public class AccountKitController : Controller
    {
        private const string appId = "2236335950012706";
        private const string appSecret = "c140ea1690fb0b9e7dad863d4ca657c7";
        private const string appVersion = "v1.1";
        private const string TokenEndpoint = "https://graph.accountkit.com/{0}/access_token";
        private const string UserInfoEndpoint = "https://graph.accountkit.com/{0}/me";

        [NonAction]
        private static Uri BuildUri(string baseUri, NameValueCollection queryParameters)
        {
            var q = HttpUtility.ParseQueryString(string.Empty);
            q.Add(queryParameters);
            var builder = new UriBuilder(baseUri) { Query = q.ToString() };
            return builder.Uri;
        }

        [NonAction]
        private string QueryAccessToken(string code)
        {
            var access_token = string.Join("|", new string[] { "AA", appId, appSecret });
            var uri = BuildUri(string.Format(TokenEndpoint, appVersion), new NameValueCollection {
                    { "grant_type", "authorization_code" },
                    { "code", code },
                    { "access_token", access_token }
            });
            string accessToken = "";
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(uri);
                using (var webResponse = webRequest.GetResponse())
                using (var stream = webResponse.GetResponseStream())
                {
                    if (stream == null)
                        return null;
                    using (var textReader = new StreamReader(stream))
                    {
                        var response = JObject.Parse(textReader.ReadToEnd());
                        accessToken = response["access_token"].Value<string>();
                    }
                }
            }
            catch (Exception exp)
            {
            }
            return accessToken;
        }

        public JsonResult TestAjax(string code, string csrf_nonce, string returnUrl)
        {
            //var client = new HttpClient();
            //var param = new Dictionary<string, string>();
            //param["grant_type"] = "authorization_code";
            //param["code"] = code;
            //param["access_token"] = "AA|" + appId + "|" + appSecret;

            //var response = await client.PostAsync(string.Format(TokenEndpoint, appVersion), new FormUrlEncodedContent(param));
            //var contents = await response.Content.ReadAsStringAsync();

            var accessToken = this.QueryAccessToken(code);
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return Json(new
                {
                    success = false,
                    returnUrl = returnUrl,
                    message = "Empty accessToken"
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                returnUrl = returnUrl,
                message = "Berhasil"
            }, JsonRequestBehavior.AllowGet);

            //return Json(new { message = "xxx" }, JsonRequestBehavior.AllowGet);
        }
    }
}