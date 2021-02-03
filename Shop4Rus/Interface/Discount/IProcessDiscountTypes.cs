using Shop4Rus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Interface.Discount
{
    public interface IProcessDiscountTypes
    {
        decimal GetTotalDiscount(TotalBill order);
    }
}
