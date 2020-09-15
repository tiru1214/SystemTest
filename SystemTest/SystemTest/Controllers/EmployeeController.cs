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
        
        List<Employee> global = new List<Employee>();

        public HttpResponseMessage Verify(LoginDetails ld)
        {
            
            if (global.Count == 0)
            {
                global = new List<Employee>();
                global = GetEmployeeList();
            }
            
            Employee e = global.Find(x=> x.username==ld.username && x.password==ld.password);
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

            Employee e = global.Find(x => x.id == emp.id);

            foreach(Employee e1 in global)
            {
                if (e1.id == emp.id)
                {
                    global[e.id].username = e.username;
                    global[e.id].password = e.password;
                    global[e.id].City = e.City;
                    global[e.id].phoneno = e.phoneno;

                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, e);
        }




    }
}
