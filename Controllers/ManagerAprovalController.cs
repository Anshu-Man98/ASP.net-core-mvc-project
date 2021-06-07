using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeDeactivation.Interface;
using EmployeeDeactivation.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDeactivation.Controllers
{
    public class ManagerAprovalController : Controller
    {
        private readonly IManagerAprovalOperation _managerAprovalOperation;

        public ManagerAprovalController(IManagerAprovalOperation managerAprovalOperation)
        {
            _managerAprovalOperation = managerAprovalOperation;
        }

        public IActionResult ManagerAprovalPage()
        {
        
            return View();
        }


        [HttpPost]
        [Route("ManagerAproval/PdfDoc")]
        public void PdfDoc(string employeeName, string lastWorkingDatee, string gId, string teamName, string sponsorName, string memoryStream)
        {
            _managerAprovalOperation.PdfAttachment(employeeName, lastWorkingDatee, gId, teamName, sponsorName, memoryStream);
        }


        [HttpGet]
        [Route("ManagerAproval/DeactivationDetails")]
        public JsonResult DeactivationDetails()
        {
            var userEmail = "";
            if (User.Identity.IsAuthenticated)
            {
                userEmail = GetUserEmail(User);
            }

            return Json(_managerAprovalOperation.GetPendingDeactivationWorkflows(userEmail));

        }

        [HttpGet]
        [Route("ManagerAproval/PdfDetails")]
        public ActionResult PdfDetails(string GId)
        {
            byte[] cc =_managerAprovalOperation.Getpdf(GId);
            return Json("data:application/pdf;base64," + Convert.ToBase64String(cc));

        }

        private static string GetUserEmail(ClaimsPrincipal User)
        {
            return (User.Identities.FirstOrDefault().Claims.Where(c => c.Type.Equals("preferred_username")).FirstOrDefault().Value);
        }

    }
}