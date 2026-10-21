using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recruitment.DTOs;
using Recruitment.Repos;

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public ApplicationsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
                var v = await unitOfWork.Applications.GetAllAsync();
            var comp = mapper.Map<List<ApplicationReadDTO>>(v);
            return Ok(comp);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, string status)
        {
            var app = await unitOfWork.Applications.GetByIdAsync(id);
            app.Status = status;
            unitOfWork.Applications.Update(app);
            await unitOfWork.SaveAsync();
            return Ok(app);
        }
    }
}
