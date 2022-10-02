using System.ComponentModel.DataAnnotations;

namespace Auction.Models
{
    public class Interest
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
    }
}
