using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using AuthenticationAPI.Services;
using AuthenticationAPI.Models;

namespace AuthenticationAPI.Controllers
{
    [Authorize(AuthenticationSchemes="Bearer")]
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _service;

        private string GetIpAdress()
        {
            var result = Request.Headers.ContainsKey("X-Forwarded-For");
            if(result)
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        public UserController(IUserService service) => _service = service;
        

        /// <summary>
        /// username = "test1" ,  password ="1234"
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<AuthResponse> Authenticate([FromBody] AuthRequest req)
        {
            var res = _service.Authenticate(req, GetIpAdress());
            if(res == null)
                return StatusCode(204, new { message = "Incorrect Password or Username"});
            return Ok(res);
        }
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public ActionResult<AuthResponse> RefreshToken([FromBody] RefreshTokenRequest req)
        {
            var response = _service.RefreshToken(req, GetIpAdress());

            if (response == null)
                return Unauthorized(new { message = "Invalid Token" });

            return Ok(response);
        }
        /// <summary>
        /// Authorization Key gereklidir , Authenticate işleminin ardından jwtTokeni Authorization 
        /// butonuna tıklayıp inputa gösterildiği şekilde girerek login olun ve test edin
        /// </summary>
        [HttpPost("revoke-refresh-token")]
        public IActionResult RevokeToken([FromBody] RefreshTokenRequest req)
        {
            var response = _service.RevokeRefreshToken(req, GetIpAdress());

            if (!response)
                return StatusCode(204, new {message = "Token not found"});

            return Ok(new { message = "Token revoked" });
        }

    }
}