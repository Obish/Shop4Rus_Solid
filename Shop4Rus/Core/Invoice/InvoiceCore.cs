using AutoMapper;
using Newtonsoft.Json;
using Serilog;
using Shop4Rus.Enum;
using Shop4Rus.Interface;
using Shop4Rus.Interface.Discount;
using Shop4Rus.Models;
using Shop4Rus.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core
{


    public class DiscountSystem : ICalculateInvoice
    {

       /// public readonly IDiscountPoliciesMethods _discountPoliciesMethods;

        public readonly IProcessDiscountTypes discountMethodProcessor;

        public readonly IMapper mapper;

        public readonly ILogger logger;

        public readonly IGetDiscount getDiscountType;

        public DiscountSystem(IMapper Mapper, ILogger Logger, IGetDiscount getDiscount, IProcessDiscountTypes discountMethodProcessor)
        {
         //   _discountPoliciesMethods = discountPolicy;
            mapper = Mapper;
            logger = Logger;
            this.getDiscountType = getDiscount;
            this.discountMethodProcessor = discountMethodProcessor;
        }


        public InvoiceAmount ComputePrice(TotalBill order)
        {

            var invoiceAmount = new InvoiceAmount();
            var content = (int)System.Enum.Parse(typeof(User_Type), order.UserType);


            var GetDiscount = getDiscountType.GetDiscountByType(content);
            order.DiscountPercentage = GetDiscount.Body.DiscountPercentage;
            decimal nonDiscounted = order.orders.Sum(p => p.Price);

            #region
            //OLD IMPLEMENTATION

            //decimal baseDiscount = _discountPoliciesMethods.BaseDiscount(order);
            //decimal[] discounts = new[] {

            //    _discountPoliciesMethods.CustomeTypeDiscount(order),

            //    _discountPoliciesMethods.CustomerLoyaltyDiscount(order),

            //};
            //decimal bestDiscount = discounts.Max(discount => discount);

            //var totalDiscount = bestDiscount + baseDiscount;

            #endregion
            var totalDiscount2 = discountMethodProcessor.GetTotalDiscount(order);
            var total = nonDiscounted - totalDiscount2;
            invoiceAmount.InvoicePrice = total.ToString();
            invoiceAmount.DiscountCalculated = totalDiscount2.ToString();

            invoiceAmount.UndiscountedPrice = nonDiscounted.ToString();
            logger.Information($"Invoice to be paid  => {JsonConvert.SerializeObject(invoiceAmount)}");

            return invoiceAmount;
        }

    }



}
