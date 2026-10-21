using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Recruitment.DTOs;
using Recruitment.Models;

namespace Recruitment.Repos
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return (await dbSet.FindAsync(id))!;
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }

    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(AppDbContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes
                (_configuration["Jwt:Key"]));
            var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var tokenobj = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                signingCredentials: sc,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60)
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenobj);
        }

        public async Task<LoginDTOResponse> Login(LoginDTO LoginDTO)
        {
            var user = await _context.Users.Where(e => e.Email == LoginDTO.Email && e.Password == LoginDTO.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            return new LoginDTOResponse
            {
                Id = user.Id,
                Token = GenerateToken(user),
                Role = user.Role,
            };
        }

        public async Task<string> Register(RegisterDTO RegisterDTO)
        {
            var user = await _context.Users.Where(e => e.Email == RegisterDTO.Email).FirstOrDefaultAsync();
            if (user != null)
            {
                return null;
            }
            var newuser = new User
            {
                Email = RegisterDTO.Email,
                Password = RegisterDTO.Password,
                FullName = RegisterDTO.FullName,
                Role = RegisterDTO.Role
            };
            await _context.Users.AddAsync(newuser);
            return GenerateToken(user);
        }
    }

    public class JobRepository : GenericRepository<Job>, IJobRepository
    {

        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public JobRepository(AppDbContext context, IMapper mapper) : base(context) 
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<Job> ThirdHighest()
        {
            var jobs =  context.Jobs.AsEnumerable().DistinctBy(s => s.Salary).ToList();
            var job = jobs.OrderByDescending(e => e.Salary).Skip(2).FirstOrDefault();
            return job;
        }
        public async Task<JobReadDTOWithCompany> GetWithCompany(int id)
        {
            var existing = await context.Jobs.FindAsync( id);
            var job = mapper.Map<JobReadDTOWithCompany>(existing);
            var excomp = await context.Companies.FindAsync(existing.CompanyId);
            job.Company = mapper.Map<CompanyReadDTO>(excomp);
            return job;
        }
        public async Task<List<Job>> Search(string keyword)
        {
            var jobs = await context.Jobs.Where(e=>e.Title.Contains(keyword)|| e.Description.Contains(keyword)).ToListAsync();
            return jobs;
        }
    }

    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly AppDbContext context;

        public CompanyRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Company> mostJobs()
        {
            var companyId = await context.Jobs.GroupBy(e => e.CompanyId).Select(g=> new {count = g.Count(), id = g.Key}).OrderByDescending(e=>e.count).Select(e=>e.id).FirstOrDefaultAsync();
            return await context.Companies.FindAsync(companyId);
        }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public IGenericRepository<User> Users { get; }
        public IGenericRepository<CandidateProfile> CandidateProfiles { get; }
        public IGenericRepository<Application> Applications { get; }

        public IAuthRepository Auth { get; }

        public IJobRepository Jobs { get; }
        public ICompanyRepository Companies { get; }

        public UnitOfWork(AppDbContext context, IGenericRepository<User> users, IGenericRepository<CandidateProfile> candidateProfiles, IGenericRepository<Application> applications, IAuthRepository auth, IJobRepository jobs, ICompanyRepository companies)
        {
            this.context = context;
            Users = users;
            CandidateProfiles = candidateProfiles;
            Applications = applications;
            Auth = auth;
            Jobs = jobs;
            Companies = companies;
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
