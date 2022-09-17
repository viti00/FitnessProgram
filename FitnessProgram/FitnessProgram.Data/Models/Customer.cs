namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static FitnessProgram.Global.GlobalConstants;

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CustomerConstants.FullNameMaxLength)]
        public string FullName { get; set; }

        [MaxLength(CustomerConstants.PhoneNumberMaxLength)]
        public string? PhoneNumber { get;set; }

        [MaxLength(CustomerConstants.SexMaxLength)]
        public string? Sex { get; set; }

        [Range(CustomerConstants.AgeMinValue, CustomerConstants.AgeMaxValue)]
        public int Age { get; set; }

        [Required]
        [MaxLength(CustomerConstants.DesiredResultMaxLength)]
        public string DesiredResults { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
