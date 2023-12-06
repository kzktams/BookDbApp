using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Models
{
    public class BookDetail
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Genre { get; set; }

        public override bool Equals(object obj)
        {
            BookDetail b = obj as BookDetail;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.BookId == b.BookId
                    && this.Title == b.Title
                    && this.PublicationYear == b.PublicationYear
                    && this.Genre == b.Genre;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.BookId, this.Title, this.PublicationYear, this.Genre);
        }

    }
}
