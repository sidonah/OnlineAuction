using System.ComponentModel.DataAnnotations;

namespace Auction.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? LotId { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual User User { get; set; }

    }
}
