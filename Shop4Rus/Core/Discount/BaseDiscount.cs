using Shop4Rus.Interface;
using Shop4Rus.Models;
using Shop4Rus.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core.Invoice
{
    public class BaseDiscount: ICalculateDiscount
    {
        public BaseDiscount()
        {

        }

        public decimal CalculateDiscount(TotalBill order)
        {
            Decimal TotalAmount = order.orders.Sum(p => p.Price);
            int div = (int)TotalAmount / 100;
            return div * Convert.ToInt32(GetConfig.BaseDiscount);

        }



    }
}
