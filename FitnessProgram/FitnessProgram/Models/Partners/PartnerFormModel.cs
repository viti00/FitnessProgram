using FitnessProgram.Data;
using System.ComponentModel.DataAnnotations;

namespace FitnessProgram.Models.Partners
{
    public class PartnerFormModel
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [StringLength(DataConstants.DescriptionMaxLegth, MinimumLength =DataConstants.DescriptionMaxLegth)]
        public string Description { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [Url]
        public string Url { get; init; }

        [Required]
        public string PromoCode { get; init; }
    }
}
