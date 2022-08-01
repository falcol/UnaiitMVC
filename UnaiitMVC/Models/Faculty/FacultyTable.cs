using System.ComponentModel.DataAnnotations;
using UnaiitMVC.CustomValidate;
using UnaiitMVC.Models.School;

namespace UnaiitMVC.Models.Faculty
{
    public class FacultyTable
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Cần phải có tên khoa")]
        [Display(Name = "Tên khoa")]
        [StringLength(100, ErrorMessage = "Tên khoa không được vượt quá 100 ký tự")]
        public string? Name { get; set; }

        [Display(Name = "Học viên khoa tối đa")]
        [Required(ErrorMessage = "Cần phải có học viên tối đa")]
        [ValidateCapacityFaculty]
        public int? Capacity { get; set; }

        [Display(Name = "Ngày thành lập")]
        [Required(ErrorMessage = "Cần phải có ngày thành lập")]
        [DataType(DataType.Date)]
        [CustomDateTime(ErrorMessage = "Ngày thành lập nhỏ hơn hoặc bằng ngày hiện tại")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Founded { get; set; }

        [Display(Name = "Người tạo")]
        [StringLength(100, ErrorMessage = "Tên người tạo không được vượt quá 100 ký tự")]
        [Required(ErrorMessage = "Cần phải có tên người tạo")]
        public string? Creator { get; set; }

        [Display(Name = "Tên trường")]
        public SchoolTable? School { get; set; }

        [Display(Name = "Trường")]
        public Guid? SchoolId { get; set; }
    }
}
