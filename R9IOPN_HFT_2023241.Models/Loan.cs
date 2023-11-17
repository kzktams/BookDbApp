using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Models
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }

        public int UserId { get; set; } 
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
