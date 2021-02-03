using Shop4Rus.Interface;
using Shop4Rus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core.Invoice
{
    public class CustomeTypeDiscount: ICalculateDiscount
    {

        public decimal CalculateDiscount(TotalBill order)
        {


            if (order.UserType.ToString() == "AffliateCustomer")
            {
                return Convert.ToDecimal(order.DiscountPercentage) * order.orders.Where(p => p.ProductCategory != 1).Sum(p => p.Price); ;
            }

            if (order.UserType.ToString() == "Staff")
            {
                return Convert.ToDecimal(order.DiscountPercentage) * order.orders.Where(p => p.ProductCategory != 1).Sum(p => p.Price); ;
            }
            else
                return 0m;
        }
    }
}
