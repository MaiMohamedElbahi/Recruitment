using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recruitment.DTOs;
using Recruitment.Repos;

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanidateProfilesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        public CanidateProfilesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Patch(int id, CandidateProfileUpdateDTO candidateProfileUpdateDTO)
        {
            var app = await unitOfWork.CandidateProfiles.GetByIdAsync(id);
            mapper.Map(candidateProfileUpdateDTO, app);
            unitOfWork.CandidateProfiles.Update(app);
            await unitOfWork.SaveAsync();
            return Ok(app);
        }
    }
}
