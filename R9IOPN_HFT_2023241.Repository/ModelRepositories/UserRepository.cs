using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(BookDbContext ctx) : base(ctx)
        {
        }

        public override User Read(int id)
        {
            return context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public override void Update(User item)
        {
            var existingUser = Read(item.UserId);
            if (existingUser == null)
            {
                throw new InvalidOperationException("The user cannot be found in our currand database");
            }

            context.Entry(existingUser).CurrentValues.SetValues(item);
            context.SaveChanges();
        }
    }
}
