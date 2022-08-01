using System.ComponentModel;
using UnaiitMVC.Models;

namespace App.Areas.Identity.Models.UserViewModels
{
  public class AddUserRoleModel
  {
    public AppUser? user { get; set; }

    [DisplayName("Các role gán cho user")]
    public string[]? RoleNames { get; set; }

  }
}
