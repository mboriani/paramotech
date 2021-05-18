using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Services;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService service;

        private readonly List<UserDto> _users = new List<UserDto>();
        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserDto user)
        {
            if (ModelState.IsValid)
            {
                var result = await this.service.AddUser(user);
                if (result.Errors.Count == 0)
                    return Ok();
                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await this.service.GetAll());
        }
    }
}
