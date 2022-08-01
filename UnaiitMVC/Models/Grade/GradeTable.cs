using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnaiitMVC.CustomValidate;
using UnaiitMVC.Models.Faculty;

namespace UnaiitMVC.Models.Grade
{
    [Table("Grade")]
    public class GradeTable
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Cần phải có tên lớp")]
        [Display(Name = "Tên lớp")]
        [MaxLength(50, ErrorMessage = "Tên lớp không được vượt quá 50 ký tự")]
        public string? Name { get; set; }

        [Display(Name = "Học viên lớp tối đa")]
        [Required(ErrorMessage = "Cần phải có học viên tối đa")]
        [ValidateCapacityGrade]
        public int? Capacity { get; set; }

        [Display(Name = "Ngày thành lập")]
        [Required(ErrorMessage = "Cần phải có ngày thành lập")]
        [DataType(DataType.Date)]
        [CustomDateTime(ErrorMessage = "Ngày thành lập nhỏ hơn hoặc bằng ngày hiện tại")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Founded { get; set; }

        [Display(Name = "Người tạo")]
        [MaxLength(50, ErrorMessage = "Tên người tạo không được vượt quá 50 ký tự")]
        [Required(ErrorMessage = "Cần phải có người tạo")]
        public string? Creator { get; set; }

        [Display(Name = "Tên khoa")]
        public FacultyTable? Faculty { get; set; }
        [Display(Name = "Khoa")]
        // [Required(ErrorMessage = "Cần phải có khoa")]
        public Guid? FacultyId { get; set; }
        public ICollection<AppUser>? Students { get; set; }
    }
}
