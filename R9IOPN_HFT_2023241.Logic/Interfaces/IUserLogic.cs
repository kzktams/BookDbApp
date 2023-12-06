using System.Collections.Generic;
using System.Linq;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Logic
{
    public interface IUserLogic
    {
        void Create(User item);
        void Delete(int id);
        IEnumerable<UserActivity> GetMostActiveUsers();
        User Read(int id);
        IQueryable<User> ReadAll();
        void Update(User item);
    }
}