using AutoMapper;
using Newtonsoft.Json;
using Serilog;
using Shop4Rus.Enum;
using Shop4Rus.Interface;
using Shop4Rus.Models;
using Shop4Rus.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Core.Customer
{
    public class CreateCustomer : ICreateCustomer
    {
        public readonly IMapper mapper;
        public readonly ILogger logger;

        //Default Contructor
        public CreateCustomer(IMapper Mapper, ILogger Logger)
        {
            mapper = Mapper;
            logger = Logger;
        }

        //Method to get Customers with Customer ID 

     
        //Method to create customer
        public ReturnMessage<Customers_VM> CreateNewCustomer(Customers_VM customer)
        {
            var CreateCustomer = new ReturnMessage<Customers_VM>();

            var Creatusermap = mapper.Map<Customers_VM, Customers>(customer);
            Creatusermap.User_Type = (int)customer.UserType;
            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
            var paras = new Dictionary<string, string>
            {
                { "@Email",Creatusermap.Email },

                { "@User_Name",Creatusermap.User_Name },

                { "@First_Name",Creatusermap.First_Name },

                { "@Last_Name",Creatusermap.Last_Name },

                { "@Phone_Number",Creatusermap.Phone_Number },

                { "@User_Type",Creatusermap.User_Type.ToString() },
            };


            var returnMessage = Repo<ReturnMessage<Customers_VM>>.GetObject(dbConnection, paras, "proc_tblCustomers_AddCustomer",
                CommandType.StoredProcedure);
            logger.Information($"Response from DB  to create  Customer  => {JsonConvert.SerializeObject(returnMessage)}");

            if (returnMessage.ResponseCode == "1")
            {
                CreateCustomer.ResponseCode = "00";
                CreateCustomer.ResponseDescription = "Success";
                CreateCustomer.Body = customer;
                CreateCustomer.Body.UserID = returnMessage.Id;

            }
            else if (returnMessage.ResponseCode == "2")
            {
                CreateCustomer.ResponseCode = "01";
                CreateCustomer.ResponseDescription = $"Unable to save record => {returnMessage.ResponseDescription}";
            }

            else
            {
                CreateCustomer.ResponseDescription = "96";
                CreateCustomer.ResponseDescription = "Unable to save customer Records";
            }

            return CreateCustomer;


        }


    }
}
