using Shop4Rus.Models;

namespace Shop4Rus.Interface
{
    public interface ICalculateInvoice
    {
        InvoiceAmount ComputePrice(TotalBill order);
    }
}