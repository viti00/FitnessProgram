namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class BestResultPhoto : Photo
    {
        [ForeignKey(nameof(BestResult))]
        public int? BestResultId { get; set; }

        public virtual BestResult? BestResult { get; set; }

        public string PhotoType { get; set; }

        //the types are before and after
    }
}
