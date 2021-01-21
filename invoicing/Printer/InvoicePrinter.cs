using invoicing.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace invoicing.Printer
{
    public interface IInvoicePrinter
    {
        void Print(Basket basket);
    }
    public class InvoicePrinter : IInvoicePrinter
    {
        public void Print(Basket basket)
        {
            Console.WriteLine("NAME            | QTY | Unit Cost | Cost");
            Console.WriteLine("-----------------| ----| ----------| -----");
            basket.BasketItems.ForEach(bi =>
           {
               Console.WriteLine($"{bi.Name}   |{bi.Quantity}|{bi.Price}|{bi.Price * bi.Quantity}");
           });

            Console.WriteLine($"SubTotal: {basket.TotalPrice}");
            Console.WriteLine($"Value Added Tax: {basket.TotalFinalPrice - basket.TotalPrice - basket.TotalAdditionalPrice}");
            Console.WriteLine($"SubTotal: {basket.TotalAdditionalPrice}");
            Console.WriteLine($"SubTotal: {basket.TotalFinalPrice}");
        }
    }
}
