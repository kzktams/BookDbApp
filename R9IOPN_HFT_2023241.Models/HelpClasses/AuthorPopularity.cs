using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Models
{
    public class AuthorPopularity
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int LoanCount { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AuthorPopularity popularity &&
                   AuthorId == popularity.AuthorId &&
                   AuthorName == popularity.AuthorName &&
                   LoanCount == popularity.LoanCount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AuthorId, AuthorName, LoanCount);
        }

        public override string ToString()
        {
            return ($"AuthorId: {AuthorId}; AuthorName: {AuthorName}; LoanCount: {LoanCount}");
        }
    }
}
