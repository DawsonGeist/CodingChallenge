using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation GetById(String id);
        Compensation Add(Compensation employeePay);
        Compensation Remove(Compensation employeePay);
        Task SaveAsync();
    }
}