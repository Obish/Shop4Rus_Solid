using Shop4Rus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop4Rus.Interface
{
    public interface IGetDiscount
    {
        ReturnMessage<List<Discounts_VM>> GetAllDiscounts();
        ReturnMessage<Discounts_VM> GetDiscountByType(int Type);
    }
}
