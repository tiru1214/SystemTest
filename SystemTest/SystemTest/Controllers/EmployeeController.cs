using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using SystemTest.Models;

namespace SystemTest.Controllers
{
    public class EmployeeController : ApiController
    {
        
        

        public HttpResponseMessage Verify(LoginDetails ld)
        {
            Employee e = GetEmployeeList().Find(x=> x.username==ld.username && x.password==ld.password);
            return Request.CreateResponse(HttpStatusCode.OK, e);
        }

        public List<Employee> GetEmployeeList()
        {
            List<Employee> le = new List<Employee>()
            {
                new Employee {id=1,username="tiru",password="tiru@123",City="guntur",phoneno="8639865959" },
                new Employee {id=2,username="ramesh",password="ramesh@123",City="hyderabad",phoneno="8639865959" },
                new Employee {id=3,username="",password="ramesh@123",City="vijaywada",phoneno="8639865959" },
            };
           
            return le;
        }

        public HttpResponseMessage SaveEmployeeInfo(Employee emp)
        {
            List<Employee> le = GetEmployeeList();
            Employee e = GetEmployeeList().Find(x => x.id == emp.id);
            e.username = emp.username;
            e.password = emp.password;
            e.phoneno = emp.phoneno;
            e.City = emp.City;
            

            return Request.CreateResponse(HttpStatusCode.OK, e);
        }




    }
}
