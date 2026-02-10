using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace front2.Models
{
    public class SearchMarshall
    {
        public string role { get; set; }
        public string name { get; set; }
    }
    public class SearchStudentModel
    {
        public string student_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string gender { get; set; }
        public string birth_date { get; set; }
    }
    public class HomeModel1
    {
        public IEnumerable<SearchStudentModel> searchStudent { get; set; }
        public IEnumerable<SearchMarshall> searchMarshall { get; set; }
    }
}