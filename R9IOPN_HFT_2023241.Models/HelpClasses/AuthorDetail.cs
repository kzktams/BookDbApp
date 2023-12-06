using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Models
{
    public class AuthorDetail
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public int BookCount { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AuthorDetail detail &&
                   AuthorId == detail.AuthorId &&
                   Name == detail.Name &&
                   BirthDate == detail.BirthDate &&
                   Country == detail.Country &&
                   BookCount == detail.BookCount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AuthorId, Name, BirthDate, Country, BookCount);
        }
    }
}
