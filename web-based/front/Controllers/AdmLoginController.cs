using front3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace front.Controllers
{
    public class AdmLoginController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdmLogin(loginmodel adm)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string postData = "&username=" + adm.username
                        + "&userpassword=" + adm.userpassword;
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/admin/login?" + postData);
                    Byte[] data = Encoding.UTF8.GetBytes(postData);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req.ContentLength = data.Length;

                    using (Stream stream = req.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    using (res = req.GetResponse())
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                        int comVal = msg.CompareTo("Successfully Logged in");
                        if (comVal == 0)
                        {
                            FormsAuthentication.SetAuthCookie(adm.username, false);
                            Session["Adm"] = adm.username;
                            return Content("Successfully Logged in", "text/plain", Encoding.UTF8);
                        }
                        else
                        {
                            return Content(msg, "text/plain", Encoding.UTF8);
                        }
                    }
                }
                catch (WebException ex)
                {
                    using (HttpWebResponse res = (HttpWebResponse)ex.Response)
                    using (Stream receiveStream = res.GetResponseStream())
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                    }
                }
            }
            catch (WebException ex)
            {
                using (HttpWebResponse res = (HttpWebResponse)ex.Response)
                using (Stream receiveStream = res.GetResponseStream())
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                }
            }
        }
    }
}