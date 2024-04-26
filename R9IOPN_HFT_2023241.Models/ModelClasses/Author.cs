using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace R9IOPN_HFT_2023241.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [StringLength(100)]
        public string Country { get; set; }
        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {

        }
        public string GetFormattedBirthDate()
        {
            return BirthDate.ToString("yyyy-MM-dd");
        }
    }

}
