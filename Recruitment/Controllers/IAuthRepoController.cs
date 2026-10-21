using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recruitment.DTOs;
using Recruitment.Repos;

namespace Recruitment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IAuthRepoController : ControllerBase
    {
        private readonly IUnitOfWork UnitOfWork;

        public IAuthRepoController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await UnitOfWork.Auth.Login(loginDTO);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Role == "Admin")
            {
                return BadRequest("Can't register admin");
            }
            if (registerDTO.Role != "Canidate" || registerDTO.Role != "Employee")
            {
                return BadRequest("Invalid Role");
            }
            var response = await UnitOfWork.Auth.Register(registerDTO);
            if (response == null)
            {
                return BadRequest("Email already used");
            }
            await UnitOfWork.SaveAsync();
            return Ok(response);
        }
    }
}
