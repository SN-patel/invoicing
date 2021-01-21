using System;
using System.Collections.Generic;
using System.Text;

namespace invoicing.Model
{
    public class Basket
    {
        public List<BasketItem> BasketItems { get; set; }
        public float TotalPrice { get; set; }
        public float TotalFinalPrice { get; set; }
        public float TotalAdditionalPrice { get; set; }
        public Basket(List<BasketItem> basketItems)
        {
            BasketItems = basketItems;
        }

        public bool AddBasketItem(BasketItem item)
        {
            if(BasketItems.Exists( bi => bi.Id.Equals(item.Id, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            BasketItems.Add(item);
            return true;
        }   
    }

    public class BasketItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float FinalPrice { get; set; }
        public bool IsImported { get; set; }
        public Dictionary<string,object> Attributes { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
