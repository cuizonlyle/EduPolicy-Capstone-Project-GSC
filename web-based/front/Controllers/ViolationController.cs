using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using front.Models;

namespace front.Controllers
{
    public class ViolationController : Controller
    {
        //RecordViolation
        [HttpPost]
        public ActionResult AddViolation(offModels s)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string postData = "&studentid=" + s.studentid
                        + "&last_name=" + s.last_name
                        + "&first_name=" + s.first_name
                        + "&middle_name=" + s.middle_name
                        + "&gender=" + s.gender
                        + "&off_id=" + s.off_id
                        + "&u_k=" + Session["Marshall"]
                        + "&prior_no=" + s.prior_number;
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/student/violation_add?" + postData);
                    Byte[] data = Encoding.UTF8.GetBytes(postData);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req.ContentLength = data.Length;

                    Stream stream = req.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();

                    using (res = req.GetResponse())
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                        int comVal = msg.CompareTo("Submitted Successfully");
                        if (comVal == 0)
                        {
                            return Content("Submitted Successfully", "text/plain", Encoding.UTF8);
                        }
                        else
                        {
                            return Content(msg, "text/plain", Encoding.UTF8);
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse res = (HttpWebResponse)ex.Response;
                    Stream receiveStream = res.GetResponseStream();
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))

                    {
                        return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                    }
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                Stream receiveStream = res.GetResponseStream();
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                }
            }
        }

        [HttpPut]
        public ActionResult Put_Violation_Status(offModels changestat)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string putData = "&status=" + changestat.status
                        + "&prior_no=" + changestat.prior_number
                        + "&username=" + Session["Adm"];
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/violation/status?" + putData);
                    Byte[] data = Encoding.UTF8.GetBytes(putData);
                    req.Method = "PUT";
                    req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req.ContentLength = data.Length;

                    Stream stream = req.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();

                    using (res = req.GetResponse())
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                        int comVal = msg.CompareTo("Status Changed.");
                        if (comVal == 0)
                        {
                            return Content("Status Changed.", "text/plain", Encoding.UTF8);
                        }
                        else
                        {
                            return Content(msg, "text/plain", Encoding.UTF8);
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse res = (HttpWebResponse)ex.Response;
                    Stream receiveStream = res.GetResponseStream();
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))

                    {
                        return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                    }
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                Stream receiveStream = res.GetResponseStream();
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                }
            }
        }

        [HttpPut]
        public ActionResult Put_Sanction_Status(offModels changestat)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string putData = "&prior_no=" + changestat.prior_number
                        + "&username=" + Session["Adm"];
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/sanction/status?" + putData);
                    Byte[] data = Encoding.UTF8.GetBytes(putData);
                    req.Method = "PUT";
                    req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req.ContentLength = data.Length;

                    Stream stream = req.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();

                    using (res = req.GetResponse())
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                        int comVal = msg.CompareTo("Status Changed.");
                        if (comVal == 0)
                        {
                            return Content("Status Changed.", "text/plain", Encoding.UTF8);
                        }
                        else
                        {
                            return Content(msg, "text/plain", Encoding.UTF8);
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse res = (HttpWebResponse)ex.Response;
                    Stream receiveStream = res.GetResponseStream();
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))

                    {
                        return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                    }
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                Stream receiveStream = res.GetResponseStream();
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                }
            }
        }

        //Assign Task
        [HttpPost]
        public ActionResult AddSanction(offModels sanction)
        {
            try
            {
                string msg;
                try
                {
                    WebRequest req;
                    WebResponse res;
                    string postData = "&sanction_id=" + sanction.sanction_id
                        + "&prior_no=" + sanction.prior_number
                        + "&username=" + Session["Adm"]
                        + "&off_id=" + sanction.off_id;
                    req = WebRequest.Create(ConfigurationManager.AppSettings["API_Path"] + "api/student/sanction_add?" + postData);
                    Byte[] data = Encoding.UTF8.GetBytes(postData);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    req.ContentLength = data.Length;

                    Stream stream = req.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();

                    using (res = req.GetResponse())
                    using (var reader = new StreamReader(res.GetResponseStream()))
                    {
                        msg = reader.ReadToEnd();
                        int comVal = msg.CompareTo("Assigned Successfully.");
                        if (comVal == 0)
                        {
                            return Content("Assigned Successfully.", "text/plain", Encoding.UTF8);
                        }
                        else
                        {
                            return Content(msg, "text/plain", Encoding.UTF8);
                        }
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse res = (HttpWebResponse)ex.Response;
                    Stream receiveStream = res.GetResponseStream();
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))

                    {
                        return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                    }
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                Stream receiveStream = res.GetResponseStream();
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    return Content(readStream.ReadToEnd(), "text/plain", Encoding.UTF8);
                }
            }
        }

    }
}