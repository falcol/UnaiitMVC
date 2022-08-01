using UnaiitMVC.Models;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string? Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(AppUser user, string token)
        {
            Id = user.Id;
            Username = user.UserName;
            Token = token;
        }
    }
}
