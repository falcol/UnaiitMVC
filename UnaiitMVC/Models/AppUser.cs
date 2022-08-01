using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace UnaiitMVC.Models
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar")]
        [Display(Name = "Quê quán")]
        [StringLength(250, ErrorMessage = "Quê quán không được vượt quá 250 ký tự")]
        public string? HomeAddress { get; set; }

        [DataType(DataType.Date)]
        [AllowNull]
        public DateTime? DeletedAt { get; set; }

        [Display(Name = "Loại tài khoản")]
        public int type { get; set; } = 0;

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Tên")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 250 ký tự")]
        public string? Name { get; set; }
    }
}
