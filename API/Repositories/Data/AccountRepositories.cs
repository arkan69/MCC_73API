using API.Contexts;
using API.Handlers;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Transactions;

namespace API.Repositories.Data
{
    public class AccountRepositories : GeneralRepository<Account, string>
    {
        private MyContext _context;
        private DbSet<Account> _account;
        private DbSet<Employee> _employees;
        private DbSet<Profiling> _profile;
        private DbSet<Education> _educations;
        private DbSet<University> _universities;


        public AccountRepositories(MyContext context):base(context)
        {
            _context = context;
            _account = context.Set<Account>();
            _employees = context.Set<Employee>();
            _profile = context.Set<Profiling>();
            _educations = context.Set<Education>();
            _universities = context.Set<University>();

        }

        public int Register(RegisterVM registerVM)
        {
            var result = 0;
            //registerVM.NIK = GenerateNIK();
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                //input university
                University university = new University()
                {
                    Name = registerVM.UniversityName,
                };
                _universities.Add(university);
                result = _context.SaveChanges();

                //input education
                Education education = new Education()
                {
                    UniversityId = university.Id,
                    GPA = registerVM.GPA,
                    Degree = registerVM.Degree,
                };
                _educations.Add(education);
                result = _context.SaveChanges();

                //input employee
                Employee employee = new Employee()
                {
                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Phone = registerVM.Phone,
                    Gender = registerVM.Gender,
                    BirthDate = registerVM.BirthDate,
                    Salary = registerVM.Salary,
                    Email = registerVM.Email,
                };
                _employees.Add(employee);
                result = _context.SaveChanges();

                //input account
                Account account = new Account()
                {
                    NIK = employee.NIK,
                    Password = Hashing.HashPassword(registerVM.Password),
                };
                _account.Add(account);
                result = _context.SaveChanges();

                //input profiling
                Profiling profiling = new Profiling()
                {
                    NIK = account.NIK,
                    EducationId = education.Id
                };
                _profile.Add(profiling);
                result = _context.SaveChanges();

                //Accont Role
                AccountRole accountRole = new AccountRole()
                {
                    AccountNIK = account.NIK,
                    RoleId = 1
                };
                _context.AccountRoles.Add(accountRole);
                _context.SaveChanges();


                transaction.Commit();
            }
            catch
            {
                result = 0;
                transaction.Rollback();
            }
            return result;
        }

        public int Login(LoginVM loginVM)
        {
            var result = _employees.Join(_account, em => em.NIK, acc => acc.NIK, (em, acc) => new LoginVM
            {
                Email = em.Email,
                Password = acc.Password
            }).SingleOrDefault(e => e.Email == loginVM.Email);

            if (result == null)
            {
                return 0;
            }
            else if (Hashing.ValidatePassword(loginVM.Password, result.Password) != true)
            {
                return 1;
            }
            return 2;
        }

        public int CheckRegister(RegisterVM register)
        {
            var result = _employees.Select(a => new RegisterVM
            {
                Email = a.Email
            }).Where(e => e.Email == register.Email);

            var result1 = _employees.Select(b => new RegisterVM
            {
                Phone = b.Phone
            }).Where(p => p.Phone == register.Phone);

            if (result.Count() > 0)
            {
                return 2;
            }
            else if (result1.Count() > 0)
            {
                return 3;
            }
            return 1;
        }

        public int CheckAccount(LoginVM login)
        {
            var result = _account.Join(_context.Employees, acc => acc.NIK, em => em.NIK, (acc, em) =>
            new LoginVM
            {
                Email = em.Email,
                Password = acc.Password
            }).SingleOrDefault(e => e.Email == login.Email);

            if (result == null)
            {
                return 0;//Incorret Email.
            }
            if (Hashing.ValidatePassword(login.Password, result.Password) != true)
            {
                return 1;//Incorrect pasword
            }
            return 2;
        }
        
        private string GenerateNIK()
        {
            var empCount = _context.Employees.OrderByDescending(e => e.NIK).FirstOrDefault();

            if (empCount == null)
            {
                return "x0001";
            }
            string NIK = empCount.NIK.Substring(1, 4);
            return Convert.ToString("x" + Convert.ToInt32(NIK) + 1);
        }

        public List<string> UserRoles(string email)
        {
            //{"Employee","Manager","Admin"}
            //List<string> result = new List<string>();

            List<string> result = (from em in _employees
                                  join a in _account on em.NIK equals a.NIK
                                  join ar in _context.AccountRoles on a.NIK equals ar.AccountNIK
                                  join r in _context.Roles on ar.RoleId equals r.Id
                                  where em.Email == email
                                  select r.Name).ToList();
            return result;
        }
    }
}
