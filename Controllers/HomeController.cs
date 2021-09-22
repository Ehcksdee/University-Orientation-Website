using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using University_Orientation_Website.Models;
using Microsoft.Data.Sqlite;

namespace University_Orientation_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Other_Services()
        {
            return View();
        }

        public IActionResult Induction()
        {
            return View();
        }
        [HttpPost,HttpGet]
        public IActionResult Staff(Staff model)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            //Use DB in project directory.  If it does not exist, create it:
            connectionStringBuilder.DataSource = "./dm.db";
            
            List<Staff> stf = new List<Staff>();
            string where = "";
            if (!string.IsNullOrEmpty(model.name))
            {
                where = " where name like '%" + model.name +"%' ";
            }else if (!string.IsNullOrEmpty(model.school))
            {
                where = " where school like '%" + model.school +"%' ";
            }else if(!string.IsNullOrEmpty(model.faculty))
            {
                where = " where faculty like '%" + model.faculty +"%' ";
            }else if(!string.IsNullOrEmpty(model.discipline))
            {
                where = " where discipline like '%" + model.discipline +"%' ";
            }

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                //Read the data:
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM Staff" + where;

                
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stf.Add(new Staff
                        {
                            id = Convert.ToInt32(reader["id"]),
                            email = reader["email"].ToString(),
                            name = reader["name"].ToString(),
                            school = reader["school"].ToString(),
                            faculty = reader["faculty"].ToString(),
                            discipline = reader["discipline"].ToString(),
                            office = reader["office"].ToString(),
                            phone = reader["phone"].ToString(),
                            positions = reader["positions"].ToString()
                        });
                    }
                }
            }
            ViewData["Staff"] = stf;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
