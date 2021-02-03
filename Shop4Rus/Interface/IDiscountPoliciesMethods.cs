using Shop4Rus.Models;

namespace Shop4Rus.Interface
{
    public interface IDiscountPoliciesMethods
    {
        decimal BaseDiscount(TotalBill order);
        decimal CustomerLoyaltyDiscount(TotalBill order);
        decimal CustomeTypeDiscount(TotalBill order);
    }
}