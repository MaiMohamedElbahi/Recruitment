using Recruitment.DTOs;
using Recruitment.Models;

namespace Recruitment.Repos
{
        public interface IGenericRepository<T> where T : class
        {
            Task<List<T>> GetAllAsync();
            Task<T> GetByIdAsync(int id);
            Task AddAsync(T entity);
            void Update(T entity);
            void Delete(T entity);
        }

        public interface IAuthRepository
        {
        Task<LoginDTOResponse> Login(LoginDTO LoginDTO);
        Task<string> Register(RegisterDTO RegisterDTO);
        }

        public interface IJobRepository : IGenericRepository<Job>
        {
         Task<Job> ThirdHighest();
        Task<JobReadDTOWithCompany> GetWithCompany(int id);
        Task<List<Job>> Search(string keyword);
        }

        public interface ICompanyRepository : IGenericRepository<Company>
        {
        Task<Company> mostJobs();
        }

        public interface IUnitOfWork
        {
            IGenericRepository<User> Users { get; }
            IGenericRepository<CandidateProfile> CandidateProfiles { get; }
            IGenericRepository<Application> Applications { get; }

            IAuthRepository Auth { get; }

            IJobRepository Jobs { get; }
            ICompanyRepository Companies { get; }

            Task<int> SaveAsync();
        }
    
}
