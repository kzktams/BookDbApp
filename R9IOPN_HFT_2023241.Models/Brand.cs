using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace R9IOPN_HFT_2023241.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId{ get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [NotMapped]
        public virtual ICollection<Car> Cars { get; set; }
    }
}
