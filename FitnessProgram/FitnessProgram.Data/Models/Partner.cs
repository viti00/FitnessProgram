namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static FitnessProgram.Global.GlobalConstants;

    public class Partner
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PartnerConstants.NameMaxLegth)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PartnerConstants.DescriptionMaxLegth)]
        public string Description { get; set; }

        [Required]
        public PartnerPhoto Photo { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(PartnerConstants.PromoCodeMaxLegth)]
        public string PromoCode { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
