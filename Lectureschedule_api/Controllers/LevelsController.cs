using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lectureschedule_api.Models;
using System.Data.SqlClient;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lectureschedule_api.Controllers
{
    [Route("api/[controller]")]
    public class LevelsController : Controller
    {
        List<Levels> levels = new List<Levels>();
        SqlConnection connect = new SqlConnection("Data Source=DESKTOP-201DTNO\\SQLEXPRESS;Initial Catalog=lectureschedule_db;Integrated Security=True");
        SqlCommand command;

        // GET: api/<controller>
        [HttpGet]
        public List<Levels> Get()
        {
            connect.Open();
            command = new SqlCommand("select * from level", connect);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                levels.Add(new Levels { id = int.Parse(reader["levelid"].ToString()), year = int.Parse(reader["levelyear"].ToString()) });
            }
            connect.Close();
            return levels;
        }

        // GET: api/<controller>/5
        [HttpGet("{id}")]
        public List<Levels> Get(int id)
        {
            connect.Open();
            command = new SqlCommand("select * from level where levelid="+id+"", connect);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                levels.Add(new Levels { id = int.Parse(reader["levelid"].ToString()), year = int.Parse(reader["levelyear"].ToString()) });
            }
            connect.Close();
            return levels;
        }

        // POST api/<controller>
        [HttpPost]
        public Dictionary<string, string> Post([FromBody]Dictionary<string,string> data)
        {
            Dictionary<string, string> error1 = new Dictionary<string, string>() { { "warn", "levelyear exists" } };
            Dictionary<string, string> error2 = new Dictionary<string, string>() { { "success", "successfull" } };
            try
            {
                if (data["levelyear"].ToString() != "")
                {
                    connect.Open();
                    command = new SqlCommand("insert into level(levelyear) values(" + int.Parse(data["levelyear"]) + ")", connect);
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
            Dictionary<string, string> error1 = new Dictionary<string, string>() { { "warn", "levelyear exists" } };
            Dictionary<string, string> error2 = new Dictionary<string, string>() { { "success", "successfull" } };
            try
            {
                if (data["levelyear"].ToString() != "")
                {
                    connect.Open();
                    command = new SqlCommand("update level set levelyear=" + int.Parse(data["levelyear"]) + " where levelid=" + id + "", connect);
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
            command = new SqlCommand("delete from level where levelid="+id+"",connect);
            command.ExecuteNonQuery();
            connect.Close();
        }
    }
}
