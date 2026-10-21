using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recruitment.DTOs;
using Recruitment.Repos;

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public CompaniesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet("MostJobs")]
        public async Task<IActionResult> MostJobs()
        {
            var comp = await unitOfWork.Companies.mostJobs();
            return Ok(mapper.Map<CompanyReadDTO>(comp));
        }
    }
}
