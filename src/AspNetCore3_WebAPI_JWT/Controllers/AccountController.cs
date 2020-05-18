using AspNetCore3_WebAPI_JWT.DTOs;
using AspNetCore3_WebAPI_JWT.Interfaces;
using AspNetCore3_WebAPI_JWT.Models;
using AspNetCore3_WebAPI_JWT.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AspNetCore3_WebAPI_JWT.Controllers
{
    [Route("v1/account")]
    public class AccountController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountController(IConfiguration configuration, IMapper mapper, IUserRepository userRepository)
        {
            this._configuration = configuration;
            this._mapper = mapper;
            this._userRepository = userRepository;
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.Login(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { errors = "Incorrect user or password." });

            var token = new TokenService(_configuration).GenerateToken(user);
            var userDTO = _mapper.Map<UserDTO>(user);

            return Ok(new { user = userDTO, token });
        }
    }
}