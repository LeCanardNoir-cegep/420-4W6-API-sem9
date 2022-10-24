using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserToken.Data;
using UserToken.Models;

namespace UserToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        UserManager<Owner> UserManager;

        public OwnersController(UserManager<Owner> userManager)
        {
            this.UserManager = userManager;
        }

        [HttpGet]
        public Owner GetOwner()
        {
            return UserManager.FindByNameAsync("Roland123").Result;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            if(register.Password != register.PasswordConfirm)
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Les deux mots de passe spécifié sont différents." });

            Owner owner = new Owner()
            {
                UserName = register.UserName,
                Email = register.Email
            };

            IdentityResult identityResult = await this.UserManager.CreateAsync(owner, register.Password);

            if (!identityResult.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "La création de l'utilisateur a échoué." });

            return Ok();
        }
    }
}
