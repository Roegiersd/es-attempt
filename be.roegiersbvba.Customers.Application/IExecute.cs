using be.roegiersbvba.Customers.Dto;
using be.roegiersbvba.Customers.Queries;

namespace be.roegiersbvba.Customers.Application
{
    public interface IExecute<U, out T> where U : IQuery
        where T : IDto
    {
        T Execute(U query);
    }
}
