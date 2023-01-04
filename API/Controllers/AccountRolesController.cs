using API.Base;
using API.Contexts;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AccountRolesController : BaseController<AccountRole,int,AccountRoleRepositories>
    {
        private AccountRoleRepositories _accrolerepositories;

        public AccountRolesController(AccountRoleRepositories accrolerepositories) : base(accrolerepositories)
        {
            _accrolerepositories = accrolerepositories;
        }
    }
}
