using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Manager")]
    public class EducationsController : BaseController<Education,int,EducationRepositories>
    {
        private EducationRepositories _education;

        public EducationsController(EducationRepositories education):base(education)
        {
            _education = education;
        }
    }
}
