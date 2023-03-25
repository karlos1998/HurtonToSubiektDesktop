using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductsXML
{

    [XmlRoot("catalog")]
    public class Catalog
    {
        [XmlElement("date")]
        public string Date { get; set; }
        
        [XmlArray("products")]
        [XmlArrayItem("product", typeof(Product))]
        public Product[] Products { get; set; }
    }

    public class Product
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("sku")]
        public string Sku { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("ean")]
        public string Ean { get; set; }

        [XmlElement("qty")]
        public int Qty { get; set; }

        [XmlElement("prices")]
        public Prices Prices { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("brand")]
        public string Brand { get; set; }

        [XmlElement("categorySecondary")]
        public string CategorySecondary { get; set; }

        [XmlElement("images")]
        public Images Images { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("weight")]
        public double Weight { get; set; }

        [XmlElement("attributes")]
        public Attributes Attributes { get; set; }
    }

    public class Prices
    {
        [XmlElement("grossPriceCustomer")]
        public decimal GrossPriceCustomer { get; set; }

        [XmlElement("netPriceCustomer")]
        public decimal NetPriceCustomer { get; set; }

        [XmlElement("grossPriceCatalog")]
        public decimal GrossPriceCatalog { get; set; }

        [XmlElement("netPriceCatalog")]
        public decimal NetPriceCatalog { get; set; }

        [XmlElement("tax")]
        public int Tax { get; set; }
    }

    public class Images
    {
        [XmlElement("image")]
        public List<string> Image { get; set; }
    }

    public class Attributes
    {
        [XmlElement(ElementName = "attribute")]
        public List<Attribute> Attribute { get; set; }
    }

    public class Attribute
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
