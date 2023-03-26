using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingList.Objects
{
    public class Category
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }


        public Category()
        {

        }

        public Category(string _name)
        {
            name = _name;
        }
    }
}
