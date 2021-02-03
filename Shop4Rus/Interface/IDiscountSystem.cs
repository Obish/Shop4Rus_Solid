using Shop4Rus.Models;

namespace Shop4Rus.Interface
{
    public interface IDiscountSystem
    {
        InvoiceAmount ComputePrice(TotalBill order);
    }
}