using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using front3.Models;

namespace front.Controllers
{
    [OutputCache(Duration = 0, NoStore = true)]
    public class dashboardController : Controller
    {
        // GET: dashboard
        public ActionResult studentDashboard(string studentid)
        {
            HomeModel3 studentmodel = new HomeModel3();
            studentmodel.ObligationMod = ObligationInfo(studentid);

            return View("studentDashboard", studentmodel);
        }

        public IEnumerable<loginmodel> ObligationInfo(string studentid)
        {
            IEnumerable<loginmodel> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/sanction/");

            var consumedata = hc.GetAsync("info?studentid=" + studentid);
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<loginmodel>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<loginmodel>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public ActionResult AdminDashboard()
        {
            HomeModel3 adminmodel = new HomeModel3();
            adminmodel.AdmMod = GetAdmDashboard();
            adminmodel.ViolationInfo = ViolationInfo();
            adminmodel.Light = LightSanction();
            adminmodel.Less = LessSanction();
            adminmodel.Serious = SeriousSanction();
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return View("AdminDashboard", adminmodel);
            }
        }

        public ActionResult MarshallList()
        {
            HomeModel3 adminmodel = new HomeModel3();
            adminmodel.Marshalls = GetMarshallList();
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return View("MarshallList", adminmodel);
            }
        }
        public IEnumerable<loginmodel> GetMarshallList()
        {
            IEnumerable<loginmodel> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/marshall/");

            if (Session["Adm"] == null)
            {
                throw new UnauthorizedAccessException("Session expired. Please log in again.");
            }

            var consumedata = hc.GetAsync("list");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<loginmodel>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<loginmodel>();
                ViewBag.Data = "An error has occurred";
            }

            return ec;
        }

        public IEnumerable<loginmodel> GetAdmDashboard()
        {
            IEnumerable<loginmodel> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/admin/");

            //Check if session is expired
            if (Session["Adm"] == null)
                {
                    throw new UnauthorizedAccessException("Session expired. Please log in again.");
                }

            var consumedata = hc.GetAsync("incharge?username=" + Session["Adm"].ToString());
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<loginmodel>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<loginmodel>();
                ViewBag.Data = "An error has occurred";
            }

            return ec;
        }


        public ActionResult SanctionInfo(string studentid)
        {
            HomeModel3 sanction = new HomeModel3();
            sanction.SanctionInfo = SanctionGetInfo(studentid);
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return View("SanctionInfo", sanction);
            }
        }

        public IEnumerable<loginmodel> SanctionGetInfo(string studentid)
        {
            IEnumerable<loginmodel> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/sanction/");

            if (Session["Adm"] == null)
            {
                throw new UnauthorizedAccessException("Session expired. Please log in again.");
            }

            var consumedata = hc.GetAsync("info?studentid=" + studentid);
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<loginmodel>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<loginmodel>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public IEnumerable<loginmodel> ViolationInfo()
        {
            IEnumerable<loginmodel> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/violation/");

            var consumedata = hc.GetAsync("info");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<loginmodel>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<loginmodel>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public IEnumerable<loginmodel> LightSanction()
        {
            IEnumerable<loginmodel> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/light/");

            var consumedata = hc.GetAsync("list");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<loginmodel>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<loginmodel>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public IEnumerable<loginmodel> LessSanction()
        {
            IEnumerable<loginmodel> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/less/");

            var consumedata = hc.GetAsync("list");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<loginmodel>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<loginmodel>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public IEnumerable<loginmodel> SeriousSanction()
        {
            IEnumerable<loginmodel> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/serious/");

            var consumedata = hc.GetAsync("list");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<loginmodel>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<loginmodel>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public ActionResult Logout()
        {
            // Sign out the user
            FormsAuthentication.SignOut();

            // Redirect to login page
            return RedirectToAction("HomePage", "Home");
        }

}
}