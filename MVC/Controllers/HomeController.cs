using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using front.Models;
using front2.Models;
using System.Net.Http;
using System.Configuration;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Web.Security;
using Newtonsoft.Json;
using System.Diagnostics;

namespace front.Controllers
{
    [OutputCache(Duration = 0, NoStore = true)]
    public class HomeController : Controller
    {
        public async Task<ActionResult> Search_Student(string search, string search_data)
        {
            HomeModel1 mymodel = new HomeModel1();

            // Properly await the async method
            mymodel.searchStudent = await GetStudentDetailsBySearchMethod(search, search_data);
            mymodel.searchMarshall = StaffinCharge();

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return View("Search_Student", mymodel);
            }
        }

        public ActionResult HomePage()
        {
            if (User.IsInRole("Marshall"))
            {
                return RedirectToAction("HomePage", "Home");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("HomePage", "Home");
            }
            else if (User.IsInRole("Student"))
            {
                return RedirectToAction("HomePage", "Home");
            }
            return View();
        }

        public IEnumerable<SearchMarshall> StaffinCharge()
        {
            IEnumerable<SearchMarshall> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/marshall/");

            if (Session["Marshall"] == null)
            {
                throw new UnauthorizedAccessException("Session expired. Please log in again.");
            }

            var consumedata = hc.GetAsync("incharge?u_k=" + Session["Marshall"].ToString());
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<List<SearchMarshall>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<SearchMarshall>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public async Task<ActionResult> Record_Off(string student_id)
        {
            HomeModel mymodel = new HomeModel();
            mymodel.studentList = await GetStudentDetailsbyID(student_id);
            mymodel.loffenseList = loList();
            mymodel.lsoffenseList = lsoList();
            mymodel.soffenseList = soList();
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return View("Record_Off", mymodel);
            }
        }

        public async Task<List<SearchStudentModel1>> GetStudentDetailsbyID(string student_id)
        {
            if (Session["Marshall"] == null)
            {
                throw new UnauthorizedAccessException("Session expired. Please log in again.");
            }

            if (string.IsNullOrEmpty(student_id))
            {
                return new List<SearchStudentModel1>();
            }

            List<SearchStudentModel1> students = new List<SearchStudentModel1>();

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
        {
            { "api_password", ConfigurationManager.AppSettings["GS_API_PASS"] },
            { "search", "studentid" }, // Confirm if 'search' is the correct parameter
            { "data", student_id }     // Confirm if 'data' is the correct parameter
        };

                var content = new FormUrlEncodedContent(values);

                try
                {
                    HttpResponseMessage response = await client.PostAsync("https://apis.golden-success.com/api/xenon/svs/gsc-cebu", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        students = JsonConvert.DeserializeObject<List<SearchStudentModel1>>(responseBody);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching student data: {ex.Message}");
                }
            }

            return students;
        }


        public async Task<List<SearchStudentModel>> GetStudentDetailsBySearchMethod(string search, string search_data)
        {
            if (Session["Marshall"] == null)
            {
                throw new UnauthorizedAccessException("Session expired. Please log in again.");
            }

            if (string.IsNullOrEmpty(search) || string.IsNullOrEmpty(search_data))
            {
                // Return an empty list instead of a view
                return new List<SearchStudentModel>();
            }

            if (search != "lastname" && search != "firstname" && search != "studentid")
            {
                // Return an empty list if the search type is invalid
                return new List<SearchStudentModel>();
            }

            List<SearchStudentModel> students = new List<SearchStudentModel>();

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
        {
            { "api_password", ConfigurationManager.AppSettings["GS_API_PASS"] },
            { "search", search },
            { "data", search_data }
        };

                var content = new FormUrlEncodedContent(values);

                try
                {
                    HttpResponseMessage response = await client.PostAsync("https://apis.golden-success.com/api/xenon/svs/gsc-cebu", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<SearchStudentModel>>(responseBody);
                }
                catch (Exception ex)
                {
                    // Optional: Log the error or handle it differently if needed
                    Console.WriteLine($"Error fetching student data: {ex.Message}");
                }
            }

            return students;
        }


        public IEnumerable<offModels> loList()
        {
            IEnumerable<offModels> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/loffense/");

            var consumedata = hc.GetAsync("list");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<IList<offModels>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<offModels>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public IEnumerable<offModels> lsoList()
        {
            IEnumerable<offModels> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/lsoffense/");

            var consumedata = hc.GetAsync("list");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<IList<offModels>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<offModels>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

        public IEnumerable<offModels> soList()
        {
            IEnumerable<offModels> ec = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(ConfigurationManager.AppSettings["API_Path"] + "api/soffense/");

            var consumedata = hc.GetAsync("list");
            consumedata.Wait();

            var dataread = consumedata.Result;
            if (dataread.IsSuccessStatusCode)
            {
                var results = dataread.Content.ReadAsAsync<IList<offModels>>();
                results.Wait();
                ec = results.Result;
            }
            else
            {
                ec = Enumerable.Empty<offModels>();
                ViewBag.Data = "An error has occured";
            }
            return ec;
        }

    }
}