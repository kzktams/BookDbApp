using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R9IOPN_HFT_2023241.Models
{
    public class UserActivity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int LoanCount { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserActivity activity &&
                   UserId == activity.UserId &&
                   Name == activity.Name &&
                   LoanCount == activity.LoanCount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, Name, LoanCount);
        }
    }
}
