using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class LoginModel
    {
        public string studentid { get; set; }
        public string username { get; set; }
        public string userpassword { get; set; }
        public string u_k { get; set; }
        public string role { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string name { get; set; }
        public string status { get; set; }
    }
}