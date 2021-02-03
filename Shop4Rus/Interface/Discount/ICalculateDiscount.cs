using Shop4Rus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Interface
{
    public interface ICalculateDiscount
    {
        decimal CalculateDiscount(TotalBill order);

    }
}
