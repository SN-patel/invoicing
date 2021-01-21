using invoicing.Model;
using consts = invoicing.Constants.Constants;

namespace invoicing.Pricing
{
    public interface IPricingEngine
    {
        Basket UpdateBasketWithPrice(Basket basket);
    }
    class PricingEngine : IPricingEngine
    {
        public Basket UpdateBasketWithPrice(Basket basket)
        {
            var totalPrice = 0F;
            var totalPriceWithVat = 0F;
            var totalAdditionalPrice = 0F;

            basket.BasketItems.ForEach(bi =>
           {
               if (bi.IsImported)
               {
                   bi.FinalPrice = bi.Quantity * bi.Price + bi.Quantity * (bi.Price * (consts.Vat + consts.ImportedVat) / 100);
                   totalAdditionalPrice += bi.Quantity * (bi.Price * consts.ImportedVat / 100);
               }
               else
               {
                   bi.FinalPrice = bi.Quantity * bi.Price + bi.Quantity * (bi.Price * consts.Vat / 100);
               }

               totalPrice += bi.Quantity * bi.Price;
               totalPriceWithVat += bi.FinalPrice;
           });

            basket.TotalPrice = totalPrice;
            basket.TotalFinalPrice = totalPriceWithVat;
            basket.TotalAdditionalPrice = totalAdditionalPrice;
            return basket;
        }
    }
}
