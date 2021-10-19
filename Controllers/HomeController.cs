using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using University_Orientation_Website.Models;
// using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

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
        public IActionResult Staff(Staff model,int page = 1, int pagesize = 15)
        {
            string key="1", val="1";
            if (!string.IsNullOrEmpty(model.name))
            {
                key = "name";
                val = model.name;
            }else if (!string.IsNullOrEmpty(model.school))
            {
                key = "school";
                val = model.school;
            }else if(!string.IsNullOrEmpty(model.faculty))
            {
                key = "faculty";
                val = model.faculty;
            }else if(!string.IsNullOrEmpty(model.discipline))
            {
                key = "discipline";
                val = model.discipline;
            }
            List<Staff> stfs;
            using (var context = new SqliteDbContext())
            {
                //Read the data:
                string selectCmd = "SELECT * FROM Staff WHERE " + key + " LIKE '%" + val + "%'";
                if(key=="1") selectCmd = "SELECT * FROM Staff  ORDER BY id DESC";
                stfs = context.Staff.FromSqlRaw(selectCmd).ToList();

            }
            return View(stfs.ToPagedList(page, pagesize));
            //ViewData["Staff"] = stfs;
            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Createstf(Staff stf)
        {
            using (var context = new SqliteDbContext())
            {
                context.Staff.Update(stf);
                context.SaveChanges();
            }
            return CreatedAtAction(nameof(Createstf), new { id = stf.id }, stf);
        }
        public IActionResult Staffedit(int page = 1, int pagesize = 15)
        {
            List<Staff> stfs;
            using (var context = new SqliteDbContext())
            {
                string selectCmd = "SELECT * FROM Staff ORDER BY id DESC";
                stfs = context.Staff.FromSqlRaw(selectCmd).ToList();
            }
            return View(stfs.ToPagedList(page, pagesize));
        }
        public IActionResult Staffdel(int sid)
        {
            using (var context = new SqliteDbContext())
            {
                context.Remove(context.Staff.Single(a => a.id == sid));
                context.SaveChanges();
            }
            return Ok();
        }
    }
}
