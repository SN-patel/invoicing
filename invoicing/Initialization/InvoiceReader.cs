using invoicing.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace invoicing.Initialization
{
    public interface IBasketItemParser
    {
        BasketItem ParseRaw(string rawValue);
    }

    public class TypeABasketItemParser : IBasketItemParser
    {
        public BasketItem ParseRaw(string rawValue)
        {
            var details = rawValue.Split(' ');
            var b = new BasketItem();
            b.Id = Guid.NewGuid().ToString();
            b.Quantity = Convert.ToInt32(details[0]);
            if (details[1].Equals("imported", StringComparison.OrdinalIgnoreCase))
            {
                b.IsImported = true;
            }

            int i = b.IsImported ? 2 : 1;
            for ( ; i < details.Length; i++)
            {
                if (details[i].Equals("@", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                b.Name = b.Name + " "+ details[i];
            }

            b.Price = float.Parse(details[details.Length-1]);

            return b;
        }
    }

    public interface IBasketItemProvider
    {
        IBasketItemParser _parser { get; set; }
        List<BasketItem> GetBasketItems(string filePath);
    }



    public class BasketItemProvider : IBasketItemProvider
    {
        public IBasketItemParser _parser { get;set; }

        public BasketItemProvider(IBasketItemParser parser)
        {
            _parser = parser;  
        }
        public List<BasketItem> GetBasketItems(string filePath)
        {
            var bItems = new List<BasketItem>();

            using (var fileStream = File.Open(filePath, FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            {
                string line;
                while(( line = reader.ReadLine()) != null){
                    bItems.Add(_parser.ParseRaw(line));
                }
            }

            return bItems;
        }
    }
    public interface IBasketProvider
    {
        IBasketItemProvider _basketItemProvider { get; set; }
        Basket GetBasket(string filePath);
    }

    public class BasketProvider : IBasketProvider
    {
        public IBasketItemProvider _basketItemProvider { get; set; }

        public BasketProvider(IBasketItemProvider provider)
        {
            _basketItemProvider = provider;
        }
        public Basket GetBasket(string filePath)
        {
            return new Basket(_basketItemProvider.GetBasketItems(filePath));
        }
    }
}
