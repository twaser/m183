using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace M183_SQLInjection.Controllers
{
    public class MainController : Controller
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

        public ActionResult Feedback()
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
            var usernameFromView = Request["username"];
            var passwordFromView = Request["password"];

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Jerome\\Documents\\sql_xss_injection.mdf;Integrated Security=True;Connect Timeout=30"; //TODO Connectionstring anpassen!
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT [Id] ,[username] , [password] FROM [dbo].[User] WHERE [username] = '" + usernameFromView + "' AND [password] = '" + passwordFromView + "'";
            cmd.Connection = con;
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ViewBag.Message = "Return from Database: ";
                while (reader.Read())
                {
                    ViewBag.Message += reader.GetInt32(0) + " " + reader.GetString(2) + " " + reader.GetString(2) + ", ";
                }
            }
            else
            {
                ViewBag.Message = "Failed.";
            }
            return View("~/Views/Main/Index.cshtml");
        }

        [HttpPost]
        public ActionResult FeedbackSubmit()
        {
            var feedbackFromView = Request["feedback"];
            string connetionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Jerome\\Documents\\sql_xss_injection.mdf;Integrated Security=True;Connect Timeout=30"; //TODO Connectionstring anpassen!
            string sqlQuery = "INSERT INTO Feedback ([feedback]) values(@value)";
            SqlDataAdapter adapter = new SqlDataAdapter();
            using (SqlConnection con = new SqlConnection(connetionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@value", feedbackFromView);
                    cmd.ExecuteNonQuery();
                    ViewBag.Message = "Data Added";
                }
            }

            return View("~/Views/Main/Feedback.cshtml");
        }
    }
}