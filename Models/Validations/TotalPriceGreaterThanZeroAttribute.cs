using System.ComponentModel.DataAnnotations;

namespace SurfsupEmil.Models.Validations
{
    public class TotalPriceGreaterThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            // Cast the object instance from the validation context to the type that contains TotalPrice
            var instance = validationContext.ObjectInstance as Surfboard; 

            // Check if the instance and HourlyPrice are not null
            if (instance != null && instance.PriceOfPurchase > 0)
            {
                // If TotalPrice is valid (greater than 0), return success
                return ValidationResult.Success;
            }

            // If TotalPrice is invalid, return an error message
            return new ValidationResult("Price must be greater than 0.");
        }
    }

}
