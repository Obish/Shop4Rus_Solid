using Shop4Rus.Models;
using System.Collections.Generic;

namespace Shop4Rus.Interface
{
    public interface ICreateCustomer
    {
        ReturnMessage<Customers_VM> CreateNewCustomer(Customers_VM customer);

    }
}