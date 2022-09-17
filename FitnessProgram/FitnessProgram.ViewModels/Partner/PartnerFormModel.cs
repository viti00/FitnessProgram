namespace FitnessProgram.ViewModels.Partner
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static FitnessProgram.Global.GlobalConstants;

    public class PartnerFormModel
    {
        [Required]
        [StringLength(PartnerConstants.NameMaxLegth)]
        public string Name { get; init; }

        [Required]
        [StringLength(PartnerConstants.DescriptionMaxLegth, MinimumLength =PartnerConstants.DescriptionMinLegth)]
        public string Description { get; init; }

        [FromForm]
        [NotMapped]
        public IFormFile File { get; set; }

        [Required]
        [Url]
        public string Url { get; init; }

        [Required]
        [StringLength(PartnerConstants.PromoCodeMaxLegth, MinimumLength =PartnerConstants.PromoCodeMinLegth)]
        public string PromoCode { get; init; }
    }
}
