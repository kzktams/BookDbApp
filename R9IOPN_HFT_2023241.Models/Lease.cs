using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Models
{
    public class Lease
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaseId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal LeaseAmount { get; set; }
        public string LesseeName { get; set; }
        public bool IsActive { get; set; }

        public int CarId { get; set; }

        [NotMapped]
        public virtual Car Cars { get; set; }
    }
}
