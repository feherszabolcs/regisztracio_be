using System.ComponentModel.DataAnnotations;

namespace regisztracio_be.Dto
{
    public class VehiclePostDTO
    {
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
