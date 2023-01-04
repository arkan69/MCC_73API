using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class RolesController : BaseController<Role,int,RoleRepositories>
    {
        private RoleRepositories _rolerepositories;

        public RolesController(RoleRepositories rolerepositories):base(rolerepositories)
        {
            _rolerepositories = rolerepositories;
        }
    }
}
