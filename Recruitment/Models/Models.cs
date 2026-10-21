using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Recruitment.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^(Candidate|Employer|Admin)$")]
        public string Role { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public CandidateProfile CandidateProfile { get; set; }

        public List<Company> Companies { get; set; } = new List<Company>();
    }

    public class CandidateProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public string? ResumeUrl { get; set; }

        [Required]
        [MaxLength(500)]
        public string Skill { get; set; }

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Application> Applications { get; set; } = new List<Application>();
    }

    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public string? Website { get; set; }

        [Required]
        public string Location { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public List<Job> Jobs { get; set; } = new List<Job>();
    }

    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal Salary { get; set; }

        [Required]
        [RegularExpression("^(FullTime|PartTime|Contract|Internship)$")]
        public string EmploymentType { get; set; }

        [Required]
        public DateTime PostedDate { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        [RegularExpression("^(Open|Closed)$")]
        public string Status { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public List<Application> Applications { get; set; } = new List<Application>();
    }

    public class Application
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }

        [Required]
        [RegularExpression("^(Pending|UnderReview|Accepted|Rejected)$")]
        public string Status { get; set; }

        [MaxLength(2000)]
        public string? CoverLetter { get; set; }

        public int CandidateProfileId { get; set; }

        public CandidateProfile CandidateProfile { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CandidateProfile> CandidateProfiles { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.CandidateProfile)
                .WithOne(c => c.User)
                .HasForeignKey<CandidateProfile>(c => c.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Companies)
                .WithOne(c => c.Owner)
                .HasForeignKey(c => c.OwnerId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Jobs)
                .WithOne(j => j.Company)
                .HasForeignKey(j => j.CompanyId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CandidateProfile>()
                .HasMany(c => c.Applications)
                .WithOne(a => a.CandidateProfile)
                .HasForeignKey(a => a.CandidateProfileId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Job>()
                .HasMany(j => j.Applications)
                .WithOne(a => a.Job)
                .HasForeignKey(a => a.JobId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "Admin User", Email = "admin@example.com", Password = "Admin123!", Role = "Admin", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 2, FullName = "Ahmed Employer", Email = "ahmed@example.com", Password = "Employer123!", Role = "Employer", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 3, FullName = "Sara Employer", Email = "sara@example.com", Password = "Employer123!", Role = "Employer", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 4, FullName = "Omar Candidate", Email = "omar@example.com", Password = "Candidate123!", Role = "Candidate", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 5, FullName = "Mona Candidate", Email = "mona@example.com", Password = "Candidate123!", Role = "Candidate", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 6, FullName = "Youssef Candidate", Email = "youssef@example.com", Password = "Candidate123!", Role = "Candidate", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 7, FullName = "Nour Employer", Email = "nour@example.com", Password = "Employer123!", Role = "Employer", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 8, FullName = "Laila Candidate", Email = "laila@example.com", Password = "Candidate123!", Role = "Candidate", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 9, FullName = "Karim Employer", Email = "karim@example.com", Password = "Employer123!", Role = "Employer", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 10, FullName = "Hana Candidate", Email = "hana@example.com", Password = "Candidate123!", Role = "Candidate", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 11, FullName = "Ali Employer", Email = "ali@example.com", Password = "Employer123!", Role = "Employer", CreatedAt = new DateTime(2026, 1, 1) },
                new User { Id = 12, FullName = "Salma Candidate", Email = "salma@example.com", Password = "Candidate123!", Role = "Candidate", CreatedAt = new DateTime(2026, 1, 1) }
            );

            modelBuilder.Entity<CandidateProfile>().HasData(
                new CandidateProfile { Id = 1, UserId = 4, Phone = "01012345678", Skill = "C#, ASP.NET Core, SQL", YearsOfExperience = 2 },
                new CandidateProfile { Id = 2, UserId = 5, Phone = "01023456789", Skill = "React, JavaScript, CSS", YearsOfExperience = 1 },
                new CandidateProfile { Id = 3, UserId = 6, Phone = "01034567890", Skill = "Python, Machine Learning", YearsOfExperience = 3 },
                new CandidateProfile { Id = 4, UserId = 8, Phone = "01045678901", Skill = "UI/UX, Figma, Graphic Design", YearsOfExperience = 1 },
                new CandidateProfile { Id = 5, UserId = 10, Phone = "01056789012", Skill = "Java, Spring Boot, SQL", YearsOfExperience = 4 },
                new CandidateProfile { Id = 6, UserId = 12, Phone = "01067890123", Skill = "C++, Algorithms, Problem Solving", YearsOfExperience = 2 }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "TechNova", Description = "Software development and IT solutions.", Location = "Cairo, Egypt", Website = "https://technova.example.com", OwnerId = 2 },
                new Company { Id = 2, Name = "Future Systems", Description = "Enterprise software and digital transformation.", Location = "Giza, Egypt", Website = "https://futuresystems.example.com", OwnerId = 2 },
                new Company { Id = 3, Name = "Nile Digital", Description = "Web and mobile application development.", Location = "Alexandria, Egypt", Website = "https://niledigital.example.com", OwnerId = 3 },
                new Company { Id = 4, Name = "Smart Solutions", Description = "Cloud computing and IT consulting.", Location = "Cairo, Egypt", Website = "https://smartsolutions.example.com", OwnerId = 3 },
                new Company { Id = 5, Name = "DataSphere", Description = "Data analytics and artificial intelligence.", Location = "Giza, Egypt", Website = "https://datasphere.example.com", OwnerId = 7 },
                new Company { Id = 6, Name = "CodeCraft", Description = "Custom software and application engineering.", Location = "Mansoura, Egypt", Website = "https://codecraft.example.com", OwnerId = 7 },
                new Company { Id = 7, Name = "CloudCore", Description = "Cloud infrastructure and cybersecurity.", Location = "Cairo, Egypt", Website = "https://cloudcore.example.com", OwnerId = 9 },
                new Company { Id = 8, Name = "PixelWorks", Description = "Creative design and digital products.", Location = "Alexandria, Egypt", Website = "https://pixelworks.example.com", OwnerId = 9 },
                new Company { Id = 9, Name = "NextGen Robotics", Description = "Robotics, embedded systems and automation.", Location = "Cairo, Egypt", Website = "https://nextgenrobotics.example.com", OwnerId = 11 },
                new Company { Id = 10, Name = "BrightPath", Description = "Technology training and consulting.", Location = "Tanta, Egypt", Website = "https://brightpath.example.com", OwnerId = 11 }
            );

            modelBuilder.Entity<Job>().HasData(
    new Job { Id = 1, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Cairo, Egypt", Salary = 10500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 30), Deadline = new DateTime(2027, 1, 1), Status = "Open", CompanyId = 1 },
    new Job { Id = 2, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Cairo, Egypt", Salary = 13000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 29), Deadline = new DateTime(2027, 1, 2), Status = "Open", CompanyId = 1 },
    new Job { Id = 3, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Cairo, Egypt", Salary = 15500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 28), Deadline = new DateTime(2027, 1, 3), Status = "Open", CompanyId = 1 },
    new Job { Id = 4, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Cairo, Egypt", Salary = 18000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 27), Deadline = new DateTime(2027, 1, 4), Status = "Open", CompanyId = 1 },

    new Job { Id = 5, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Giza, Egypt", Salary = 20500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 26), Deadline = new DateTime(2027, 1, 5), Status = "Closed", CompanyId = 2 },
    new Job { Id = 6, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Giza, Egypt", Salary = 23000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 25), Deadline = new DateTime(2027, 1, 6), Status = "Open", CompanyId = 2 },
    new Job { Id = 7, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Giza, Egypt", Salary = 25500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 24), Deadline = new DateTime(2027, 1, 7), Status = "Open", CompanyId = 2 },
    new Job { Id = 8, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Giza, Egypt", Salary = 28000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 23), Deadline = new DateTime(2027, 1, 8), Status = "Open", CompanyId = 2 },
    new Job { Id = 9, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Giza, Egypt", Salary = 30500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 22), Deadline = new DateTime(2027, 1, 9), Status = "Open", CompanyId = 2 },
    new Job { Id = 10, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Giza, Egypt", Salary = 8000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 21), Deadline = new DateTime(2027, 1, 10), Status = "Closed", CompanyId = 2 },

    new Job { Id = 11, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Cairo, Egypt", Salary = 10500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 20), Deadline = new DateTime(2027, 1, 11), Status = "Open", CompanyId = 3 },
    new Job { Id = 12, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Cairo, Egypt", Salary = 13000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 19), Deadline = new DateTime(2027, 1, 12), Status = "Open", CompanyId = 3 },
    new Job { Id = 13, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Cairo, Egypt", Salary = 15500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 18), Deadline = new DateTime(2027, 1, 13), Status = "Open", CompanyId = 3 },
    new Job { Id = 14, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Cairo, Egypt", Salary = 18000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 17), Deadline = new DateTime(2027, 1, 14), Status = "Open", CompanyId = 3 },
    new Job { Id = 15, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Cairo, Egypt", Salary = 20500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 16), Deadline = new DateTime(2027, 1, 15), Status = "Closed", CompanyId = 3 },
    new Job { Id = 16, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Cairo, Egypt", Salary = 23000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 15), Deadline = new DateTime(2027, 1, 1), Status = "Open", CompanyId = 3 },
    new Job { Id = 17, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Cairo, Egypt", Salary = 25500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 14), Deadline = new DateTime(2027, 1, 2), Status = "Open", CompanyId = 3 },
    new Job { Id = 18, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Cairo, Egypt", Salary = 28000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 13), Deadline = new DateTime(2027, 1, 3), Status = "Open", CompanyId = 3 },

    new Job { Id = 19, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Giza, Egypt", Salary = 30500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 12), Deadline = new DateTime(2027, 1, 4), Status = "Open", CompanyId = 4 },
    new Job { Id = 20, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Giza, Egypt", Salary = 8000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 11), Deadline = new DateTime(2027, 1, 5), Status = "Closed", CompanyId = 4 },
    new Job { Id = 21, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Giza, Egypt", Salary = 10500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 10), Deadline = new DateTime(2027, 1, 6), Status = "Open", CompanyId = 4 },
    new Job { Id = 22, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Giza, Egypt", Salary = 13000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 9), Deadline = new DateTime(2027, 1, 7), Status = "Open", CompanyId = 4 },
    new Job { Id = 23, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Giza, Egypt", Salary = 15500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 8), Deadline = new DateTime(2027, 1, 8), Status = "Open", CompanyId = 4 },
    new Job { Id = 24, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Giza, Egypt", Salary = 18000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 7), Deadline = new DateTime(2027, 1, 9), Status = "Open", CompanyId = 4 },
    new Job { Id = 25, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Giza, Egypt", Salary = 20500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 6), Deadline = new DateTime(2027, 1, 10), Status = "Closed", CompanyId = 4 },
    new Job { Id = 26, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Giza, Egypt", Salary = 23000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 5), Deadline = new DateTime(2027, 1, 11), Status = "Open", CompanyId = 4 },
    new Job { Id = 27, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Giza, Egypt", Salary = 25500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 4), Deadline = new DateTime(2027, 1, 12), Status = "Open", CompanyId = 4 },
    new Job { Id = 28, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Giza, Egypt", Salary = 28000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 3), Deadline = new DateTime(2027, 1, 13), Status = "Open", CompanyId = 4 },

    new Job { Id = 29, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Cairo, Egypt", Salary = 30500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 2), Deadline = new DateTime(2027, 1, 14), Status = "Open", CompanyId = 5 },
    new Job { Id = 30, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Cairo, Egypt", Salary = 8000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 1), Deadline = new DateTime(2027, 1, 15), Status = "Closed", CompanyId = 5 },
    new Job { Id = 31, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Cairo, Egypt", Salary = 10500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 30), Deadline = new DateTime(2027, 1, 1), Status = "Open", CompanyId = 5 },
    new Job { Id = 32, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Cairo, Egypt", Salary = 13000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 29), Deadline = new DateTime(2027, 1, 2), Status = "Open", CompanyId = 5 },
    new Job { Id = 33, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Cairo, Egypt", Salary = 15500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 28), Deadline = new DateTime(2027, 1, 3), Status = "Open", CompanyId = 5 },
    new Job { Id = 34, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Cairo, Egypt", Salary = 18000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 27), Deadline = new DateTime(2027, 1, 4), Status = "Open", CompanyId = 5 },
    new Job { Id = 35, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Cairo, Egypt", Salary = 20500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 26), Deadline = new DateTime(2027, 1, 5), Status = "Closed", CompanyId = 5 },
    new Job { Id = 36, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Cairo, Egypt", Salary = 23000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 25), Deadline = new DateTime(2027, 1, 6), Status = "Open", CompanyId = 5 },
    new Job { Id = 37, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Cairo, Egypt", Salary = 25500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 24), Deadline = new DateTime(2027, 1, 7), Status = "Open", CompanyId = 5 },
    new Job { Id = 38, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Cairo, Egypt", Salary = 28000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 23), Deadline = new DateTime(2027, 1, 8), Status = "Open", CompanyId = 5 },
    new Job { Id = 39, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Cairo, Egypt", Salary = 30500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 22), Deadline = new DateTime(2027, 1, 9), Status = "Open", CompanyId = 5 },
    new Job { Id = 40, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Cairo, Egypt", Salary = 8000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 21), Deadline = new DateTime(2027, 1, 10), Status = "Closed", CompanyId = 5 },

    new Job { Id = 41, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Giza, Egypt", Salary = 10500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 20), Deadline = new DateTime(2027, 1, 11), Status = "Open", CompanyId = 6 },
    new Job { Id = 42, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Giza, Egypt", Salary = 13000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 19), Deadline = new DateTime(2027, 1, 12), Status = "Open", CompanyId = 6 },
    new Job { Id = 43, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Giza, Egypt", Salary = 15500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 18), Deadline = new DateTime(2027, 1, 13), Status = "Open", CompanyId = 6 },
    new Job { Id = 44, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Giza, Egypt", Salary = 18000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 17), Deadline = new DateTime(2027, 1, 14), Status = "Open", CompanyId = 6 },
    new Job { Id = 45, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Giza, Egypt", Salary = 20500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 16), Deadline = new DateTime(2027, 1, 15), Status = "Closed", CompanyId = 6 },
    new Job { Id = 46, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Giza, Egypt", Salary = 23000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 15), Deadline = new DateTime(2027, 1, 1), Status = "Open", CompanyId = 6 },
    new Job { Id = 47, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Giza, Egypt", Salary = 25500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 14), Deadline = new DateTime(2027, 1, 2), Status = "Open", CompanyId = 6 },
    new Job { Id = 48, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Giza, Egypt", Salary = 28000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 13), Deadline = new DateTime(2027, 1, 3), Status = "Open", CompanyId = 6 },
    new Job { Id = 49, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Giza, Egypt", Salary = 30500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 12), Deadline = new DateTime(2027, 1, 4), Status = "Open", CompanyId = 6 },
    new Job { Id = 50, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Giza, Egypt", Salary = 8000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 11), Deadline = new DateTime(2027, 1, 5), Status = "Closed", CompanyId = 6 },
    new Job { Id = 51, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Giza, Egypt", Salary = 10500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 10), Deadline = new DateTime(2027, 1, 6), Status = "Open", CompanyId = 6 },
    new Job { Id = 52, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Giza, Egypt", Salary = 13000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 9), Deadline = new DateTime(2027, 1, 7), Status = "Open", CompanyId = 6 },
    new Job { Id = 53, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Giza, Egypt", Salary = 15500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 8), Deadline = new DateTime(2027, 1, 8), Status = "Open", CompanyId = 6 },
    new Job { Id = 54, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Giza, Egypt", Salary = 18000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 7), Deadline = new DateTime(2027, 1, 9), Status = "Open", CompanyId = 6 },

    new Job { Id = 55, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Cairo, Egypt", Salary = 20500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 6), Deadline = new DateTime(2027, 1, 10), Status = "Closed", CompanyId = 7 },
    new Job { Id = 56, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Cairo, Egypt", Salary = 23000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 5), Deadline = new DateTime(2027, 1, 11), Status = "Open", CompanyId = 7 },
    new Job { Id = 57, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Cairo, Egypt", Salary = 25500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 4), Deadline = new DateTime(2027, 1, 12), Status = "Open", CompanyId = 7 },
    new Job { Id = 58, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Cairo, Egypt", Salary = 28000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 3), Deadline = new DateTime(2027, 1, 13), Status = "Open", CompanyId = 7 },
    new Job { Id = 59, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Cairo, Egypt", Salary = 30500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 2), Deadline = new DateTime(2027, 1, 14), Status = "Open", CompanyId = 7 },
    new Job { Id = 60, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Cairo, Egypt", Salary = 8000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 1), Deadline = new DateTime(2027, 1, 15), Status = "Closed", CompanyId = 7 },
    new Job { Id = 61, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Cairo, Egypt", Salary = 10500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 30), Deadline = new DateTime(2027, 1, 1), Status = "Open", CompanyId = 7 },
    new Job { Id = 62, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Cairo, Egypt", Salary = 13000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 29), Deadline = new DateTime(2027, 1, 2), Status = "Open", CompanyId = 7 },
    new Job { Id = 63, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Cairo, Egypt", Salary = 15500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 28), Deadline = new DateTime(2027, 1, 3), Status = "Open", CompanyId = 7 },
    new Job { Id = 64, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Cairo, Egypt", Salary = 18000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 27), Deadline = new DateTime(2027, 1, 4), Status = "Open", CompanyId = 7 },
    new Job { Id = 65, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Cairo, Egypt", Salary = 20500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 26), Deadline = new DateTime(2027, 1, 5), Status = "Closed", CompanyId = 7 },
    new Job { Id = 66, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Cairo, Egypt", Salary = 23000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 25), Deadline = new DateTime(2027, 1, 6), Status = "Open", CompanyId = 7 },
    new Job { Id = 67, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Cairo, Egypt", Salary = 25500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 24), Deadline = new DateTime(2027, 1, 7), Status = "Open", CompanyId = 7 },
    new Job { Id = 68, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Cairo, Egypt", Salary = 28000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 23), Deadline = new DateTime(2027, 1, 8), Status = "Open", CompanyId = 7 },
    new Job { Id = 69, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Cairo, Egypt", Salary = 30500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 22), Deadline = new DateTime(2027, 1, 9), Status = "Open", CompanyId = 7 },
    new Job { Id = 70, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Cairo, Egypt", Salary = 8000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 21), Deadline = new DateTime(2027, 1, 10), Status = "Closed", CompanyId = 7 },

    new Job { Id = 71, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Giza, Egypt", Salary = 10500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 20), Deadline = new DateTime(2027, 1, 11), Status = "Open", CompanyId = 8 },
    new Job { Id = 72, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Giza, Egypt", Salary = 13000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 19), Deadline = new DateTime(2027, 1, 12), Status = "Open", CompanyId = 8 },
    new Job { Id = 73, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Giza, Egypt", Salary = 15500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 18), Deadline = new DateTime(2027, 1, 13), Status = "Open", CompanyId = 8 },
    new Job { Id = 74, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Giza, Egypt", Salary = 18000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 17), Deadline = new DateTime(2027, 1, 14), Status = "Open", CompanyId = 8 },
    new Job { Id = 75, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Giza, Egypt", Salary = 20500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 16), Deadline = new DateTime(2027, 1, 15), Status = "Closed", CompanyId = 8 },

    new Job { Id = 76, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Cairo, Egypt", Salary = 23000, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 15), Deadline = new DateTime(2027, 1, 1), Status = "Open", CompanyId = 9 },
    new Job { Id = 77, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Cairo, Egypt", Salary = 25500, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 14), Deadline = new DateTime(2027, 1, 2), Status = "Open", CompanyId = 9 },
    new Job { Id = 78, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Cairo, Egypt", Salary = 28000, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 13), Deadline = new DateTime(2027, 1, 3), Status = "Open", CompanyId = 9 },
    new Job { Id = 79, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Cairo, Egypt", Salary = 30500, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 12), Deadline = new DateTime(2027, 1, 4), Status = "Open", CompanyId = 9 },
    new Job { Id = 80, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Cairo, Egypt", Salary = 8000, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 11), Deadline = new DateTime(2027, 1, 5), Status = "Closed", CompanyId = 9 },
    new Job { Id = 81, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Cairo, Egypt", Salary = 10500, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 10), Deadline = new DateTime(2027, 1, 6), Status = "Open", CompanyId = 9 },
    new Job { Id = 82, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Cairo, Egypt", Salary = 13000, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 9), Deadline = new DateTime(2027, 1, 7), Status = "Open", CompanyId = 9 },

    new Job { Id = 83, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Giza, Egypt", Salary = 15500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 8), Deadline = new DateTime(2027, 1, 8), Status = "Open", CompanyId = 10 },
    new Job { Id = 84, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Giza, Egypt", Salary = 18000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 7), Deadline = new DateTime(2027, 1, 9), Status = "Open", CompanyId = 10 },
    new Job { Id = 85, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Giza, Egypt", Salary = 20500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 6), Deadline = new DateTime(2027, 1, 10), Status = "Closed", CompanyId = 10 },
    new Job { Id = 86, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Giza, Egypt", Salary = 23000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 5), Deadline = new DateTime(2027, 1, 11), Status = "Open", CompanyId = 10 },
    new Job { Id = 87, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Giza, Egypt", Salary = 25500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 4), Deadline = new DateTime(2027, 1, 12), Status = "Open", CompanyId = 10 },
    new Job { Id = 88, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Giza, Egypt", Salary = 28000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 3), Deadline = new DateTime(2027, 1, 13), Status = "Open", CompanyId = 10 },
    new Job { Id = 89, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Giza, Egypt", Salary = 30500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 2), Deadline = new DateTime(2027, 1, 14), Status = "Open", CompanyId = 10 },
    new Job { Id = 90, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Giza, Egypt", Salary = 8000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 1), Deadline = new DateTime(2027, 1, 15), Status = "Closed", CompanyId = 10 },
    new Job { Id = 91, Title = "Junior .NET Developer", Description = "Develop and maintain applications using modern technologies.", Location = "Giza, Egypt", Salary = 10500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 30), Deadline = new DateTime(2027, 1, 1), Status = "Open", CompanyId = 10 },
    new Job { Id = 92, Title = "Frontend Developer", Description = "Build responsive user interfaces and improve user experience.", Location = "Giza, Egypt", Salary = 13000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 29), Deadline = new DateTime(2027, 1, 2), Status = "Open", CompanyId = 10 },
    new Job { Id = 93, Title = "Backend Developer", Description = "Design APIs, databases, and reliable backend services.", Location = "Giza, Egypt", Salary = 15500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 28), Deadline = new DateTime(2027, 1, 3), Status = "Open", CompanyId = 10 },
    new Job { Id = 94, Title = "Full Stack Developer", Description = "Develop frontend and backend features for web applications.", Location = "Giza, Egypt", Salary = 18000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 27), Deadline = new DateTime(2027, 1, 4), Status = "Open", CompanyId = 10 },
    new Job { Id = 95, Title = "Software Tester", Description = "Test applications, identify bugs, and improve software quality.", Location = "Giza, Egypt", Salary = 20500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 26), Deadline = new DateTime(2027, 1, 5), Status = "Closed", CompanyId = 10 },
    new Job { Id = 96, Title = "Data Analyst", Description = "Analyze data and prepare reports to support decisions.", Location = "Giza, Egypt", Salary = 23000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 25), Deadline = new DateTime(2027, 1, 6), Status = "Open", CompanyId = 10 },
    new Job { Id = 97, Title = "AI Engineer", Description = "Build and evaluate machine learning solutions.", Location = "Giza, Egypt", Salary = 25500, EmploymentType = "PartTime", PostedDate = new DateTime(2026, 9, 24), Deadline = new DateTime(2027, 1, 7), Status = "Open", CompanyId = 10 },
    new Job { Id = 98, Title = "UI UX Designer", Description = "Create user flows, wireframes, and high-quality interfaces.", Location = "Giza, Egypt", Salary = 28000, EmploymentType = "Contract", PostedDate = new DateTime(2026, 9, 23), Deadline = new DateTime(2027, 1, 8), Status = "Open", CompanyId = 10 },
    new Job { Id = 99, Title = "DevOps Engineer", Description = "Maintain deployment pipelines and cloud infrastructure.", Location = "Giza, Egypt", Salary = 30500, EmploymentType = "Internship", PostedDate = new DateTime(2026, 9, 22), Deadline = new DateTime(2027, 1, 9), Status = "Open", CompanyId = 10 },
    new Job { Id = 100, Title = "Mobile App Developer", Description = "Build and maintain mobile applications.", Location = "Giza, Egypt", Salary = 8000, EmploymentType = "FullTime", PostedDate = new DateTime(2026, 9, 21), Deadline = new DateTime(2027, 1, 10), Status = "Closed", CompanyId = 10 }
);
     

            modelBuilder.Entity<Application>().HasData(
                new Application { Id = 1, CandidateProfileId = 1, JobId = 1, ApplicationDate = new DateTime(2026, 10, 5), Status = "Pending", CoverLetter = "I am interested in this .NET development position." },
                new Application { Id = 2, CandidateProfileId = 1, JobId = 3, ApplicationDate = new DateTime(2026, 10, 6), Status = "UnderReview", CoverLetter = "My backend development experience matches the role." },
                new Application { Id = 3, CandidateProfileId = 2, JobId = 2, ApplicationDate = new DateTime(2026, 10, 7), Status = "Accepted", CoverLetter = "I enjoy building responsive web applications." },
                new Application { Id = 4, CandidateProfileId = 3, JobId = 6, ApplicationDate = new DateTime(2026, 10, 8), Status = "Rejected", CoverLetter = "I would like to contribute my data analysis skills." },
                new Application { Id = 5, CandidateProfileId = 4, JobId = 8, ApplicationDate = new DateTime(2026, 10, 9), Status = "Pending", CoverLetter = "I am passionate about user-centered design." },
                new Application { Id = 6, CandidateProfileId = 5, JobId = 4, ApplicationDate = new DateTime(2026, 10, 10), Status = "UnderReview", CoverLetter = "I have experience with application development." },
                new Application { Id = 7, CandidateProfileId = 6, JobId = 1, ApplicationDate = new DateTime(2026, 10, 11), Status = "Pending", CoverLetter = "I have strong programming and problem-solving skills." },
                new Application { Id = 8, CandidateProfileId = 2, JobId = 4, ApplicationDate = new DateTime(2026, 10, 12), Status = "Accepted", CoverLetter = "I would be happy to join your development team." },
                new Application { Id = 9, CandidateProfileId = 3, JobId = 7, ApplicationDate = new DateTime(2026, 10, 13), Status = "Pending", CoverLetter = "I am interested in working on AI solutions." },
                new Application { Id = 10, CandidateProfileId = 5, JobId = 10, ApplicationDate = new DateTime(2026, 10, 14), Status = "UnderReview", CoverLetter = "I am excited about this mobile development opportunity." }
            );
        }
    }
}
