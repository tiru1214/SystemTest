
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SystemTest.Models;

namespace SystemTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginVerify(LoginDetails LD)
        {
            //Session["EmployeeList"] = GetEmployee();
            string ResponseString = "";
            HttpWebResponse response = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://localhost:4758//api/Employee/Verify");
                request.Accept = "application/json";
                request.Method = "POST";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var myContent = jss.Serialize(LD);
                var data = Encoding.ASCII.GetBytes(myContent);
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
                ResponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Employee obj = JsonConvert.DeserializeObject<Employee>(ResponseString);
                if (obj == null)
                {
                    ViewBag.Error = "Username/Password mismated";
                    return View("Login");
                }
                else
                {
                    return RedirectToAction("Userinfo", obj);
                }

            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Login");
            }

        }

        public ActionResult Userinfo(Employee emp)
        {
            return View(emp);
        }

        public ActionResult SaveInfo(Employee emp)
        {
            string ResponseString = "";
            HttpWebResponse response = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://localhost:4758//api/Employee/SaveEmployeeInfo");
                request.Accept = "application/json"; //"application/xml";
                request.Method = "POST";


                JavaScriptSerializer jss = new JavaScriptSerializer();
                // serialize into json string
                var myContent = jss.Serialize(emp);

                var data = Encoding.ASCII.GetBytes(myContent);

                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                response = (HttpWebResponse)request.GetResponse();

                ResponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Employee obj = JsonConvert.DeserializeObject<Employee>(ResponseString);
                if (obj == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("Userinfo", obj);
                }

            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Login");
            }

        }

        //public IEnumerable<Employee> GetEmployee()
        //{
           
        //    IEnumerable<Employee> employee = null;

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:4758//api/");
        //        //HTTP GET
        //        var responseTask = client.GetAsync("Employee/GetEmployeeList");
        //        responseTask.Wait();
        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<IList<Employee>>();
        //            readTask.Wait();
        //            employee = readTask.Result;
                 
        //        }
        //        return employee;
        //    }
        //}



    }
}

