using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDeactivation.Data;
using EmployeeDeactivation.Interface;
using EmployeeDeactivation.Models;


namespace EmployeeDeactivation.BusinessLayer
{
    public class AdminDataOperation : IAdminDataOperation
    {

        private readonly EmployeeDeactivationContext _context;
        public AdminDataOperation(EmployeeDeactivationContext context)
        {
            _context = context;
        }


        public List<Teams> RetrieveSponsorDetails()
        {
            List<Teams> teamDetails = new List<Teams>();
            var details = _context.Teams.ToList();
            foreach (var item in details)
            {
                teamDetails.Add(new Teams
                {
                    SponsorGID = item.SponsorGID,
                    TeamName = item.TeamName,
                    SponsorFirstName = item.SponsorFirstName,
                    SponsorLastName = item.SponsorLastName,
                    SponsorEmailID = item.SponsorEmailID,
                    Department = item.Department,
                    ReportingManagerEmail = item.ReportingManagerEmail
                });
            }
            return teamDetails;
        }

        public async Task<bool> AddSponsorData(string teamName, string sponsorFirstName, string sponsorLastName, string sGid, string sponsorEmail, string sDepartment, string rmEmail)
        //review change make parameters as class
        {
            Teams sponsor = new Teams()
            {
                TeamName = teamName,
                SponsorFirstName = sponsorFirstName,
                SponsorLastName = sponsorLastName,
                SponsorGID = sGid,
                SponsorEmailID = sponsorEmail,
                Department = sDepartment,
                ReportingManagerEmail = rmEmail,
                
            };
            var check = _context.Teams.ToList();
            foreach (var i in check)
            {
                if (i.SponsorGID == sGid)
                {
                    _context.Remove(_context.Teams.Single(a => a.SponsorGID == sGid));
                    _context.SaveChanges();
                }
            }

            _context.Add(sponsor);
            var databaseUpdateStatus = await _context.SaveChangesAsync() == 1 ? true : false;
            return databaseUpdateStatus;
        }

    }   
}
