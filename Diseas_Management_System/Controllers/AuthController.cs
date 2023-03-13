using Diseas_Management_System.Data;
using Diseas_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diseas_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;

        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<int>> Register(AdminRegisterDTO adminDTO)
        {
            var res = await _authRepo.Register(new Admin() { Email = adminDTO.Email }, adminDTO.Password);
            if (res == 0)
            {
                return BadRequest($"cannot register {adminDTO.Email}");
            }
            return Ok($"Admin Account Created Successfully!");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<int>> Login(AdminLoginDTO adminDTO)
        {
            var res = await _authRepo.Login(adminDTO.Email, adminDTO.Password);
            if (res == null)
            {
                return BadRequest($"Incorrect Email or Password");
            }
            return Ok(res);
        }
    }
}
