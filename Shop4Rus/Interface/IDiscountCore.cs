using Shop4Rus.Models;
using System.Collections.Generic;

namespace Shop4Rus.Core
{
    public interface IDiscountCore
    {
        ReturnMessage<Discounts_VM> CreateNewDiscount(Discounts_VM discount);

    }
}