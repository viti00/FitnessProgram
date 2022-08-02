namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static FitnessProgram.Data.DataConstants;

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CustomerConstants.FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(CustomerConstants.LastNameMaxLength)]
        public string LastName { get; set; }

        [Range(CustomerConstants.AgeMinValue, CustomerConstants.AgeMaxValue)]
        public int Age { get; set; }

        [Range(typeof(decimal), CustomerConstants.WeightMinValue, CustomerConstants.WeightMaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Weight { get; set; }

        [Range(typeof(decimal), CustomerConstants.HeightMinValue, CustomerConstants.HeightMaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Height { get; set; }

        [Required]
        [MaxLength(CustomerConstants.DesiredResultMaxLength)]
        public string DesiredResults { get; set; }

        public string UserId { get; set; }
    }
}
