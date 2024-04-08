using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Lectureschedule_api.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lectureschedule_api.Controllers
{
    [Route("api/[controller]")]
    public class HallsController : Controller
    {
        List<Halls> halls = new List<Halls>();
        SqlConnection connect = new SqlConnection("Data Source=DESKTOP-201DTNO\\SQLEXPRESS;Initial Catalog=lectureschedule_db;Integrated Security=True");
        SqlCommand command;

        // GET: api/<controller>
        [HttpGet]
        public List<Halls> Get()
        {
            connect.Open();
            command = new SqlCommand("select * from hall",connect);
            command.ExecuteNonQuery();
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                halls.Add(new Halls { id = int.Parse(dataReader["hallid"].ToString()), name = dataReader["hallname"].ToString() });
            }
            connect.Close();
            return halls;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public List<Halls> Get(int id)
        {
            List<Halls> hallselected = new List<Halls>();
            connect.Open();
            command = new SqlCommand("select * from hall where hallid=" + id + "", connect);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                hallselected.Add(new Halls { id = int.Parse(reader["hallid"].ToString()), name = reader["hallname"].ToString() });
            }
            connect.Close();
            return hallselected;
        }

        // POST api/<controller>
        [HttpPost]
        public Dictionary<string, string> Post([FromBody]Dictionary<string,string> data)
        {
            Dictionary<string, string> error1 = new Dictionary<string, string>() { { "warn", "hallname exists" } };
            Dictionary<string, string> error2 = new Dictionary<string, string>() { { "success", "successfull" } };
            try
            {
                if (data["hallname"].ToString() != "")
                {
                    connect.Open();
                    command = new SqlCommand("insert into hall(hallname) values('" + data["hallname"] + "')", connect);
                    command.ExecuteNonQuery();
                    connect.Close();
                }
                return error2;
            }
            catch(Exception e)
            {
                return error1;
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public Dictionary<string, string> Put(int id, [FromBody]Dictionary<string,string> data)
        {
            Dictionary<string, string> error1 = new Dictionary<string, string>() { { "warn", "hallname exists" } };
            Dictionary<string, string> error2 = new Dictionary<string, string>() { { "success", "successfull" } };
            try
            {
                if (data["hallname"].ToString() != "")
                {
                    connect.Open();
                    command = new SqlCommand("update hall set hallname='" + data["hallname"] + "' where hallid=" + id + " ", connect);
                    command.ExecuteNonQuery();
                    connect.Close();
                }
                return error2;
            }
            catch(Exception e)
            {
                return error1;
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connect.Open();
            command = new SqlCommand("delete from hall where hallid = " + id + "", connect);
            command.ExecuteNonQuery();
            connect.Close();
        }
    }
}
