using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemTest.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string City { get; set; }

        public string phoneno { get; set; }
    }


    public class LoginDetails
    {
        public string username { get; set; }
        public string password { get; set; }
    }

}