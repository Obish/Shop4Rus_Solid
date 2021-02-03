using Shop4Rus.Interface;
using Shop4Rus.Models;
using Shop4Rus.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core.Invoice
{
    public class CalculateDiscountForEachType : IDiscountPoliciesMethods
    {

        public decimal CustomeTypeDiscount(TotalBill order, ReturnMessage<Discounts_VM> GetDiscount)
        {


            if (order.UserType.ToString() == "AffliateCustomer")
            {
                return Convert.ToDecimal(GetDiscount.Body.DiscountPercentage) * order.orders.Where(p => p.ProductCategory != 1).Sum(p => p.Price); ;
            }

            if (order.UserType.ToString() == "Staff")
            {
                return Convert.ToDecimal(GetDiscount.Body.DiscountPercentage) * order.orders.Where(p => p.ProductCategory != 1).Sum(p => p.Price); ;
            }
            else
                return 0m;
        }
        public decimal CustomerLoyaltyDiscount(TotalBill order)
        {
            TimeSpan Span = DateTime.Now - order.DateCreated;
            if ((Span.TotalDays / 365) > Convert.ToInt32(GetConfig.LoyalCustomerYears))
            {
                return (decimal)(Convert.ToInt32(GetConfig.LoyalCustomerPercent) * order.orders.Where(p => p.ProductCategory != 1).Sum(p => p.Price)); ;
            }
            else

                return 0m;
        }

        public decimal BaseDiscount(TotalBill order)
        {
            Decimal TotalAmount = order.orders.Sum(p => p.Price);
            int div = (int)TotalAmount / 100;
            return div * Convert.ToInt32(GetConfig.BaseDiscount);

        }

    }
}
