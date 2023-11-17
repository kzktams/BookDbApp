﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace R9IOPN_HFT_2023241.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {

        }
    }

}