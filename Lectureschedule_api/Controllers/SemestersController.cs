using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lectureschedule_api.Controllers
{
    [Route("api/[controller]")]
    public class SemestersController : Controller
    {
        List<string> semesters = new List<string>();
        SqlConnection connect = new SqlConnection("Data Source=DESKTOP-201DTNO\\SQLEXPRESS;Initial Catalog=lectureschedule_db;Integrated Security=True");
        SqlCommand command;
        // GET: api/<controller>
        [HttpGet]
        public List<string> Get()
        {
            connect.Open();
            command = new SqlCommand("select semestername from semester", connect);
            command.ExecuteNonQuery();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                semesters.Add(reader["semestername"].ToString());
            }
            connect.Close();
            return semesters;
        }
    }
}
