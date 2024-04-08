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
    public class LecturescheduleController : Controller
    {
        SqlConnection connect = new SqlConnection("Data Source=DESKTOP-201DTNO\\SQLEXPRESS;Initial Catalog=lectureschedule_db;Integrated Security=True");
        SqlCommand command;

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Dictionary<string,string>[] data)
        {
            int sizedata = data.Length;
            connect.Open();
            for (int i = 0; i < sizedata; i++)
            {
                command = new SqlCommand("insert into time(timefrom,timeto) values('" + data[i]["timefrom"] + "','" + data[i]["timeto"] + "');", connect);
                command.ExecuteNonQuery();
            }
            connect.Close();
            connect.Open();
            for (int i = 0; i < sizedata; i++)
            {
                command = new SqlCommand("insert into tabledata(teacherid,subjectid,hallid,dayid,timeid) values((select id from teacher where name='"+data[i]["teachername"]+"'),(select subjectid from subject where subjectname='"+data[i]["subjectname"]+"'),(select hallid from hall where hallname='"+data[i]["hallname"]+"'),(select dayid from day where dayname='"+data[i]["dayname"]+"'),(select id from time where timefrom='"+data[i]["timefrom"]+"' and timeto='"+data[i]["timeto"]+"'));", connect);
                command.ExecuteNonQuery();
            }
            connect.Close();
            connect.Open();
            command = new SqlCommand("insert into lectureschedule(levelid,semesterid) values((select levelid from level where levelyear="+int.Parse(data[0]["levelyear"])+"),(select semesterid from semester where semestername='"+data[0]["semestername"]+"'));", connect);
            command.ExecuteNonQuery();
            connect.Close();
            connect.Open();
            for (int i=0; i < sizedata; i++)
            {
                command = new SqlCommand("insert into lecturescheduletotabledata(lecturescheduleid,tabledataid) values((select id from lectureschedule where semesterid=(select semesterid from semester where semestername='"+data[0]["semestername"] +"')),(select id from tabledata where teacherid=(select id from teacher where name='"+data[i]["teachername"]+"')));", connect);
                command.ExecuteNonQuery();
            }
            connect.Close();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
