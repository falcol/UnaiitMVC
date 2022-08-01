using System.ComponentModel.DataAnnotations;
using UnaiitMVC.Models;
using UnaiitMVC.Models.Grade;

namespace UnaiitMVC.CustomValidate
{
    public class ValidateCapacityGrade : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var grade_faculty = (GradeTable)validationContext.ObjectInstance;
            var _context = (UnaiitDbContext?)validationContext?.GetService(typeof(UnaiitDbContext));
            if (_context?.Grade.FirstOrDefault(m => m.Id == grade_faculty.Id) != null)
            {
                return ValidationResult.Success;
            }
            if (grade_faculty.FacultyId == null)
            {
                return new ValidationResult("Bạn chưa chọn khoa");
            }
            var capacity_faculty_value = _context?.Faculty?.FirstOrDefault(m => m.Id == grade_faculty.FacultyId)?.Capacity;
            if (capacity_faculty_value == null)
                capacity_faculty_value = 0;

            var all_capacity_faculty = _context?.Grade.Where(x => x.FacultyId == grade_faculty.FacultyId).ToList() ?? new List<GradeTable>();
            var sum_capacity_faculty = all_capacity_faculty.Sum(x => x.Capacity);
            Console.WriteLine("CAPACITY2: " + sum_capacity_faculty);
            var capacity_left = capacity_faculty_value - sum_capacity_faculty;
            if (capacity_left >= 0)
            {
                return (grade_faculty?.Capacity > capacity_left) ? new ValidationResult($"Khối còn đủ cho {(int)capacity_left} học sinh") : ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Khối đã đủ học sinh");
            }
        }
    }
}
