using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserToken.Data;
using UserToken.Models;

namespace UserToken.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        UserManager<Owner> UserManager;
        IConfiguration config;

        public OwnersController(UserManager<Owner> userManager, IConfiguration configuration)
        {
            this.UserManager = userManager;
            this.config = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            Owner user = await UserManager.FindByNameAsync(login.Username);

            if( user != null && await UserManager.CheckPasswordAsync(user, login.Password))
            {
                IList<string> roles = await UserManager.GetRolesAsync(user);
                List<Claim> authClaims = new List<Claim>();

                foreach(string role in roles)
                    authClaims.Add(new Claim(ClaimTypes.Role, role));

                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["JWT:Secret"]));
                JwtSecurityToken Token = new JwtSecurityToken(
                    issuer: this.config["JWT:ValidIssuer"],
                    audience: this.config["JWT:ValidAudience"],
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(Token),
                    validTo = Token.ValidTo
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Le Nom d'utilisateur ou le mot de pass est invalide." });
            };

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
