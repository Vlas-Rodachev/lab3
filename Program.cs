using lab3;

class Program
{
    static void Main(string[] args)
    {
        ProductCatalog catalog = new ProductCatalog("C:\\Users\\roda4\\OneDrive\\Рабочий стол\\lab_c#\\products.bin");

        Product product1 = new Product(1, "Морковка", "Овощи", 20, 40, DateTime.ParseExact("10.04.2025", "dd.MM.yyyy", null), true);
        Product product2 = new Product(2, "Лук", "Овощи", 10, 20, DateTime.ParseExact("15.04.2025", "dd.MM.yyyy", null), true);
        Product product3 = new Product(3, "Яблоко", "Фрукты", 50, 30, DateTime.ParseExact("20.04.2025", "dd.MM.yyyy", null), true);
        Product product4 = new Product(4, "Банан", "Фрукты", 80, 25, DateTime.ParseExact("10.04.2025", "dd.MM.yyyy", null), true);
        Product product5 = new Product(5, "Молоко", "Мочные продукты", 50, 50, DateTime.ParseExact("23.04.2025", "dd.MM.yyyy", null), true);
        Product product6 = new Product(6, "Творог", "Мочные продукты", 90, 40, DateTime.ParseExact("24.04.2025", "dd.MM.yyyy", null), true);
        Product product7 = new Product(7, "Вино", "Напитки", 210, 30, DateTime.ParseExact("10.01.2025", "dd.MM.yyyy", null), true);
        Product product8 = new Product(8, "Балтика 9", "Напитки", 45, 50, DateTime.ParseExact("30.01.2025", "dd.MM.yyyy", null), true);
        catalog.AddProduct(product1);
        catalog.AddProduct(product2);
        catalog.AddProduct(product3);
        catalog.AddProduct(product4);
        catalog.AddProduct(product5);
        catalog.AddProduct(product6);
        catalog.AddProduct(product7);
        catalog.AddProduct(product8);

        catalog.LoadData();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== КАТАЛОГ ПРОДУКЦИИ ===");
            Console.WriteLine("1. Просмотреть все продукты");
            Console.WriteLine("2. Добавить продукт");
            Console.WriteLine("3. Удалить продукт");
            Console.WriteLine("4. Запросы к каталогу");
            Console.WriteLine("5. Выход");


            int choice = IntInputValidator.GetValidIntInput();
            if (choice == 1) {
                Console.WriteLine("\nВсе продукты:");
                catalog.ViewAllProducts();
            }
            else if (choice == 2)
            {
                AddProductMenu(catalog);
            }
            else if (choice == 3)
            { 
                RemoveProductMenu(catalog);
            }
            else if (choice == 4)
            {
                QueryMenu(catalog);
            }
            else if (choice == 5)
            {
                return;
            }
            else 
            { 
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
            }
         


            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    // Добавление продукта
    static void AddProductMenu(ProductCatalog catalog)
    {
        Console.WriteLine("\nДобавление нового продукта:");

        Console.Write("ID: ");
        uint id = uint.Parse(Console.ReadLine());

        Console.Write("Название: ");
        string name = Console.ReadLine();

        Console.Write("Категория: ");
        string category = Console.ReadLine();

        Console.Write("Цена: ");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.Write("Количество на складе: ");
        uint stock = uint.Parse(Console.ReadLine());

        Console.Write("Дата производства (дд.мм.гггг): ");
        DateTime date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);

        Console.Write("Доступен (да/нет): ");
        bool available = Console.ReadLine().ToLower() == "да";

        Product product = new Product(id, name, category, price, stock, date, available);
        catalog.AddProduct(product);
    }

    // удаление продукта
    static void RemoveProductMenu(ProductCatalog catalog)
    {
        Console.WriteLine("\nУдаление продукта:");
        Console.Write("Введите ID продукта для удаления: ");
        int id = int.Parse(Console.ReadLine());
        catalog.RemoveProduct(id);
    }

    // запросы к каталогу
    static void QueryMenu(ProductCatalog catalog)
    {
        Console.WriteLine("\nЗапросы к каталогу:");
        Console.WriteLine("1. Продукты по категории");
        Console.WriteLine("2. Продукты с низким запасом");
        Console.WriteLine("3. Средняя цена продуктов");
        Console.WriteLine("4. Самый старый продукт");
        Console.Write("Выберите запрос: ");

        int queryChoice = int.Parse(Console.ReadLine());

        switch (queryChoice)
        {
            case 1:
                Console.Write("Введите категорию: ");
                string category = Console.ReadLine();
                var categoryProducts = catalog.GetProductsByCategory(category);
                Console.WriteLine($"\nПродукты в категории '{category}':");
                foreach (var p in categoryProducts)
                {
                    Console.WriteLine(p);
                }
                break;
            case 2:
                Console.Write("Введите пороговое количество: ");
                int threshold = int.Parse(Console.ReadLine());
                var lowStockProducts = catalog.GetProductsWithLowStock(threshold);
                Console.WriteLine($"\nПродукты с количеством меньше {threshold}:");
                foreach (var p in lowStockProducts)
                {
                    Console.WriteLine(p);
                }
                break;
            case 3:
                decimal avgPrice = catalog.GetAveragePrice();
                Console.WriteLine($"\nСредняя цена продуктов: {avgPrice:C}");
                break;
            case 4:
                var oldestProduct = catalog.GetOldestProduct();
                Console.WriteLine("\nСамый старый продукт:");
                Console.WriteLine(oldestProduct != null ? oldestProduct.ToString() : "Продукты отсутствуют");
                break;
            default:
                Console.WriteLine("Неверный выбор запроса.");
                break;
        }
    }
}