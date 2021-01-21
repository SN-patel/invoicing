using invoicing.Initialization;
using invoicing.Model;
using invoicing.Printer;
using invoicing.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace invoicing
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"E:\Talentica\invoice\file\invoice_typeA.txt";

            //below three lines can be handled using dependency injection
            IBasketItemParser parser = new TypeABasketItemParser();
            IBasketItemProvider basketItemProvider = new BasketItemProvider(parser);
            IBasketProvider basketProvider = new BasketProvider(basketItemProvider);

            var basket = basketProvider.GetBasket(filePath);

            IPricingEngine engine = new PricingEngine();

            basket = engine.UpdateBasketWithPrice(basket);

            IInvoicePrinter printer = new InvoicePrinter();
            printer.Print(basket);

            Console.WriteLine(basket.BasketItems.First().Name);

        }
    }

    
}
