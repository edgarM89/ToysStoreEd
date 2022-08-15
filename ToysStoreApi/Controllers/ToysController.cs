using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using ToysStoreApi.Models;
using Newtonsoft.Json;

namespace ToysStoreApi.Controllers
{
    [ApiController]
    [Route("toy")]
    public class ToysController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ToysController> _logger;
        public ToysController(ILogger<ToysController> logger, IConfiguration config)
        {
            _logger = logger;
            this.configuration = config;
        }

        [HttpGet]
        [Route("getToys")]
        public dynamic getToys()
        {

            var toys = new List<Toy>();
            //to get the connection string             
            
            var connectionstring = configuration["conApp"];
            //build the sqlconnection and execute the sql command
            
                
                //string commandtext = "select name, price, age from toys";

                DataSet ds = new DataSet();
                SqlConnection conn2 = new SqlConnection(connectionstring);
                conn2.Open();
                SqlCommand com = new SqlCommand("sp_getToys", conn2);
                com.CommandTimeout = 100000;
                com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.Add(new SqlParameter("@Buildingid", 1));
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(ds);                               
                //SqlCommand cmd = new SqlCommand(commandtext, conn2);

                //var reader = cmd.ExecuteReader();

                for (var i =0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var toy = new Toy()
                    {
                        Name = ds.Tables[0].Rows[i]["Name"].ToString(),
                        price = (float)(decimal)ds.Tables[0].Rows[i]["price"],
                        age = (int)ds.Tables[0].Rows[i]["age"]
                    };

                    toys.Add(toy);
                }            
                return toys;
        }

        [HttpPost]
        [Route("saveToy")]
        public dynamic saveToy(Toy toy)
        {
            var toys = new List<Toy>();
            //to get the connection string             

            var connectionstring = configuration["conApp"];

            DataSet ds = new DataSet();
            SqlConnection conn2 = new SqlConnection(connectionstring);
            conn2.Open();
            SqlCommand com = new SqlCommand("sp_saveNewToy", conn2);
            com.CommandTimeout = 100000;
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@name", "Edgar"));
            com.Parameters.Add(new SqlParameter("@description", "muñeco"));
            com.Parameters.Add(new SqlParameter("@age", "70"));
            com.Parameters.Add(new SqlParameter("@price", 1000));
            com.Parameters.Add(new SqlParameter("@idCompany", 1));
            com.Parameters.Add(new SqlParameter("@imgtoy", ""));
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            adapter.Fill(ds);            

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var toy2 = new Toy()
                {
                    Name = ds.Tables[0].Rows[i]["Name"].ToString(),
                    price = (float)(decimal)ds.Tables[0].Rows[i]["price"],
                    age = (int)ds.Tables[0].Rows[i]["age"]
                };

                toys.Add(toy2);
            }
            return toys;

        }

        [HttpPost]
        [Route("updateToy")]
        public dynamic updateToy(Toy toy, int idtoy, int idCompany)
        {
            var toys = new List<Toy>();
            //to get the connection string             

            var connectionstring = configuration["conApp"];

            DataSet ds = new DataSet();
            SqlConnection conn2 = new SqlConnection(connectionstring);
            conn2.Open();
            SqlCommand com = new SqlCommand("sp_updateToy", conn2);
            com.CommandTimeout = 100000;
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@name", toy.Name));
            com.Parameters.Add(new SqlParameter("@description", toy.Description));
            com.Parameters.Add(new SqlParameter("@age", toy.age));
            com.Parameters.Add(new SqlParameter("@price", toy.price));
            com.Parameters.Add(new SqlParameter("@idCompany", idCompany));
            com.Parameters.Add(new SqlParameter("@imgtoy", toy.imgtoy));
            com.Parameters.Add(new SqlParameter("@idToy", idtoy));
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            adapter.Fill(ds);

            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var toy2 = new Toy()
                {
                    Name = ds.Tables[0].Rows[i]["Name"].ToString(),
                    price = (float)(decimal)ds.Tables[0].Rows[i]["price"],
                    age = (int)ds.Tables[0].Rows[i]["age"]
                };

                toys.Add(toy2);
            }
            return toys;

        }
    }

}
