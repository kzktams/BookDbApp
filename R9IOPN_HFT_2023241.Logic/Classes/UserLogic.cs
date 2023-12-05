using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R9IOPN_HFT_2023241.Models;
using R9IOPN_HFT_2023241.Repository;

namespace R9IOPN_HFT_2023241.Logic
{
    public class UserLogic : IUserLogic
    {
        IRepository<User> _userRepository;
        IRepository<Loan> _loanRepository;

        public UserLogic(IRepository<User> userRepository, IRepository<Loan> loanRepository)
        {
            _userRepository = userRepository;
            _loanRepository = loanRepository;
        }
        public void Create(User item)
        {
            _userRepository.Create(item);
        }

        public void Delete(int id)
        {
            var user = _userRepository.Read(id);
            if (user == null)
            {
                throw new InvalidOperationException("User isn't found");
            }
            _userRepository.Delete(id);
        }

        public User Read(int id)
        {
            return _userRepository.Read(id);
        }

        public IQueryable<User> ReadAll()
        {
            return _userRepository.ReadAll();
        }

        public void Update(User item)
        {
            _userRepository.Update(item);
        }


        public IEnumerable<UserActivity> GetMostActiveUsers()
        {
            var userLoanCounts = _loanRepository.ReadAll()
                                                .GroupBy(loan => loan.UserId)
                                                .Select(group => new
                                                {
                                                    UserId = group.Key,
                                                    LoanCount = group.Count()
                                                })
                                                .OrderByDescending(x => x.LoanCount)
                                                .Take(10)
                                                .ToList();

            var userActivities = userLoanCounts.Select(userLoanCount =>
            new UserActivity
            {
                UserId = userLoanCount.UserId,
                Name = _userRepository.ReadAll().FirstOrDefault(u => u.UserId == userLoanCount.UserId)?.Name,
                LoanCount = userLoanCount.LoanCount
            });
            return userActivities;
        }

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
    
}
