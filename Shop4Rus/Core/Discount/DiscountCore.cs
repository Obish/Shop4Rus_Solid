using AutoMapper;
using Newtonsoft.Json;
using Serilog;
using Shop4Rus.Models;
using Shop4Rus.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core.Discount
{
    public class DiscountCore : IDiscountCore
    {
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public DiscountCore(IMapper Mapper, ILogger Logger)
        {
            logger = Logger;
            mapper = Mapper;
        }

        public ReturnMessage<Discounts_VM> CreateNewDiscount(Discounts_VM discount)
        {
            var CreateDiscount = new ReturnMessage<Discounts_VM>();

            var CreateDiscountmap = mapper.Map<Discounts_VM, Discounts>(discount);

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
            var paras = new Dictionary<string, string>
            {
                { "@Discount_Name", CreateDiscountmap.Discount_Name},

                { "@Percentage",CreateDiscountmap.Percentage },

            };


            var returnMessage = Repo<ReturnMessage<Discounts_VM>>.GetObject(dbConnection, paras, "proc_tblDiscounts_AddDiscount",
                CommandType.StoredProcedure);
            logger.Information($"Response from DB  to Creat new  Discounts  => {JsonConvert.SerializeObject(returnMessage)}");

            if (returnMessage.ResponseCode == "1")
            {
                CreateDiscount.ResponseCode = "00";
                CreateDiscount.ResponseDescription = "Success";
                CreateDiscount.Body = discount;
                CreateDiscount.Body.DiscountType = returnMessage.Id;

            }
            else if (returnMessage.ResponseCode == "2")
            {
                CreateDiscount.ResponseCode = "01";
                CreateDiscount.ResponseDescription = $"Unable to save record => {returnMessage.ResponseDescription}";
            }

            else
            {
                CreateDiscount.ResponseDescription = "96";
                CreateDiscount.ResponseDescription = "Unable to save customer Records";
            }

            return CreateDiscount;


        }

    }
}
