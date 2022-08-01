using System.ComponentModel.DataAnnotations;
using UnaiitMVC.Models;

namespace UnaiitMVC.CustomValidate
{
    public class Min18Years : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var student = (AppUser)validationContext.ObjectInstance;

            if (student.BirthDate == null)
                return new ValidationResult("Cần nhập ngày sinh");

            var age = DateTime.Today.Year - student.BirthDate?.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Học sinh phải đủ 18 tuổi");
        }
    }
}
