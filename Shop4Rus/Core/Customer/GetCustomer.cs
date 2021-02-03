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

namespace Shop4Rus.Core.Customer
{
    public class GetCustomer : IGetCustomer 
    {
        public readonly IMapper mapper;
        public readonly ILogger logger;

        //Default Contructor
        public GetCustomer(IMapper Mapper, ILogger Logger)
        {
            mapper = Mapper;
            logger = Logger;
        }

        //Method to get Customers with Customer ID 

        public ReturnMessage<Customers_VM> GetCustomerByID(int ID)
        {
            var GetCustomerResult = new ReturnMessage<Customers_VM>();

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
            var paras = new Dictionary<string, string>
            {
                { "@ID",ID.ToString() }
            };
            var CustomerResult = Repo<Customers>.GetObjectNoParam(dbConnection, paras, "proc_tblCustomers_GetCustomerByID");
            logger.Information($"Response from DB  to get  Customer By ID => {JsonConvert.SerializeObject(CustomerResult)}");

            if (CustomerResult != null)
            {
                var CustomerResult_VM = mapper.Map<Customers, Customers_VM>(CustomerResult);
                CustomerResult_VM.UserType = (Enum.User_Type)(int)CustomerResult.User_Type;
                GetCustomerResult.Body = CustomerResult_VM;
                GetCustomerResult.ResponseCode = "00";
                GetCustomerResult.ResponseDescription = "Success";
            }

            else
            {
                GetCustomerResult.ResponseCode = "25";
                GetCustomerResult.ResponseDescription = "No result found";

            }
            return GetCustomerResult;

        }

        //Method to get Customers with Customer Username 

        public ReturnMessage<Customers_VM> GetCustomerByUsername(string Username)
        {
            var GetCustomerResult = new ReturnMessage<Customers_VM>();

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);
            var paras = new Dictionary<string, string>
            {
                { "@User_Name",Username }
            };
            var CustomerResult = Repo<Models.Customers>.GetObjectNoParam(dbConnection, paras, "proc_tblCustomers_GetCustomerByUserName");
            logger.Information($"Response from DB  to get  Customer By username => {JsonConvert.SerializeObject(CustomerResult)}");

            if (CustomerResult != null)
            {
                var CustomerResult_VM = mapper.Map<Customers, Customers_VM>(CustomerResult);
                CustomerResult_VM.UserType = (Enum.User_Type)(int)CustomerResult.User_Type;

                GetCustomerResult.Body = CustomerResult_VM;

                GetCustomerResult.ResponseCode = "00";
                GetCustomerResult.ResponseDescription = "Success";
            }

            else
            {
                GetCustomerResult.ResponseCode = "25";
                GetCustomerResult.ResponseDescription = "No result found";

            }
            return GetCustomerResult;

        }

        //Method to get all Customers  

        public ReturnMessage<List<Customers_VM>> GetCustomers()
        {
            var GetCustomerResult = new ReturnMessage<List<Customers_VM>>();

            var dbConnection = DatabaseUtilities.GetSQLConnection(GetConfig.ConnectionString);

            var CustomerResult = Repo<Customers>.GetListNoParam(dbConnection, "proc_tblCustomers_GetAllCustomers");
            logger.Information($"Response from DB  to get all  Customers => {JsonConvert.SerializeObject(CustomerResult)}");

            if (CustomerResult != null && CustomerResult.Count > 0)
            {
                List<Customers_VM> CustomerResult_VM = mapper.Map<List<Customers>, List<Customers_VM>>(CustomerResult);
                CustomerResult_VM.Where(g => g.UserType.ToString() != null).Select(g => g.UserType = (User_Type)(int)System.Enum.Parse(typeof(User_Type), g.UserType.ToString()));
                GetCustomerResult.Body = CustomerResult_VM;
                GetCustomerResult.ResponseCode = "00";
                GetCustomerResult.ResponseDescription = "Success";
            }

            else
            {
                GetCustomerResult.ResponseCode = "25";
                GetCustomerResult.ResponseDescription = "No result found";

            }
            return GetCustomerResult;
        }
    }
}
