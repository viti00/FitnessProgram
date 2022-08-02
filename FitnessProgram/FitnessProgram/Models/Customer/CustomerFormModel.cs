using System.ComponentModel.DataAnnotations;
using static FitnessProgram.Data.DataConstants;

namespace FitnessProgram.Models.Customer
{
    public class CustomerFormModel
    {
        [Required]
        [StringLength(CustomerConstants.FirstNameMaxLength, MinimumLength =CustomerConstants.FirstNameMinLength)]
        public string FirstName { get; init; }

        [Required]
        [StringLength(CustomerConstants.LastNameMaxLength, MinimumLength =CustomerConstants.LastNameMinLength)]
        public string LastName { get; init; }

        [Range(CustomerConstants.AgeMinValue, CustomerConstants.AgeMaxValue)]
        public int Age { get; init; }

        [Range(typeof(decimal), CustomerConstants.WeightMinValue, CustomerConstants.WeightMaxValue)]
        public decimal Weight { get; init; }

        [Range(typeof(decimal), CustomerConstants.HeightMinValue, CustomerConstants.HeightMaxValue)]
        public decimal Height { get; init; }

        [Required]
        [StringLength(CustomerConstants.DesiredResultMaxLength, MinimumLength =CustomerConstants.DesiredResultMinLength)]
        public string DesiredResults { get; init; }
    }
}
