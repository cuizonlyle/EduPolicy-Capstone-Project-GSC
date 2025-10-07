using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace front3.Models
{
    public class loginmodel
    {
        public string username { get; set; }
        public string userpassword { get; set; }
        public string uk { get; set; }
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
        public string prior_no { get; set; }
        public string sanction_id { get; set; }
        public string sanction_def { get; set; }
        public string sanct_status { get; set; }
        public string assigned_date { get; set; }
        public string assigned_time { get; set; }
    }

    public class HomeModel3
    {
        public IEnumerable<loginmodel> loginmod { get; set; }
        public IEnumerable<loginmodel> ObligationMod { get; set; }
        public IEnumerable<loginmodel> AdmMod { get; set; }
        public IEnumerable<loginmodel> Marshalls { get; set; }
        public IEnumerable<loginmodel> ViolationInfo { get; set; }
        public IEnumerable<loginmodel> SanctionInfo { get; set; }
        public IEnumerable<loginmodel> Light { get; set; }
        public IEnumerable<loginmodel> Less { get; set; }
        public IEnumerable<loginmodel> Serious { get; set; }
        public IEnumerable<loginmodel> ObligationStat { get; set; }

    }

}