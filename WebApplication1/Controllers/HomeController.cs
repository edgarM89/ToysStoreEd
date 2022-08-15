using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        static string connection = ConfigurationManager.ConnectionStrings["toysconnection"].ConnectionString;
        static string pathimages = ConfigurationManager.AppSettings["pathimages"].ToString();        
        public ActionResult Index()
        {
            DataSet ds = new DataSet();
            connection = "data source=(localdb)\\Local;initial catalog=ToysStore;MultipleActiveResultSets=True;App=EntityFramework;Trusted_Connection=True;TrustServerCertificate=True";
            SqlConnection connection1 = new SqlConnection(connection);
            try
            {
                connection1.Open();
                SqlCommand com = new SqlCommand("sp_getToys", connection1);
                com.CommandTimeout = 1000;
                com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.AddWithValue("@name", name);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(ds);

                for(var i= 0; i< ds.Tables[0].Rows.Count; i++)
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(pathimages + ds.Tables[0].Rows[i]["img"]);
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                    ds.Tables[0].Rows[i]["img"] = base64ImageRepresentation;
                }

                ViewBag.datatoys = ds.Tables[0];
            }
            catch (Exception e)
            {
                ViewBag.messageError = "-- " + e.Message;
            }

            ViewBag.pathimg = pathimages;
            return View();
        }

        public ActionResult AddToy(HttpPostedFileBase img1, string name, string desc, string age, float price, string company)
        {
            var imgtoy = System.Web.HttpContext.Current.Request.Files["img1"];
            if (!System.IO.Directory.Exists(pathimages))
            {
                System.IO.Directory.CreateDirectory(pathimages); 
            }

            var fileName = Path.GetFileName(imgtoy.FileName);
            imgtoy.SaveAs(Path.Combine(pathimages + fileName));
            imgtoy.InputStream.Close();

            DataSet ds = new DataSet();            
            SqlConnection connection1 = new SqlConnection(connection);
            try
            {
                connection1.Open();
                SqlCommand com = new SqlCommand("sp_saveNewToy", connection1);
                com.CommandTimeout = 1000;
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@name", name);
                com.Parameters.AddWithValue("@description", desc);
                com.Parameters.AddWithValue("@age", age);
                com.Parameters.AddWithValue("@price", price);
                com.Parameters.AddWithValue("@company", company);
                com.Parameters.AddWithValue("@imgtoy", fileName);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(ds);
            }
            catch (Exception e)
            {
                ViewBag.messageError = "-- " + e.Message;
            }

            return Json("");
        }

        public ActionResult updateToy(HttpPostedFileBase img1, string name, string desc, string age, float price, string company, int idT)
        {
            //var imgtoy = System.Web.HttpContext.Current.Request.Files["img1"];
            //if (!System.IO.Directory.Exists("/imagestoys"))
            //{
            //    System.IO.Directory.CreateDirectory("/imagestoys");
            //}

            //var fileName = Path.GetFileName(imgtoy.FileName);
            //imgtoy.SaveAs(Path.Combine("/imagestoys/" + fileName));
            //imgtoy.InputStream.Close();

            DataSet ds = new DataSet();
            connection = "data source=(localdb)\\Local;initial catalog=ToysStore;MultipleActiveResultSets=True;App=EntityFramework;Trusted_Connection=True;TrustServerCertificate=True";
            SqlConnection connection1 = new SqlConnection(connection);
            try
            {
                connection1.Open();
                SqlCommand com = new SqlCommand("sp_updateToy", connection1);
                com.CommandTimeout = 1000;
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@name", name);
                com.Parameters.AddWithValue("@description", desc);
                com.Parameters.AddWithValue("@age", age);
                com.Parameters.AddWithValue("@price", price);
                com.Parameters.AddWithValue("@company", company);
                com.Parameters.AddWithValue("@idToy", idT);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(ds);
            }
            catch (Exception e)
            {
                ViewBag.messageError = "-- " + e.Message;
            }

            return Json("");
        }

        public ActionResult deletetoy(int idT)
        {
            //var imgtoy = System.Web.HttpContext.Current.Request.Files["img1"];
            //if (!System.IO.Directory.Exists("/imagestoys"))
            //{
            //    System.IO.Directory.CreateDirectory("/imagestoys");
            //}

            //var fileName = Path.GetFileName(imgtoy.FileName);
            //imgtoy.SaveAs(Path.Combine("/imagestoys/" + fileName));
            //imgtoy.InputStream.Close();

            DataSet ds = new DataSet();
            connection = "data source=(localdb)\\Local;initial catalog=ToysStore;MultipleActiveResultSets=True;App=EntityFramework;Trusted_Connection=True;TrustServerCertificate=True";
            SqlConnection connection1 = new SqlConnection(connection);
            try
            {
                connection1.Open();
                SqlCommand com = new SqlCommand("sp_deletetoy", connection1);
                com.CommandTimeout = 1000;
                com.CommandType = CommandType.StoredProcedure;
                
                com.Parameters.AddWithValue("@idToy", idT);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(ds);
            }
            catch (Exception e)
            {
                ViewBag.messageError = "-- " + e.Message;
            }

            return Json("");
        }
    }
}