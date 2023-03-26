using ShoppingList.Objects;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ShoppingList;

public partial class AddProduct : ContentPage
{
    private string connectionLink = "mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000";
    MongoClient client;
    IMongoCollection<Category> categoriesCollection;
    IMongoCollection<Product> productsCollection;

    List<Category> categories;

	public AddProduct()
	{
		InitializeComponent();

        client = new MongoClient(connectionLink);
        categoriesCollection = client.GetDatabase("ShoppingList").GetCollection<Category>("categories");
        productsCollection = client.GetDatabase("ShoppingList").GetCollection<Product>("products");

        categories = fetchCategories();
        updatePickerCategories(); 

    }

    private List<Category> fetchCategories() {
        var filter = Builders<Category>.Filter.Empty;

        List<Category> categories = this.categoriesCollection.Find(filter).ToList();

        return categories;
    }

    private void updatePickerCategories() {
        // var categoriesNames = categories.Select(c => c.name).ToList();

        var picker = category_picker;

        foreach (var category in categories)
        {
            picker.Items.Add(category.name);
        }
    }

    private void AddProductClicked(Object sender, EventArgs e) {
        string categoryName = category_picker.SelectedItem.ToString();
        string productName = product_name.Text;
        string productUnit = product_unit.Text;
        string amountInput = product_amount.Text;
        try {
            int amount = Int32.Parse(amountInput);

            Product product = new(productName, categoryName, amount, productUnit);
            productsCollection.InsertOne(product);
        }catch(FormatException)
        {

        }
    }
}