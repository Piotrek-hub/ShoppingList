using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingList.Objects
{
    public class Product
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("category")]
        public string category { get; set; }

        [BsonElement("amount")]
        public int amount { get; set; }
        [BsonElement("unit")]
        public string unit { get; set; }

        [BsonElement("isBought")]
        public bool isBought { get; set; }

        public Product()
        {

        }

        public Product(string _name, string _category, int _amount, string _unit)
        {
            name = _name;
            category = _category;
            amount = _amount;
            isBought = false;
            unit = _unit;
        }
    }
}
