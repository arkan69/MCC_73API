using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class AccountRoleRepositories : GeneralRepository<AccountRole, int>
    {
        private MyContext _context;
        private DbSet<AccountRole> _accountrole;

        public AccountRoleRepositories(MyContext context) : base(context)
        {
        }
    }
}
