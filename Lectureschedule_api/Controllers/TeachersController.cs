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
    public class TeachersController : Controller
    {
        List<Teachers> teachers = new List<Teachers>();
        SqlConnection connect = new SqlConnection("Data Source=DESKTOP-201DTNO\\SQLEXPRESS;Initial Catalog=lectureschedule_db;Integrated Security=True");
        SqlCommand command;
        // GET: api/<controller>
        [HttpGet]
        public List<Teachers> Get()
        {
            connect.Open();
            command = new SqlCommand("select * from teacher",connect);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                teachers.Add(new Teachers {id=int.Parse(reader["id"].ToString()),name=reader["name"].ToString(),email=reader["email"].ToString(),phone=reader["phone"].ToString(),country=reader["country"].ToString(),password=reader["password"].ToString(),numberoflecture=int.Parse(reader["lectureofcount"].ToString())});
            }
            connect.Close();
            return teachers;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public List<Teachers> Get(int id)
        {
            List<Teachers> teacherselected = new List<Teachers>();
            connect.Open();
            command = new SqlCommand("select * from teacher where id="+id+"", connect);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                teacherselected.Add(new Teachers { id = int.Parse(reader["id"].ToString()), name = reader["name"].ToString(), email = reader["email"].ToString(), phone = reader["phone"].ToString(), country = reader["country"].ToString(), password = reader["password"].ToString(), numberoflecture = int.Parse(reader["lectureofcount"].ToString()) });
            }
            connect.Close();
            return teacherselected;
        }

        // POST api/<controller>
        [HttpPost]
        public Dictionary<string,string> Post([FromBody]Dictionary<string,string> data)
        {
            Dictionary<string, string> error1 = new Dictionary<string, string>() { { "warn", "User exists" } };
            Dictionary<string, string> error2 = new Dictionary<string, string>() { { "success", "successfull" } };
            try
            {
                connect.Open();
                command = new SqlCommand("insert into teacher(name,email,phone,country,password,lectureofcount) values('" + data["name"] + "','" + data["email"] + "','" + data["phone"] + "','" + data["country"] + "','" + data["password"] + "'," + int.Parse(data["numberoflecture"]) + ")", connect);
                command.ExecuteNonQuery();
                connect.Close();
                return error2;
            }
            catch (Exception e)
            {
                return error1;
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connect.Open();
            command = new SqlCommand("delete from teacher where id = "+id+"",connect);
            command.ExecuteNonQuery();
            connect.Close();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public Dictionary<string, string> Put(int id, [FromBody]Dictionary<string,string> data)
        {
            Dictionary<string, string> error1 = new Dictionary<string, string>() { { "warn", "Email is used" } };
            Dictionary<string, string> error2 = new Dictionary<string, string>() { { "success", "successfull" } };
            try
            {
                connect.Open();
                command = new SqlCommand("update teacher set name='" + data["name"] + "', email='" + data["email"] + "', phone = '" + data["phone"] + "' ,country='" + data["country"] + "', password='" + data["password"] + "', lectureofcount=" + int.Parse(data["numberoflecture"]) + " where id=" + id + " ", connect);
                command.ExecuteNonQuery();
                connect.Close();
                return error2;
            }
            catch(Exception e)
            {
                return error1;
            }
        }
    }
}
