using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace R9IOPN_HFT_2023241.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public int PublicationYear { get; set; }

        [StringLength(100)]
        public string Genre { get; set; }

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [JsonIgnore]
        public virtual ICollection<Loan> Loans { get; set; }

        public Book()
        {

        }
    }

}
