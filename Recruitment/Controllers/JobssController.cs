using AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recruitment.DTOs;
using Recruitment.Models;
using Recruitment.Repos;

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper IMapper;

        public JobsController(IUnitOfWork unitOfWork, IMapper IMapper)
        {
            this.unitOfWork = unitOfWork;
            this.IMapper = IMapper;
        }

        [HttpGet("ThirdHighestSalary")]
        public async Task<IActionResult> Highest()
        {
            var jobs = await unitOfWork.Jobs.ThirdHighest();
            return Ok(jobs.Salary);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComp(int id)
        {
            var jobs = await unitOfWork.Jobs.GetWithCompany(id);
            if(jobs == null)
            {
                return NotFound();
            }
            return Ok(jobs);
        }

        [HttpGet("Serach{Keyword}")]
        public async Task<IActionResult> search( string Keyword )
        {
            var jobs = await unitOfWork.Jobs.Search(Keyword);
            if (jobs == null)
            {
                return NotFound();
            }
            return Ok(IMapper.Map<List<JobReadDTO>>(jobs));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var job = await unitOfWork.Jobs.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
             unitOfWork.Jobs.Delete(job);
            await unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
