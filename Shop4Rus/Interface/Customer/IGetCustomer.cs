using Shop4Rus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Interface
{
   public  interface IGetCustomer
    {
        ReturnMessage<Customers_VM> GetCustomerByID(int ID);
        ReturnMessage<Customers_VM> GetCustomerByUsername(string Username);
        ReturnMessage<List<Customers_VM>> GetCustomers();
    }
}
