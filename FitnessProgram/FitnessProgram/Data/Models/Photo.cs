namespace FitnessProgram.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public abstract class Photo
    {
        [Key]
        public int Id { get; set; }

        public byte[] Bytes { get; set; }

        public string Description { get; set; }

        public string FileExtension { get; set; }

        public double Size { get; set; }
    }
}
