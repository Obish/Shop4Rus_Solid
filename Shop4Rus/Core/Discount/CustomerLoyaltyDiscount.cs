using Shop4Rus.Interface;
using Shop4Rus.Models;
using Shop4Rus.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core.Invoice
{
    public class CustomerLoyaltyDiscount: ICalculateDiscount
    {


        public decimal CalculateDiscount(TotalBill order)
        {
            TimeSpan Span = DateTime.Now - order.DateCreated;
            if ((Span.TotalDays / 365) > Convert.ToInt32(GetConfig.LoyalCustomerYears))
            {
                return (decimal)(Convert.ToInt32(GetConfig.LoyalCustomerPercent) * order.orders.Where(p => p.ProductCategory != 1).Sum(p => p.Price)); ;
            }
            else

               
                return 0m;
        }
    }
}
