using AutoMapper;
using Newtonsoft.Json;
using Serilog;
using Shop4Rus.Enum;
using Shop4Rus.Interface;
using Shop4Rus.Models;
using Shop4Rus.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core
{


    public class DiscountSystem : IDiscountSystem
    {

        public readonly IDiscountPoliciesMethods _discountPoliciesMethods;

        public readonly IMapper mapper;

        public readonly ILogger logger;

        public readonly IGetDiscount getDiscountType;

        public DiscountSystem(IDiscountPoliciesMethods discountPolicy, IMapper Mapper, ILogger Logger, IGetDiscount getDiscount)
        {
            _discountPoliciesMethods = discountPolicy;
            mapper = Mapper;
            logger = Logger;
            this.getDiscountType = getDiscount;
        }


        public InvoiceAmount ComputePrice(TotalBill order)
        {

            var invoiceAmount = new InvoiceAmount();
            var content = (int)System.Enum.Parse(typeof(User_Type), order.UserType);


            var GetDiscount = getDiscountType.GetDiscountByType(content);
            decimal nonDiscounted = order.orders.Sum(p => p.Price);
            decimal baseDiscount = _discountPoliciesMethods.BaseDiscount(order);
            decimal[] discounts = new[] {

                _discountPoliciesMethods.CustomeTypeDiscount(order,GetDiscount),

                _discountPoliciesMethods.CustomerLoyaltyDiscount(order),

            };
            decimal bestDiscount = discounts.Max(discount => discount);

            var totalDiscount = bestDiscount + baseDiscount;

            var total = nonDiscounted - totalDiscount;
            invoiceAmount.InvoicePrice = total.ToString();
            invoiceAmount.DiscountCalculated = totalDiscount.ToString();

            invoiceAmount.UndiscountedPrice = nonDiscounted.ToString();
            logger.Information($"Invoice to be paid  => {JsonConvert.SerializeObject(invoiceAmount)}");

            return invoiceAmount;
        }

    }



}
