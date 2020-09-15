
using Newtonsoft.Json;
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
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginVerify(LoginDetails LD)
        {

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
                
                return RedirectToAction("Userinfo", obj);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)ex.Response;
                    ResponseString = "Some error occured: " + response.StatusCode.ToString();
                }
                else
                {
                    ResponseString = "Some error occured: " + ex.Status.ToString();
                }
            }



            return View();
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
                return RedirectToAction("Userinfo", obj);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)ex.Response;
                    ResponseString = "Some error occured: " + response.StatusCode.ToString();
                }
                else
                {
                    ResponseString = "Some error occured: " + ex.Status.ToString();
                }
            }
            return View();
        }
    }


 
}

