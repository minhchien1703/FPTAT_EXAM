using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example_C_2.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [MaxLength(255)]
        public string FullName { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        public DateTime Registration { get; set; }


    }
}
