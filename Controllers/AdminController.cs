using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDeactivation.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDeactivation.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller 
    {
        private readonly IAdminDataOperation _adminDataOperation;
        public AdminController(IAdminDataOperation adminDataOperation)
        {
            _adminDataOperation = adminDataOperation;
        }

        public IActionResult AdminPage()
        {
            return View();
        }

        public IActionResult AccountDeactivationDatePage()
        {
            return View(_adminDataOperation.Customers());
        }

        [HttpGet]
        [Route("Admin/SponsorDetails")]
        public JsonResult SponsorDetails()
        {
            return Json(_adminDataOperation.RetrieveSponsorDetails());
        }

        [HttpPost]
        [Route("Admin/AddSponsorDetails")]
        public JsonResult AddSponsorDetails(string teamName, string sponsorFirstName, string sponsorLastName, string sGid, string sponsorEmail, string sDepartment, string rmEmail)
        {
            var updateStatus = _adminDataOperation.AddSponsorData(teamName, sponsorFirstName, sponsorLastName, sGid, sponsorEmail, sDepartment, rmEmail);

            return Json(true);
        }

        [HttpPost]
        [Route("Admin/DeleteSponsorDetail")]
        public JsonResult DeleteSponsorDetails(string gid)
        {
            var updateStatus = _adminDataOperation.DeleteSponsorData(gid);

            return Json(true);
        }

        [HttpGet]
        [Route("Admin/EmployeeDetails")]
        public JsonResult EmployeeDetails()
        {
            return Json(_adminDataOperation.RetrieveEmployeeDetails());
        }

    }
}