using AutoMapper;
using NUnit.Framework;
using Serilog;
using Shop4Rus.Core;

namespace ShopTest
{
    public class Tests
    {
        private readonly ILogger logger;
        private readonly IMapper mapper;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetCustomer()
        {
            var Customer = new CreateCustomer(mapper, logger);
            var CustomerDet = Customer.GetCustomerByID(1);
            Assert.Pass();
        }
    }
}