using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recruitment.Migrations
{
    /// <inheritdoc />
    public partial class gd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResumeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skill = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverLetter = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CandidateProfileId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_CandidateProfiles_CandidateProfileId",
                        column: x => x.CandidateProfileId,
                        principalTable: "CandidateProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Applications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "Password", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Admin User", "Admin123!", "Admin" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed@example.com", "Ahmed Employer", "Employer123!", "Employer" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara@example.com", "Sara Employer", "Employer123!", "Employer" },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "omar@example.com", "Omar Candidate", "Candidate123!", "Candidate" },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mona@example.com", "Mona Candidate", "Candidate123!", "Candidate" },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "youssef@example.com", "Youssef Candidate", "Candidate123!", "Candidate" },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nour@example.com", "Nour Employer", "Employer123!", "Employer" },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "laila@example.com", "Laila Candidate", "Candidate123!", "Candidate" },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "karim@example.com", "Karim Employer", "Employer123!", "Employer" },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hana@example.com", "Hana Candidate", "Candidate123!", "Candidate" },
                    { 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ali@example.com", "Ali Employer", "Employer123!", "Employer" },
                    { 12, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "salma@example.com", "Salma Candidate", "Candidate123!", "Candidate" }
                });

            migrationBuilder.InsertData(
                table: "CandidateProfiles",
                columns: new[] { "Id", "Phone", "ResumeUrl", "Skill", "UserId", "YearsOfExperience" },
                values: new object[,]
                {
                    { 1, "01012345678", null, "C#, ASP.NET Core, SQL", 4, 2 },
                    { 2, "01023456789", null, "React, JavaScript, CSS", 5, 1 },
                    { 3, "01034567890", null, "Python, Machine Learning", 6, 3 },
                    { 4, "01045678901", null, "UI/UX, Figma, Graphic Design", 8, 1 },
                    { 5, "01056789012", null, "Java, Spring Boot, SQL", 10, 4 },
                    { 6, "01067890123", null, "C++, Algorithms, Problem Solving", 12, 2 }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Description", "Location", "Name", "OwnerId", "Website" },
                values: new object[,]
                {
                    { 1, "Software development and IT solutions.", "Cairo, Egypt", "TechNova", 2, "https://technova.example.com" },
                    { 2, "Enterprise software and digital transformation.", "Giza, Egypt", "Future Systems", 2, "https://futuresystems.example.com" },
                    { 3, "Web and mobile application development.", "Alexandria, Egypt", "Nile Digital", 3, "https://niledigital.example.com" },
                    { 4, "Cloud computing and IT consulting.", "Cairo, Egypt", "Smart Solutions", 3, "https://smartsolutions.example.com" },
                    { 5, "Data analytics and artificial intelligence.", "Giza, Egypt", "DataSphere", 7, "https://datasphere.example.com" },
                    { 6, "Custom software and application engineering.", "Mansoura, Egypt", "CodeCraft", 7, "https://codecraft.example.com" },
                    { 7, "Cloud infrastructure and cybersecurity.", "Cairo, Egypt", "CloudCore", 9, "https://cloudcore.example.com" },
                    { 8, "Creative design and digital products.", "Alexandria, Egypt", "PixelWorks", 9, "https://pixelworks.example.com" },
                    { 9, "Robotics, embedded systems and automation.", "Cairo, Egypt", "NextGen Robotics", 11, "https://nextgenrobotics.example.com" },
                    { 10, "Technology training and consulting.", "Tanta, Egypt", "BrightPath", 11, "https://brightpath.example.com" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CompanyId", "Deadline", "Description", "EmploymentType", "Location", "PostedDate", "Salary", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 2, 1, new DateTime(2027, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 3, 1, new DateTime(2027, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 4, 1, new DateTime(2027, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 5, 2, new DateTime(2027, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 6, 2, new DateTime(2027, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 7, 2, new DateTime(2027, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 8, 2, new DateTime(2027, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 9, 2, new DateTime(2027, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 10, 2, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" },
                    { 11, 3, new DateTime(2027, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 12, 3, new DateTime(2027, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 13, 3, new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 14, 3, new DateTime(2027, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 15, 3, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 16, 3, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 17, 3, new DateTime(2027, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 18, 3, new DateTime(2027, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 19, 4, new DateTime(2027, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 20, 4, new DateTime(2027, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "FullTime", "Giza, Egypt", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" },
                    { 21, 4, new DateTime(2027, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 22, 4, new DateTime(2027, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 23, 4, new DateTime(2027, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 24, 4, new DateTime(2027, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 25, 4, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 26, 4, new DateTime(2027, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 27, 4, new DateTime(2027, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 28, 4, new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 29, 5, new DateTime(2027, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 30, 5, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" },
                    { 31, 5, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 32, 5, new DateTime(2027, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 33, 5, new DateTime(2027, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 34, 5, new DateTime(2027, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 35, 5, new DateTime(2027, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 36, 5, new DateTime(2027, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 37, 5, new DateTime(2027, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 38, 5, new DateTime(2027, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 39, 5, new DateTime(2027, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 40, 5, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "Contract", "Cairo, Egypt", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" },
                    { 41, 6, new DateTime(2027, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 42, 6, new DateTime(2027, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 43, 6, new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 44, 6, new DateTime(2027, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 45, 6, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 46, 6, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 47, 6, new DateTime(2027, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 48, 6, new DateTime(2027, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 49, 6, new DateTime(2027, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 50, 6, new DateTime(2027, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" },
                    { 51, 6, new DateTime(2027, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 52, 6, new DateTime(2027, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 53, 6, new DateTime(2027, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 54, 6, new DateTime(2027, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 55, 7, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 56, 7, new DateTime(2027, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 57, 7, new DateTime(2027, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 58, 7, new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 59, 7, new DateTime(2027, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 60, 7, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "FullTime", "Cairo, Egypt", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" },
                    { 61, 7, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 62, 7, new DateTime(2027, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 63, 7, new DateTime(2027, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 64, 7, new DateTime(2027, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 65, 7, new DateTime(2027, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 66, 7, new DateTime(2027, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 67, 7, new DateTime(2027, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 68, 7, new DateTime(2027, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 69, 7, new DateTime(2027, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 70, 7, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" },
                    { 71, 8, new DateTime(2027, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 72, 8, new DateTime(2027, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 73, 8, new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 74, 8, new DateTime(2027, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 75, 8, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 76, 9, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "Internship", "Cairo, Egypt", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 77, 9, new DateTime(2027, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 78, 9, new DateTime(2027, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 79, 9, new DateTime(2027, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "Contract", "Cairo, Egypt", new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 80, 9, new DateTime(2027, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "Internship", "Cairo, Egypt", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" },
                    { 81, 9, new DateTime(2027, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "FullTime", "Cairo, Egypt", new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 82, 9, new DateTime(2027, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "PartTime", "Cairo, Egypt", new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 83, 10, new DateTime(2027, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 84, 10, new DateTime(2027, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 85, 10, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 86, 10, new DateTime(2027, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 87, 10, new DateTime(2027, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 88, 10, new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 89, 10, new DateTime(2027, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 90, 10, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" },
                    { 91, 10, new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop and maintain applications using modern technologies.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, "Open", "Junior .NET Developer" },
                    { 92, 10, new DateTime(2027, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build responsive user interfaces and improve user experience.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 13000m, "Open", "Frontend Developer" },
                    { 93, 10, new DateTime(2027, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Design APIs, databases, and reliable backend services.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 15500m, "Open", "Backend Developer" },
                    { 94, 10, new DateTime(2027, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Develop frontend and backend features for web applications.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 18000m, "Open", "Full Stack Developer" },
                    { 95, 10, new DateTime(2027, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test applications, identify bugs, and improve software quality.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 20500m, "Closed", "Software Tester" },
                    { 96, 10, new DateTime(2027, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analyze data and prepare reports to support decisions.", "FullTime", "Giza, Egypt", new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 23000m, "Open", "Data Analyst" },
                    { 97, 10, new DateTime(2027, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and evaluate machine learning solutions.", "PartTime", "Giza, Egypt", new DateTime(2026, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 25500m, "Open", "AI Engineer" },
                    { 98, 10, new DateTime(2027, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create user flows, wireframes, and high-quality interfaces.", "Contract", "Giza, Egypt", new DateTime(2026, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 28000m, "Open", "UI UX Designer" },
                    { 99, 10, new DateTime(2027, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maintain deployment pipelines and cloud infrastructure.", "Internship", "Giza, Egypt", new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 30500m, "Open", "DevOps Engineer" },
                    { 100, 10, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build and maintain mobile applications.", "FullTime", "Giza, Egypt", new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8000m, "Closed", "Mobile App Developer" }
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "ApplicationDate", "CandidateProfileId", "CoverLetter", "JobId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "I am interested in this .NET development position.", 1, "Pending" },
                    { 2, new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "My backend development experience matches the role.", 3, "UnderReview" },
                    { 3, new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "I enjoy building responsive web applications.", 2, "Accepted" },
                    { 4, new DateTime(2026, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "I would like to contribute my data analysis skills.", 6, "Rejected" },
                    { 5, new DateTime(2026, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "I am passionate about user-centered design.", 8, "Pending" },
                    { 6, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "I have experience with application development.", 4, "UnderReview" },
                    { 7, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "I have strong programming and problem-solving skills.", 1, "Pending" },
                    { 8, new DateTime(2026, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "I would be happy to join your development team.", 4, "Accepted" },
                    { 9, new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "I am interested in working on AI solutions.", 7, "Pending" },
                    { 10, new DateTime(2026, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "I am excited about this mobile development opportunity.", 10, "UnderReview" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CandidateProfileId",
                table: "Applications",
                column: "CandidateProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobId",
                table: "Applications",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProfiles_UserId",
                table: "CandidateProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "CandidateProfiles");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
