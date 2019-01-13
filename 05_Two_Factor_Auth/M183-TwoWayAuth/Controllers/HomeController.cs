using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace M183_TwoWayAuth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Token()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            var username = Request["username"];
            var password = Request["password"];
            if (username == "admin" && password == "admin")
            {
                var request = (HttpWebRequest)WebRequest.Create("https://rest.nexmo.com/sms/json");
                var secret = "Test_Secret123";
                var postData = "api_key=<YOUR_API_KEY>&api_secret=<YOUR_API_SECRET>&to=<YOUR_PHONE_NUMBER>&from=\"\"NEXMO\"\"&text=\"" + secret + "\""; //TODO Fill in API_Key, API_Secert & to
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                ViewBag.Message = responseString;
            }
            else
            {
                ViewBag.Message = "Wrong Credentials";
            }
            return View("~/Views/Home/Token.cshtml");
        }
        [HttpPost]
        public ActionResult TokenLogin()
        {
            var token = Request["token"];
            if (token == "Test_Secret123")
            {
                return View("~/Views/Home/Success.cshtml");
            }
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}