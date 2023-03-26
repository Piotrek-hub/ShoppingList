using MongoDB.Driver;
using MongoDB.Bson;
using ShoppingList.Objects;
using ShoppingList.Controls.CateogryContol;

namespace ShoppingList;

public partial class AddCategory : ContentPage
{
    private string connectionLink = "mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000";
    MongoClient client;
    IMongoCollection<Category> categoriesCollection;

   
	public AddCategory()
	{
        InitializeComponent();
        
        client = new MongoClient(connectionLink);
        categoriesCollection = client.GetDatabase("ShoppingList").GetCollection<Category>("categories");

        updateCategoriesStack();
    }

    private void AddCategoryClicked(System.Object sender, System.EventArgs e)
    {
        var inputValue = category_name.Text;
        var newCategory = new Category(inputValue);

        categoriesCollection.InsertOne(newCategory);

        updateCategoriesStack();
    }

     private void updateCategoriesStack() {
        var filter = Builders<Category>.Filter.Empty;

        List<Category> categories = this.categoriesCollection.Find(filter).ToList();


        categories_stack.Children.Clear();
        foreach ( var category in categories)
        {
            CategoryControl cc = new CategoryControl(category);
            categories_stack.Children.Add(cc);
        }
    }
}