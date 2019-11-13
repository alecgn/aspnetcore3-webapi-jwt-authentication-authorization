using System.Threading.Tasks;
using AspNetCore3_WebAPI_Authentication_Authorization.Models;
using AspNetCore3_WebAPI_Authentication_Authorization.Repositories;
using AspNetCore3_WebAPI_Authentication_Authorization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AspNetCore3_WebAPI_Authentication_Authorization.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        private IConfiguration Configuration;
        public HomeController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonymous";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Authenticated -> {User.Identity.Name}";

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Employee";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles="manager")]
        public string Manager() => "Manager";

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Incorrect user or password" });

            var token = new TokenService(Configuration).GenerateToken(user);
            user.Password = "";

            return new { user, token };
        }
    }
}