using System.ComponentModel.DataAnnotations;

namespace api.DataAnnotations{
    public class PermittedValuesAttribute : ValidationAttribute{
        private readonly string[] _permittedValues;
        public PermittedValuesAttribute(String[] permittedValues){
            _permittedValues = permittedValues;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is String strValue){
                if (_permittedValues.Contains(strValue)){
                    #pragma warning disable CS8603 // Possible null reference return.
                    return ValidationResult.Success;
                    #pragma warning restore CS8603 // Possible null reference return.
                }
                else{
                    return new ValidationResult($"This field must be of one of the following options: {string.Join(", ", _permittedValues)}");
                };
            }
            return new ValidationResult("This field must be entered.");
        }
    }
}