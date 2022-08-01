using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UnaiitMVC.Models;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<AppUser> GetAll();
        AppUser GetById(string id);
    }

    public class UserService : IUserService
    {
        private readonly UnaiitDbContext _context;

        private readonly AppSettings _appSettings;
        private readonly SignInManager<AppUser> _signInManager;

        public UserService(IOptions<AppSettings> appSettings, UnaiitDbContext context, SignInManager<AppUser> signInManager)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _signInManager = signInManager;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var result = _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false).Result;
            if (!result.Succeeded)
            {
                return null;
            }
            var user = _context.AppUser.SingleOrDefault(x => x.UserName == model.Username);
            var test = _context.AppUser.ToList();

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<AppUser> GetAll()
        {
            return _context.AppUser.ToList();
        }

        public AppUser GetById(string id)
        {
            return _context.AppUser.SingleOrDefault(x => x.Id == id);
        }

        // helper methods

        private string generateJwtToken(AppUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret ?? "aNdRgUkXp2s5v8y/B?E");
            var key2 = Convert.FromBase64String(Convert.ToBase64String(key));
            var Issuer = _appSettings.Issuer ?? "https://localhost:7106";
            var Audience = _appSettings.Audience ?? "https://localhost:7106";
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()),
                                                     new Claim("username", user.UserName??"") ,
                                                     new Claim("password", user.PasswordHash??"")
                                                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key2), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
