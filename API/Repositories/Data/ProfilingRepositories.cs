using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class ProfilingRepositories : GeneralRepository<Profiling, string>
    {
        public ProfilingRepositories(MyContext context) : base(context)
        {
        }
    }
}
