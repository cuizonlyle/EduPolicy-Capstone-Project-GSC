using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace front4.Models
{
    public class AccStatsModel
    {
        public string u_k { get; set; }
        public string name { get; set; }
    }
    public class ChangeStatus
    {
        public IEnumerable<AccStatsModel> Change_Status { get; set; }
    }
}