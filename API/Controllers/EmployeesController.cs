using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeesController : BaseController<Employee,string,EmployeeRepositories>
    {
        private EmployeeRepositories _emrepositories;

        public EmployeesController(EmployeeRepositories emrepositories):base(emrepositories)
        {
            _emrepositories = emrepositories;
        }

        //[Authorize(Roles = "Manager,Admin")]
        [HttpGet]
        [Route("DetailEmployee")]
        [AllowAnonymous]
        public ActionResult GetEmployees()
        {
            var result = _emrepositories.DetailEmployee();
            try
            {
                return result.Count() == 0
                    ? Ok(new { statusCode = 200, message = "Data Not Found!" })
                    : Ok(new { statusCode = 200, message = "Data Found!", data = result });
            }
            catch
            {
                return BadRequest(new { statusCode = 500, message = "Something Wrong!!" });
            }
        }

        
    }
}
