namespace FitnessProgram.Areas.Admin.Models.Partners
{
    using System.ComponentModel.DataAnnotations;
    using static FitnessProgram.Data.DataConstants;

    public class PartnerFormModel
    {
        [Required]
        [StringLength(PartnerConstants.NameMaxLegth)]
        public string Name { get; init; }

        [Required]
        [StringLength(PartnerConstants.DescriptionMaxLegth, MinimumLength =PartnerConstants.DescriptionMinLegth)]
        public string Description { get; init; }

        [Required]
        [Url]
        public string Image { get; init; }

        [Required]
        [Url]
        public string Url { get; init; }

        [Required]
        [StringLength(PartnerConstants.PromoCodeMaxLegth, MinimumLength =PartnerConstants.PromoCodeMinLegth)]
        public string PromoCode { get; init; }
    }
}
