using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeDeactivation.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using Newtonsoft.Json;

namespace EmployeeDeactivation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDeactivationContext _context;

        public EmployeesController(EmployeeDeactivationContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }


        [HttpGet]
        [Route("Employees/GetData")]
        public JsonResult GetData()
        {
            List<Teams> teamss = new List<Teams>();
            var itemlist =   _context.Teams.ToList();
            foreach (var item in itemlist)
            {
                teamss.Add(new Teams {
                    SponsorGID = item.SponsorGID,
                    TeamName = item.TeamName,
                });
            }
            
            return Json(teamss);
           
        }
     

        
        public IActionResult Create()
        {

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,gId,date")] Employee employee)
        {
            if (ModelState.IsValid)
            { 
                _context.Add(employee);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Deactivation Form Submitted Successfully";
            }
            return View(employee);
        }

      

    }
}
