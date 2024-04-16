using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class FavMovie
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string MovieName { get; set; }

        [StringLength(255)]
        public string Director { get; set; }

        [StringLength(255)]
        public string Actor { get; set; }

    }
}
