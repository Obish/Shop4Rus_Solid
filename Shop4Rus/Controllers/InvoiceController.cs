using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Shop4Rus.Core;
using Shop4Rus.Core.Customer;
using Shop4Rus.Interface;
using Shop4Rus.Models;

namespace Shop4Rus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {


        public readonly IMapper mapper;
        public readonly ILogger logger;
        public readonly IGetCustomer getCustomer;
        public readonly ICalculateInvoice discountSystem;

        public InvoiceController( IMapper Mapper, ILogger Logger, IGetCustomer getCustomer, ICalculateInvoice discountSystem)
        {
          //  discountpolicy = discountPolicy;
            mapper = Mapper;
            logger = Logger;
            this.getCustomer = getCustomer;
            this.discountSystem = discountSystem;
        }

        [HttpPost]
        public ReturnMessage<InvoiceAmount> DiscountPrice (TotalBill Bill)
        {
            logger.Information($"Received request to Calculte TOtal Bill => {JsonConvert.SerializeObject(Bill)}");

            var Billamount = new ReturnMessage<InvoiceAmount>();
            try
            {
                if (ModelState.IsValid)
                {


                    var GetUserDet = getCustomer.GetCustomerByID(Bill.UserId);
                    if (GetUserDet != null)
                    {
                        Bill.UserType = GetUserDet.Body.UserType.ToString();
                        Bill.DateCreated = GetUserDet.Body.DateCreated;
                        var billamount = discountSystem.ComputePrice(Bill);
                        Billamount.Body = billamount;
                        Billamount.ResponseCode = "00";
                        Billamount.ResponseDescription = "Success";
                    }
                    else
                    {
                        Billamount.ResponseCode = GetUserDet.ResponseCode;
                        Billamount.ResponseDescription = GetUserDet.ResponseDescription;

                    }

                    if (Billamount.ResponseCode == "01")

                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    var errorList = (from item in ModelState.Values
                                     from error in item.Errors
                                     select error.ErrorMessage).ToArray();

                    Billamount.ResponseDescription = String.Join('|', errorList);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;

                }


            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error Calculate Invoice Amount ");

                Billamount.ResponseCode = "96";
                Billamount.ResponseDescription = "Unable to Calculate final price";
            }
            return Billamount;
        }

    }
}
