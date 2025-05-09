using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example_C_2.Models
{
    [Table("RentalDetails")]
    public class RentalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalDetailId { get; set; }
        [ForeignKey("RentalId")]
        public int RentalId { get; set; }
        [ForeignKey("ComicBookId")]
        public int ComicBookId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
