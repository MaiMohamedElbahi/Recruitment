using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Recruitment.Models;

namespace Recruitment.DTOs
{
    public class UserReadDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class CandidateProfileReadDTO
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public string? ResumeUrl { get; set; }

        public string Skill { get; set; }

        public int YearsOfExperience { get; set; }

        public int UserId { get; set; }
    }

    public class CandidateProfileUpdateDTO
    {

        public string Phone { get; set; }

        public string? ResumeUrl { get; set; }

        public string Skill { get; set; }

        public int YearsOfExperience { get; set; }

        public int UserId { get; set; }
    }

    public class CompanyReadDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? Website { get; set; }

        public string Location { get; set; }

        public int OwnerId { get; set; }
    }
    public class JobReadDTOWithCompany
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public decimal Salary { get; set; }

        public string EmploymentType { get; set; }

        public DateTime PostedDate { get; set; }

        public DateTime Deadline { get; set; }

        public string Status { get; set; }

        public CompanyReadDTO? Company { get; set; }
    }
    public class JobReadDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public decimal Salary { get; set; }

        public string EmploymentType { get; set; }

        public DateTime PostedDate { get; set; }

        public DateTime Deadline { get; set; }

        public string Status { get; set; }

        public int CompanyId { get; set; }
    }

    public class ApplicationReadDTO
    {
        public int Id { get; set; }

        public DateTime ApplicationDate { get; set; }

        public string Status { get; set; }

        public string? CoverLetter { get; set; }

        public int CandidateProfileId { get; set; }

        public int JobId { get; set; }
    }

    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginDTOResponse
    {
        [Required]
        public string Role { get; set; }

        [Required]
        public string Token { get; set; }
        public int Id { get; set; }
    }

    public class RegisterDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^(Candidate|Employer)$")]
        public string Role { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserReadDTO>();
            CreateMap<CandidateProfile, CandidateProfileReadDTO>();
            CreateMap<CandidateProfile, CandidateProfileUpdateDTO>().ReverseMap();
            CreateMap<Company, CompanyReadDTO>();
            CreateMap<Job, JobReadDTO>();
            CreateMap<Job, JobReadDTOWithCompany>();
            CreateMap<Application, ApplicationReadDTO>();

            CreateMap<RegisterDTO, User>();
        }
    }
}
