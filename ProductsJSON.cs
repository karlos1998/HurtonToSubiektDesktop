using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsJSON
{
    public class ProductData
    {
        [JsonProperty("dataList")]
        public List<Product> DataList { get; set; }
    }

    public class Product
    {
        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("ean")]
        public string Ean { get; set; }

        [JsonProperty("qty")]
        public int Quantity { get; set; }

        [JsonProperty("prices")]
        public ProductPrice Prices { get; set; }
    }

    public class ProductPrice
    {
        [JsonProperty("grossPriceCustomer")]
        public decimal GrossPriceCustomer { get; set; }

        [JsonProperty("netPriceCustomer")]
        public decimal NetPriceCustomer { get; set; }

        [JsonProperty("grossPriceCatalog")]
        public decimal GrossPriceCatalog { get; set; }

        [JsonProperty("netPriceCatalog")]
        public decimal NetPriceCatalog { get; set; }

        [JsonProperty("tax")]
        public int Tax { get; set; }
    }
}
