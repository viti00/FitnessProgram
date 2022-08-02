namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static FitnessProgram.Data.DataConstants;

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
        public string Image { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(PartnerConstants.PromoCodeMaxLegth)]
        public string PromoCode { get; set; }

        public string CreatorId { get; set; }
    }
}
