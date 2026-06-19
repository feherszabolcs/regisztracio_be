using System.ComponentModel.DataAnnotations;

namespace regisztracio_be.Models
{
    public class Vehicle
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int BuildYear { get; set; }
        [Required]
        public string Owner { get; set; }
    }
}
