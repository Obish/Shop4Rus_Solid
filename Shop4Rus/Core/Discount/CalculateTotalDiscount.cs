using Shop4Rus.Interface;
using Shop4Rus.Interface.Discount;
using Shop4Rus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core.Invoice
{
    public class CalculateTotalDiscount: IProcessDiscountTypes
    {
        private readonly IEnumerable<ICalculateDiscount> _discounts;

        public CalculateTotalDiscount(IEnumerable<ICalculateDiscount> discounts)
        {
            _discounts = discounts;
        }
        public CalculateTotalDiscount()
        {

        }

        public decimal GetTotalDiscount(TotalBill order)
        {
            var dis = new List<decimal>();

            foreach (var discount in _discounts)
            {
                decimal des =  discount.CalculateDiscount(order);
                dis.Add(des);
            }

            return dis.Sum();
        }
    }
}
