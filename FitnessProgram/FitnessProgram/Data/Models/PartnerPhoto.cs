namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class PartnerPhoto : Photo
    {
        [ForeignKey(nameof(Partner))]
        public int PartnerId { get; set; }

        public virtual Partner Partner { get; set; }
    }
}
