using ShoppingList.Objects;
using MongoDB.Driver;
using MongoDB.Bson;
using ShoppingList.Controls;
using System.Linq;

namespace ShoppingList;

public partial class MainPage : ContentPage
{
    private string connectionLink = "mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000";
    MongoClient client;
    IMongoCollection<Product> productsCollection;

    List<Product> products; 

    public MainPage()
	{
		InitializeComponent();

        client = new MongoClient(connectionLink);
        productsCollection = client.GetDatabase("ShoppingList").GetCollection<Product>("products");

        products = fetchProducts();
        updateProductsStack();
    }

    protected override void OnAppearing() {
        products = fetchProducts();
        updateProductsStack();
    }

    private List<Product> fetchProducts()
    {
        var filter = Builders<Product>.Filter.Empty;

        List<Product> products= this.productsCollection.Find(filter).ToList();

        return products;
    }

    private void updateProductsStack()
    {
        var filter = Builders<Product>.Filter.Empty;

        List<Product> products = this.productsCollection.Find(filter).ToList();

        List<Product> orderedProducts = products.OrderBy(p => p.isBought).ToList();

        products_stack.Children.Clear();
        foreach (var product in orderedProducts)
        {
            ProductControl pc = new ProductControl(product);
            products_stack.Children.Add(pc);
        }
    }
}

