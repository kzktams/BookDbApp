using System.Linq;
using R9IOPN_HFT_2023241.Models;

namespace R9IOPN_HFT_2023241.Logic
{
    public interface ILoanLogic
    {
        void Create(Loan item);
        void Delete(int id);
        Loan Read(int id);
        IQueryable<Loan> ReadAll();
        void Update(Loan item);
    }
}