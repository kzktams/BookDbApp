using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Models
{
    public class UserLoanDetail
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string? UserName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public override bool Equals(object obj)
        {
            UserLoanDetail b = obj as UserLoanDetail;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.BookId == b.BookId
                    && this.BookTitle == b.BookTitle
                    && this.UserName == b.UserName
                    && this.LoanDate == b.LoanDate
                    && this.ReturnDate == b.ReturnDate;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.BookId, this.BookTitle, this.UserName, this.LoanDate, this.ReturnDate);
        }
    }
}
