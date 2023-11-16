using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        [StringLength(100)]
        public string Model { get; set; }
        public int BrandId { get; set; }
        [NotMapped]
        public virtual Brand Brand { get; set; }
        [NotMapped]
        public virtual ICollection<Lease> Leases { get; set; }
    }
}
