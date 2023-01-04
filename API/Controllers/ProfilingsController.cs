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
    public class ProfilingsController : BaseController<Profiling,string,ProfilingRepositories>
    {
        private ProfilingRepositories _proflingrepo;

        public ProfilingsController(ProfilingRepositories profilingRepositories):base(profilingRepositories)
        {
            _proflingrepo = profilingRepositories;
        }
    }
}
