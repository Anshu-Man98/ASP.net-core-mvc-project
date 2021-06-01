using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDeactivation.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly UserManager<UserOptions> userManager;

        public RoleController(
            UserManager<UserOptions> userManager
            )
        {
            this.userManager = userManager;
        }

        // GET api/role
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<string>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var user = await userManager.FindByIdAsync(userId);
            var role = await userManager.GetRolesAsync(user);
            return role;
        }

    }
}