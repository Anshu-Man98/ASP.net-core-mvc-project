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


namespace EmployeeDeactivation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDeactivationContext _context;

        public EmployeesController(EmployeeDeactivationContext context)
        {
            _context = context;
        }

        //// GET: Employees
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Employee.ToListAsync());
        //}

        [HttpGet]
        [Route("Employees/GetData")]
        public JsonResult GetData()
        {
            List<Teams> teamDetails = new List<Teams>();
            var itemlist =   _context.Teams.ToList();
            foreach (var item in itemlist)
            {
                teamDetails.Add(new Teams {
                    SponsorGID = item.SponsorGID,
                    TeamName = item.TeamName,
                    SponsorFirstName=item.SponsorFirstName,
                    SponsorLastName=item.SponsorLastName,
                    SponsorEmailD=item.SponsorEmailD,
                    Department=item.Department
                });
            }
            return Json(teamDetails);         
        }   
        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname,Lastname,Email,GId,Date")] Employee employee)
        {
            if (ModelState.IsValid)
            { 
                _context.Add(employee);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Deactivation Form has been Submitted Successfully";
            }
            return View(employee);
        }
    }
}
