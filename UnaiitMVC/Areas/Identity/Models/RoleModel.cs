using Microsoft.AspNetCore.Identity;

namespace App.Areas.Identity.Models.RoleViewModels
{
    public class RoleModel : IdentityRole
    {
        public string[]? Claims { get; set; }

    }
}
