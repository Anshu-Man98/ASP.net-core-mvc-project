using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDeactivation.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDeactivation.Controllers
{
    [AllowAnonymous]
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
    }
}