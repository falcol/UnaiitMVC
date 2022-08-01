using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnaiitMVC.CustomValidate;

namespace UnaiitMVC.Models.School
{
    [Table("School")]
    public class SchoolTable
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Cần phải có tên trường")]
        [Display(Name = "Tên trường")]
        [StringLength(100, ErrorMessage = "Tên trường không được vượt quá 100 ký tự")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Cần phải có địa chỉ")]
        [Display(Name = "Địa chỉ")]
        [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Cần phải có ngày thành lập")]
        [Display(Name = "Ngày thành lập")]
        [DataType(DataType.Date)]
        [CustomDateTime(ErrorMessage = "Ngày thành lập nhỏ hơn hoặc bằng ngày hiện tại")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Founded { get; set; }

        [Display(Name = "Số lượng học viên tối đa")]
        [Range(0, 1000, ErrorMessage = "Số lượng học viên tối đa nhỏ hơn 1000")]
        [Required(ErrorMessage = "Cần phải có số lượng học viên tối đa")]
        public int? Capacity { get; set; }
    }
}
