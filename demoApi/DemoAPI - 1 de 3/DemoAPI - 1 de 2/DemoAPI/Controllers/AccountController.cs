using DemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<DemoUser> userManager;

        public AccountController(UserManager<DemoUser> userManager)
        {
            this.userManager = userManager;
        }

        // POST: api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            if(register.Password != register.PasswordConfirm)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            DemoUser user = new DemoUser()
            {
                UserName = register.UserName,
                Email = register.Email
            };
            IdentityResult identityResult = await this.userManager.CreateAsync(user, register.Password);

            if(!identityResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


            return Ok();
        }

        
    }
}
