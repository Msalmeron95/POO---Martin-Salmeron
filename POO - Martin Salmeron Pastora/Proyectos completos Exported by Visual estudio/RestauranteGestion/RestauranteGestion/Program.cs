using MenuList;

namespace RestauranteGestion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AllLists allLists = new AllLists();
            Methods methods = new Methods();
            List<Menu> allOrders = new List<Menu>();

            int Options = 0;
            do 
            {
                Options = methods.MainMenu();
                switch (Options)
                {
                    case 1:
                        methods.GreenText("\nDisplaying all menus");
                        methods.DisplayAllMenus(allLists);
                        Console.WriteLine("");
                        break;

                    case 2:
                        methods.AddMenuItem(allLists);
                        break;

                    case 3:
                        methods.EditMenuItem(allLists);
                        break;

                    case 4:
                        methods.DeleteMenuItem(allLists);
                        break;

                    case 5:
                        methods.PlaceCalculateTotalCost(allLists, allOrders); 
                        break;

                    case 6:
                        methods.CountMostSoldDishes(allOrders); 
                        break;

                    case 7:
                        Console.WriteLine("Exit the program...");
                        break;

                    default:
                        methods.RedText("Invalid input...");
                        break;
                }

            } while (Options != 7);
        }
    }


    public class Methods 
    {
        public int MainMenu()
        {
            GreenText("************Welcome to UNIVO's RESTAURANT************");
            Console.WriteLine("1. See all the items of the Menu");
            Console.WriteLine("2. Add an dish to a category");
            Console.WriteLine("3. Edit an dish");
            Console.WriteLine("4. Delete an dish");
            Console.WriteLine("5. Place an order");
            Console.WriteLine("6. Common sold dishes");
            Console.WriteLine("7. Exit the program...");

            int Choice = WholeNumVerification();

            return Choice;
        }
        public void GreenText(string? PlainText) 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(PlainText);
            Console.ResetColor();
        }
        public void RedText(string? PlainText) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(PlainText);
            Console.ResetColor();
        }
        public int WholeNumVerification()
        {
            int num;
            while (true)
            {
                Console.Write("\nSelect one of the options of the menu: ");
                if (int.TryParse(Console.ReadLine(), out num))
                {
                    break;
                }
                else
                {
                    RedText("\nInvalid input. Please enter a whole number.");
                }
            }
            return num;
        }
        public int WholeNumVerification(int min, int max)
        {
            int num;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out num) && num >= min && num <= max)
                {
                    break;
                }
                else
                {
                    RedText($"\nInvalid input. Please enter a whole number between {min} and {max}.\n");
                    Console.Write("Select a category: ");
                }
            }
            return num;
        }
        public void DisplayAllMenus(AllLists allLists)
        {
            int dishNumber = 1;

            Console.WriteLine("\nStarters:");
            foreach (Menu item in allLists.Starters)
            {
                Console.WriteLine($"{dishNumber++}. Name: {item.Name}, Description: {item.Description}, Price: ${item.Price}");
            }

            Console.WriteLine("\nEntrees:");
            foreach (Menu item in allLists.Entrees)
            {
                Console.WriteLine($"{dishNumber++}. Name: {item.Name}, Description: {item.Description}, Price: ${item.Price}");
            }

            Console.WriteLine("\nDesserts:");
            foreach (Menu item in allLists.Desserts)
            {
                Console.WriteLine($"{dishNumber++}. Name: {item.Name}, Description: {item.Description}, Price: ${item.Price}");
            }
        }
        public void AddMenuItem(AllLists allLists)
        {
            int categoryOption = 0;
            while (true) 
            {
                Console.WriteLine("\nSubmenu for the Categories");
                Console.WriteLine("1. Starters");
                Console.WriteLine("2. Entrees");
                Console.WriteLine("3. Desserts");
                Console.WriteLine("4. Exit...");
                Console.Write("\nSelect one of the categories availables where you want to add the dish: ");

                if (int.TryParse(Console.ReadLine(), out categoryOption))
                {
                    if (categoryOption >= 1 && categoryOption <= 3)
                    {
                        break;
                    }
                    else if(categoryOption == 4)
                    {
                        return;
                    }
                    else
                    {
                        RedText("\nInvalid input. Please enter a number between 1 and 3:");
                    }
                }
                else
                {
                    RedText("\nInvalid input. Please enter a whole number:");
                }
            }

            string? category = categoryOption switch
            {
                1 => "Starters",
                2 => "Entrees",
                3 => "Desserts",
                4 => "Exit...",
                
            };

            Console.Write("\nEnter the name of the dish: ");
            string? name = Console.ReadLine();

            Console.Write("Enter the description of the dish: ");
            string? description = Console.ReadLine();

            Console.Write("Enter the price of the dish: $");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                RedText("\nInvalid input for price. Please enter a valid number:");
                Console.Write("\nEnter the price of the dish again: $");
            }

            Menu newItem = new Menu(name, category, description, price);

            switch (category?.ToLower())
            {
                case "starters":
                    allLists.Starters.Add(newItem);
                    break;
                case "entrees":
                    allLists.Entrees.Add(newItem);
                    break;
                case "desserts":
                    allLists.Desserts.Add(newItem);
                    break;
                default:
                    throw new InvalidOperationException("Invalid category option.");
            }

            GreenText($"\n{name} has been added to the {category} menu.\n");
        }
        public void EditMenuItem(AllLists allLists)
        {
            Console.WriteLine("\nWhich category would you like to edit?");
            Console.WriteLine("1. Starters");
            Console.WriteLine("2. Entrees");
            Console.WriteLine("3. Desserts");
            Console.WriteLine("4. Exit...");
            Console.Write("Select a category: ");

            int categoryOption = WholeNumVerification(1, 4);

            if (categoryOption == 4)
            {
                return; 
            }

            List<Menu> selectedCategoryList = categoryOption switch
            {
                1 => allLists.Starters,
                2 => allLists.Entrees,
                3 => allLists.Desserts,
                _ => throw new InvalidOperationException("Invalid category option.")
            };

            if (selectedCategoryList.Count == 0)
            {
                Console.WriteLine("The selected category has no items to edit.");
                return;
            }

            for (int i = 0; i < selectedCategoryList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectedCategoryList[i].Name} - {selectedCategoryList[i].Description} - ${selectedCategoryList[i].Price}");
            }

            Console.Write("Select an item to edit: ");
            int itemOption = WholeNumVerification(1, selectedCategoryList.Count) - 1;

            Menu itemToEdit = selectedCategoryList[itemOption];
            Console.WriteLine("What would you like to edit?");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Description");
            Console.WriteLine("3. Price");
            Console.WriteLine("4. All");
            Console.Write("Select an option: ");
            int editOption = WholeNumVerification(1, 4);
            double newPrice;
            double newAllPrice;
            switch (editOption)
            {
                case 1:
                    Console.Write("Enter the new name: ");
                    itemToEdit.Name = Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Enter the new description: ");
                    itemToEdit.Description = Console.ReadLine();
                    break;
                case 3:
                    Console.Write("Enter the new price: $");
                    while (!double.TryParse(Console.ReadLine(), out newPrice))
                    {
                        RedText("\nInvalid input for price. Please enter a valid number:");
                    }
                    itemToEdit.Price = newPrice;
                    break;
                case 4:
                    Console.Write("Enter the new name: ");
                    itemToEdit.Name = Console.ReadLine();
                    Console.Write("Enter the new description: ");
                    itemToEdit.Description = Console.ReadLine();
                    Console.Write("Enter the new price: $");
                    while (!double.TryParse(Console.ReadLine(), out newAllPrice))
                    {
                        RedText("\nInvalid input for price. Please enter a valid number:");
                    }
                    itemToEdit.Price = newAllPrice;
                    break;
            }

            GreenText($"\nThe item '{itemToEdit.Name}' has been updated.\n");
        }
        public void DeleteMenuItem(AllLists allLists)
        {
            Console.WriteLine("\nWhich category would you like to delete from?");
            Console.WriteLine("1. Starters");
            Console.WriteLine("2. Entrees");
            Console.WriteLine("3. Desserts");
            Console.WriteLine("4. Exit");
            Console.Write("Select a category: ");
            int categoryOption = WholeNumVerification(1, 4);

            if (categoryOption == 4)
            {
                return; 
            }

            List<Menu> selectedCategoryList = categoryOption switch
            {
                1 => allLists.Starters,
                2 => allLists.Entrees,
                3 => allLists.Desserts,
                _ => throw new InvalidOperationException("Invalid category option.")
            };

            if (selectedCategoryList.Count == 0)
            {
                Console.WriteLine("The selected category has no items to delete.");
                return;
            }

            for (int i = 0; i < selectedCategoryList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectedCategoryList[i].Name} - {selectedCategoryList[i].Description} - ${selectedCategoryList[i].Price}");
            }

            Console.Write("Select an item to delete: ");
            int itemOption = WholeNumVerification(1, selectedCategoryList.Count) - 1;

            Menu itemToDelete = selectedCategoryList[itemOption];
            Console.Write($"\nAre you sure you want to delete '{itemToDelete.Name}'? (Y/N):");
            string? confirm = Console.ReadLine();
            if (confirm?.ToLower() == "y")
            {
                selectedCategoryList.RemoveAt(itemOption);
                GreenText($"\nThe item '{itemToDelete.Name}' has been deleted.\n");
            }
            else
            {
                RedText("\nDeletion canceled.\n");
            }
        }
        public void PlaceCalculateTotalCost(AllLists allLists, List<Menu> allOrders)
        {
            List<Menu> selectedDishes = new List<Menu>();

            Console.Write("\nEnter your name: ");
            string? name = Console.ReadLine(); 

            Console.Write("Enter the number of table: ");

            int table;
            while (true) 
            {
                if (int.TryParse(Console.ReadLine(), out table)) 
                {
                    break; 
                }
                else
                {
                    RedText("\nInvalid input. Please enter a valid integer.");
                    Console.Write("\nEnter the number of table again: ");
                }
            }

            GreenText("\nSelect dishes to add to your order from the menu. Enter '100' when finished.\n");

            while (true)
            {
                GreenText("\n*********UNIVO's RESTAURANT MENU*********");
                DisplayAllMenus(allLists);
                Console.Write("\nSelect a dish from the menu to add (or enter '100' to finish):");

                string? input = Console.ReadLine();
                if (input?.ToLower() == "100")
                    break;

                if (int.TryParse(input, out int dishNumber))
                {

                    if (dishNumber >= 1 && dishNumber <= allLists.Starters.Count + allLists.Entrees.Count + allLists.Desserts.Count)
                    {

                        if (dishNumber <= allLists.Starters.Count)
                        {
                            selectedDishes.Add(allLists.Starters[dishNumber - 1]);
                        }
                        else if (dishNumber <= allLists.Starters.Count + allLists.Entrees.Count)
                        {
                            selectedDishes.Add(allLists.Entrees[dishNumber - allLists.Starters.Count - 1]);
                        }
                        else
                        {
                            selectedDishes.Add(allLists.Desserts[dishNumber - allLists.Starters.Count - allLists.Entrees.Count - 1]);
                        }
                    }
                    else
                    {
                        RedText("\nInvalid dish number. Please try again.");
                    }
                }
                else
                {
                    RedText("Invalid input. Please enter a valid dish number or 'done' to finish.");
                }
            }

            if (CalculateDiscount() != 0)
            {
                double totalCost = Math.Round(selectedDishes.Sum(dish => dish.Price), 2);
                double discount = CalculateDiscount();
                double discountedPrice = totalCost * (1 - discount); 

                Console.WriteLine("\nSelected Dishes:");
                foreach (var dish in selectedDishes)
                {
                    Console.WriteLine($"Name: {dish.Name}, Description: {dish.Description}, Price: ${dish.Price}");
                }

                Console.WriteLine($"\nReceipt under the name of: {name}\nTable #: {table}");

                // Display total cost with discount
                Console.WriteLine($"Total Cost: ${totalCost}");
                Console.WriteLine($"Discount: {discount * 100}%");
                Console.WriteLine($"Discounted Price: ${discountedPrice}\n");
            }
            else
            {
                double totalCost = Math.Round(selectedDishes.Sum(dish => dish.Price), 2);

                Console.WriteLine("\nSelected Dishes:");
                foreach (var dish in selectedDishes)
                {
                    Console.WriteLine($"Name: {dish.Name}, Description: {dish.Description}, Price: ${dish.Price}");
                }
                Console.WriteLine("\nReceipt under the name of: " + name + "\nTable #:" + table);
                Console.WriteLine($"Total Cost: ${totalCost}\n");
            }

            allOrders.AddRange(selectedDishes);
        }
        public void CountMostSoldDishes(List<Menu> allOrders)
        {
            var dishCount = new Dictionary<string, int>();

            // Count occurrences of each dish
            foreach (var dish in allOrders)
            {
                if (!dishCount.ContainsKey(dish.Name))
                {
                    dishCount[dish.Name] = 1;
                }
                else
                {
                    dishCount[dish.Name]++;
                }
            }

            var top3Dishes = dishCount.OrderByDescending(pair => pair.Value).Take(3);

            GreenText("\nTop 3 Most Sold Dishes:");
            foreach (var pair in top3Dishes)
            {
                Console.WriteLine($"Dish: {pair.Key}, Sold: {pair.Value} times");
            }
            Console.WriteLine();
        }
        public static double CalculateDiscount()
        {
            DayOfWeek currentDay = DateTime.Today.DayOfWeek;

            switch (currentDay)
            {
                case DayOfWeek.Monday:
                    return 0.10; 
                case DayOfWeek.Tuesday:
                    return 0.15; 
                case DayOfWeek.Wednesday:
                    return 0.20; 
                case DayOfWeek.Thursday:
                    return 0.25; 
                case DayOfWeek.Friday:
                    return 0.30; 
                case DayOfWeek.Saturday:
                    return 0.35;
                case DayOfWeek.Sunday:
                    return 0.40;
                default:
                    return 0.0; 
            }
        }

    }
}
