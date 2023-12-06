using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Models
{
    public class BookLoanCount
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int LoanCount { get; set; }

        public override bool Equals(object obj)
        {
            BookLoanCount b = obj as BookLoanCount;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.BookId == b.BookId
                    && this.Title == b.Title
                    && this.LoanCount == b.LoanCount;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.BookId, this.Title, this.LoanCount);
        }

        public override string ToString()
        {
            return $"BookId: {BookId}; Title: {Title}; LoanCount: {LoanCount}";
        }
    }
}
