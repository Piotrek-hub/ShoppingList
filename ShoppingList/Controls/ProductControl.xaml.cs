using ShoppingList.Objects;
using static MongoDB.Driver.WriteConcern;
using System.Xml.Linq;
using MongoDB.Driver;

namespace ShoppingList.Controls;

public partial class ProductControl : ContentView
{
    private string connectionLink = "mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000";
    MongoClient client;
    IMongoCollection<Product> productsCollection;

    Product product;

	public ProductControl()
	{
		InitializeComponent();
	}

	public ProductControl(Product p) {
        client = new MongoClient(connectionLink);
        productsCollection = client.GetDatabase("ShoppingList").GetCollection<Product>("products");

        product = p;

        InitializeComponent();

        product_name.Text = p.name;
        product_category.Text = p.category;
        product_amount.Text = p.amount.ToString();
        product_unit.Text = p.unit;

        if (p.isBought) {
            product_name.TextColor = new Color(00, 255, 00);
        }
    }

    private void RemoveProductClicked(System.Object sender, System.EventArgs e)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);

        productsCollection.DeleteOne(filter);
    }

    private void ProductBoughtClicked(System.Object sender, System.EventArgs e)
    {
        var filter = Builders<Product>.Filter
                .Eq(p => p.Id, product.Id);

        var update = Builders<Product>.Update.Set(p => p.isBought, true);

        productsCollection.UpdateOne(filter, update);
    }
}