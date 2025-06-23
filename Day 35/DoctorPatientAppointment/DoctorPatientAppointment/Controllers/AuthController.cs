using System.Security.Claims;
using DoctorPatientAppointment.Interfaces;
using DoctorPatientAppointment.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatientAppointment.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IRepository<string, User> _userRepository;
 
        public AuthController(ITokenService tokenService, IRepository<string, User> userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
 
        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
 
        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded) return Unauthorized("Google authentication failed");
 
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email)) return BadRequest("Email not found in Google data");
 
            // Get or create user
            var user = await _userRepository.Get(email);
            if (user == null)
            {
                user = new User
                {
                    Username = email,
                    Role = "Patient" // default or based on logic
                };
                await _userRepository.Add(user);
            }
 
            var jwt = await _tokenService.GenerateToken(user);
            return Ok(new { token = jwt });
        }
    }

}