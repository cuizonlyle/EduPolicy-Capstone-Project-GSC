using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using front4.Models;

namespace front.Controllers
{
    public class AddNewStaffController : Controller
    {
        [HttpPost]
        public ActionResult NewStaff(AccStatsModel staff)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string postData = "&name=" + staff.name +
                        "&u_k=" + staff.u_k;
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/staff/new?" + postData);
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
                        int comVal = msg.CompareTo("Successfully Added.");
                        if (comVal == 0)
                        {
                            return Content("Successfully Added.", "text/plain", Encoding.UTF8);
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