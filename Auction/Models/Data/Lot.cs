using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.Models
{
    public class Lot
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Availability Status")]
        public int AvailabilityStatus { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Picture { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }

    }
}
