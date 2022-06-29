using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _uM;
        private readonly RoleManager<IdentityRole> _rM;
        private readonly IConfiguration _conf;

        public AuthenticateController(UserManager<IdentityUser> uM, RoleManager<IdentityRole> rM, IConfiguration conf)
        {
            _uM = uM;
            _rM = rM;
            _conf = conf;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _uM.FindByNameAsync(model.Username);
            if (user != null && await _uM.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _uM.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized(new { message = "Username/Password salah, mohon untuk mengecek kembali atau hubungi CS" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _uM.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Username tersebut sudah tidak tersedia, mohon pilih yang lain"
                });
            }

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _uM.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Terjadi kesalahan dalam registrasi akun, mohon dicek kembali"
                });
            }
            await _uM.AddToRoleAsync(user, UserRoles.User);
            
            return Ok(new Response
            {
                Status = "Sukses",
                Message = "User telah berhasil didaftarkan sebagai user biasa"
            });
        }


        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await (_uM.FindByNameAsync(model.Username));
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Username tersebut sudah tidak tersedia, mohon pilih yang lain"
                });
                
            }
            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _uM.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Terjadi kesalahan dalam registrasi akun, mohon dicek kembali"
                });
            }
            if (!await _rM.RoleExistsAsync(UserRoles.Admin))
            {
                await _rM.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _rM.RoleExistsAsync(UserRoles.User))
            {
                await _rM.CreateAsync(new IdentityRole(UserRoles.User));
            }
            if (await _rM.RoleExistsAsync(UserRoles.Admin))
            {
                await _uM.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _rM.RoleExistsAsync(UserRoles.Admin))
            {
                await _uM.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { 
                Status = "Success", 
                Message = "User telah berhasil didaftarkan sebagai admin" 
            });
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _conf["JWT:validIssuer"],
                audience: _conf["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
