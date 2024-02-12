using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MenuList
{
    public class Menu
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }

        public Menu(string? name, string? category, string? description, double price)
        {
            Name = name;
            Category = category;
            Description = description;
            Price = price;
        }
    }

    public class AllLists
    {
        public List<Menu> Starters = new List<Menu>
        {
            new Menu("Caesar Salad", "Starters", "Fresh romaine lettuce, croutons, parmesan cheese, and Caesar dressing", 8.99),
            new Menu("Garlic Bread", "Starters", "Toasted bread with garlic butter", 5.99),
            new Menu("Caprese Salad", "Starters", "Fresh mozzarella, tomatoes, basil, olive oil, and balsamic glaze", 9.99),

        };

        public List<Menu> Entrees = new List<Menu>
        {
            new Menu("Spaghetti Carbonara", "Entrees", "Spaghetti with creamy sauce, bacon, and Parmesan cheese", 12.99),
            new Menu("Grilled Salmon", "Entrees", "Fresh grilled salmon served with steamed vegetables", 15.99),
            new Menu("Chicken Alfredo", "Entrees", "Fettuccine pasta with creamy Alfredo sauce and grilled chicken", 14.99),

        };

        public List<Menu> Desserts = new List<Menu>
        {
            new Menu("Cheesecake", "Desserts", "Creamy cheesecake with a graham cracker crust", 6.99),
            new Menu("Chocolate Lava Cake", "Desserts", "Warm chocolate cake with a gooey chocolate center", 7.99),
            new Menu("Tiramisu", "Desserts", "Italian dessert made with layers of coffee-soaked ladyfingers and mascarpone cheese", 8.99),
 
        };
    }
}
