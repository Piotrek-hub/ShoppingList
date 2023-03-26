using ShoppingList.Objects;

namespace ShoppingList.Controls.CateogryContol;

public partial class CategoryControl : ContentView
{
	private Category category;

	public CategoryControl()
	{
		InitializeComponent();
	}

    public CategoryControl(Category _category)
    {
        InitializeComponent();

        category = _category;
        CategoryName.Text = category.name;
    }
}