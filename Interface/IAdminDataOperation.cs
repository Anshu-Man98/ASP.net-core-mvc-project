using EmployeeDeactivation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDeactivation.Interface
{
   public interface IAdminDataOperation
    {
        List<Teams> RetrieveSponsorDetails();
        Task<bool> AddSponsorData(string teamName, string sponsorFirstName, string sponsorLastName, string sGid, string sponsorEmail, string sDepartment, string rmEmail);
        Task<bool> DeleteSponsorData(string gid);
        List<DeactivatedEmployeeDetails> RetrieveEmployeeDetails();
        List<DeactivatedEmployeeDetails> Customers();
    }
}
