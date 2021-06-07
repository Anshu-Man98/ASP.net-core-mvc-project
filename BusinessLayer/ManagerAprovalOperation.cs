using EmployeeDeactivation.Data;
using EmployeeDeactivation.Interface;
using EmployeeDeactivation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeDeactivation.BusinessLayer
{
    public class ManagerAprovalOperation: IManagerAprovalOperation
    {
        private readonly EmployeeDeactivationContext _context;
        public ManagerAprovalOperation(EmployeeDeactivationContext context)
        {
            _context = context;
        }

        public void PdfAttachment(string employeeName, string lastWorkingDatee, string gId, string teamName, string sponsorName, string memoryStream)
        {
            byte[] bytes = System.Convert.FromBase64String(memoryStream);
            ManagerAprovalStatus ManagerAprovalStatus = new ManagerAprovalStatus()
            {
                EmployeeName = employeeName,
                GId = gId,
                LastworkingDate = lastWorkingDatee,
                TeamName = teamName,
                SponsorName = sponsorName,
                PdfAttachment = bytes,
                ReportingManagerEmail="email",
                Status="pending"
            };
            _context.Add(ManagerAprovalStatus);
            _context.SaveChanges();

        }

        public List<ManagerAprovalStatus> RetrieveDeactivationDetailss()
        {
            List<ManagerAprovalStatus> deactivationDetails = new List<ManagerAprovalStatus>();
            var infoo = _context.ManagerAprovalStatus.ToList();
            foreach (var item in infoo)
            {

                deactivationDetails.Add(new ManagerAprovalStatus
                {
                    EmployeeName = item.EmployeeName,
                    LastworkingDate = item.LastworkingDate,
                    GId = item.GId,
                    TeamName = item.TeamName,
                    SponsorName = item.SponsorName,
                    PdfAttachment = item.PdfAttachment,
                    ReportingManagerEmail = item.ReportingManagerEmail,
                    Status = item.Status

                });
            }
            return deactivationDetails;
        }




        public byte[] Getpdf(string GId)
        {
            var DDetails = RetrieveDeactivationDetailss();
            foreach (var item in DDetails)
            {
                if (item.GId == GId)
                {
                    byte[] be = item.PdfAttachment;
                    return be;
                }
            }
            byte[] bb = null;
            return bb;
        }

        public List<ManagerAprovalStatus> GetPendingDeactivationWorkflows(string userEmail)
        {
            
            List<ManagerAprovalStatus> pendingDeactivationWorkflows = new List<ManagerAprovalStatus>();
            var allDeactivationWorkfolw = (RetrieveDeactivationDetailss());
            foreach (var item in allDeactivationWorkfolw)
            {
                if (item.Status.ToLower() == "pending" && item.ReportingManagerEmail== userEmail)
                {
                    pendingDeactivationWorkflows.Add(item);
                }

            }
            return pendingDeactivationWorkflows;
        }

    }
}
