using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class offensemodel
    {
        public string lo_id { get; set; }
        public string lo_def { get; set; }
        public string lso_id { get; set; }
        public string lso_def { get; set; }
        public string so_id { get; set; }
        public string so_def { get; set; }
        public string off_id { get; set; }
        public string sanction_id { get; set; }
        public string studentid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string gender { get; set; }
        public string username { get; set; }
        public string u_k { get; set; }
        public string status { get; set; }
        public string prior_no { get; set; }
    }
}