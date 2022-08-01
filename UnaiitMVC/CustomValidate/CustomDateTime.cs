using System.ComponentModel.DataAnnotations;

namespace UnaiitMVC.CustomValidate
{
    public class CustomDateTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime <= DateTime.Now;
        }
    }
}
