using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace front.Models
{
    public class offModels
    {
        public string lo_id { get; set; }
        public string lo_def { get; set; }
        public string lso_id { get; set; }
        public string lso_def { get; set; }
        public string so_id { get; set; }
        public string so_def { get; set; }
        public string off_id { get; set; }
        public string username { get; set; }
        public string sanction_id { get; set; }
        public string name { get; set; }
        public string prior_number { get; set; }
        public string status { get; set; }
        public string studentid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string gender { get; set; }
    }
    public class SearchStudentModel1
    {
        public string student_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string gender { get; set; }
        public string birth_date { get; set; }
    }

    public class HomeModel
    {
        public IEnumerable<offModels> loffenseList { get; set; }
        public IEnumerable<offModels> lsoffenseList { get; set; }
        public IEnumerable<offModels> soffenseList { get; set; }
        public IEnumerable<SearchStudentModel1> studentList { get; set; }
        public IEnumerable<offModels> searchAdminDashboard2 { get; set; }
    }
}