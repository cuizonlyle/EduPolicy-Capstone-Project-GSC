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
using front3.Models;

namespace front.Controllers
{
    public class MarshallLoginController : Controller
    {
        // GET: MarshallLogin
        public ActionResult marshallLogin()
        {
            // Redirect to dashboard if already authenticated
            if (User.Identity.IsAuthenticated == true)
            {
                FormsAuthentication.RedirectFromLoginPage(User.Identity.Name, false);
            }
            return RedirectToAction("HomePage", "Home");
        }

        [HttpPost]
        public ActionResult MarshallLogin(loginmodel l)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string postData = "&u_k=" + l.uk;
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/marshall/login?" + postData);
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
                            FormsAuthentication.SetAuthCookie(l.uk, false);
                            Session["Marshall"] = l.uk;
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