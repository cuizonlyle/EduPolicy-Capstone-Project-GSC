using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ObligationModel
    {
        public string studentid { get; set; }
        public string fullname { get; set; }
        public string date_of_v { get; set; }
        public string time_of_v { get; set; }
        public string off_id { get; set; }
        public string off_def { get; set; }
        public string off_type { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string status { get; set; }
        public string sanct_status { get; set; }
        public string prior_no { get; set; }
        public string sanction_def { get; set; }
        public string assigned_date { get; set; }
        public string assigned_time { get; set; }
    }
}