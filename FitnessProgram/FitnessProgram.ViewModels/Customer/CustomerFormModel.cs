namespace FitnessProgram.ViewModels.Customer
{
    using System.ComponentModel.DataAnnotations;
    using static FitnessProgram.Global.GlobalConstants;


    public class CustomerFormModel
    {
        [Required]
        [StringLength(CustomerConstants.FullNameMaxLength, MinimumLength =CustomerConstants.FullNameMinLength)]
        public string FullName { get; init; }

        [StringLength(CustomerConstants.PhoneNumberMaxLength, MinimumLength =CustomerConstants.PhoneNumberMinLength)]
        public string? PhoneNumber { get; init; }

        [StringLength(CustomerConstants.SexMaxLength, MinimumLength = CustomerConstants.SexMinLength)]
        public string? Sex { get; init; }

        [Range(CustomerConstants.AgeMinValue, CustomerConstants.AgeMaxValue)]
        public int Age { get; init; }

        [Required]
        [StringLength(CustomerConstants.DesiredResultMaxLength, MinimumLength =CustomerConstants.DesiredResultMinLength)]
        public string DesiredResults { get; init; }
    }
}
