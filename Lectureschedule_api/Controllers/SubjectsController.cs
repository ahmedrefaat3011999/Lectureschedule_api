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
    public class SubjectsController : Controller
    {
        List<Subjects> subjects = new List<Subjects>();
        SqlConnection connect = new SqlConnection("Data Source=DESKTOP-201DTNO\\SQLEXPRESS;Initial Catalog=lectureschedule_db;Integrated Security=True");
        SqlCommand command;
        // GET: api/<controller>
        [HttpGet]
        public List<Subjects> Get()
        {
            connect.Open();
            command = new SqlCommand("select * from subject", connect);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                subjects.Add(new Subjects {subjectid=int.Parse(reader["subjectid"].ToString()),subjectname=reader["subjectname"].ToString() });
            }
            connect.Close();
            return subjects;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public List<Subjects> Get(int id)
        {
            connect.Open();
            command = new SqlCommand("select * from subject where subjectid="+id+"", connect);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                subjects.Add(new Subjects { subjectid = int.Parse(reader["subjectid"].ToString()), subjectname = reader["subjectname"].ToString() });
            }
            connect.Close();
            return subjects;
        }

        // POST api/<controller>
        [HttpPost]
        public Dictionary<string, string> Post([FromBody]Dictionary<string,string> data)
        {
            Dictionary<string, string> error1 = new Dictionary<string, string>() { { "warn", "subject exists" } };
            Dictionary<string, string> error2 = new Dictionary<string, string>() { { "success", "successfull" } };
            try
            {
                if(data["subjectname"].ToString() != "")
                {
                    connect.Open();
                    command = new SqlCommand("insert into subject(subjectname) values('" + data["subjectname"] + "')", connect);
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
        public Dictionary<string, string> Put(int id, [FromBody]Dictionary<string, string> data)
        {
            Dictionary<string, string> error1 = new Dictionary<string, string>() { { "warn", "subject exists" } };
            Dictionary<string, string> error2 = new Dictionary<string, string>() { { "success", "successfull" } };
            try
            {
                if(data["subjectname"].ToString() != "")
                {
                    connect.Open();
                    command = new SqlCommand("update subject set subjectname = '" + data["subjectname"] + "' where subjectid = " + id + "", connect);
                    command.ExecuteNonQuery();
                    connect.Close();
                }
                return error2;
            }
            catch
            {
                return error1;
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            connect.Open();
            command = new SqlCommand("delete from subject where subjectid = " + id + "", connect);
            command.ExecuteNonQuery();
            connect.Close();
        }
    }
}
