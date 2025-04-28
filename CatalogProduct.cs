using System.Diagnostics;
using System.Xml.Linq;

namespace lab3
{
    /// <summary>
    /// класс для работы с Product
    /// </summary>
    public class ProductCatalog
    {
        private List<Product> products;
        private readonly string filePath;

        /// <summary>
        /// конструктор 
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public ProductCatalog(string path)
        {
            filePath = path;
            products = new List<Product>();
        }

        // Загрузка данных из файла
        /// <summary>
        /// загрузка данных из файла
        /// </summary>
        /// <exception>
        /// если файл невозможно открыть
        ///</exception>
        public void LoadData()
        {
            products = new List<Product>();
            try
            {
                if (File.Exists(filePath))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                    {
                        int count = reader.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            uint id = reader.ReadUInt32();
                            string name = reader.ReadString();
                            string category = reader.ReadString();
                            decimal price = reader.ReadDecimal();
                            uint stockQuantity = reader.ReadUInt32();
                            DateTime productionDate = DateTime.FromBinary(reader.ReadInt64());
                            bool isAvailable = reader.ReadBoolean();

                            // Используем конструктор вместо прямого присваивания
                            Product product = new Product(id, name, category, price, stockQuantity, productionDate, isAvailable);
                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        // перезапись файла с продуктами
        /// <summary>
        /// сохранение продуктов в файл
        /// </summary>
        /// <exception>
        /// если файл невозможно открыть
        ///</exception>
        public void SaveData()
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
                {
                    writer.Write(products.Count);
                    foreach (Product product in products)
                    {
                        writer.Write(product.Id);
                        writer.Write(product.Name);
                        writer.Write(product.Category);
                        writer.Write(product.Price);
                        writer.Write(product.StockQuantity);
                        writer.Write(product.ProductionDate.ToBinary());
                        writer.Write(product.IsAvailable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        // Добавление продукта
        /// <summary>
        /// добавление продукта в список и файл
        /// </summary>
        /// <param name="product">объект, который необходимо добавить</param>
        public void AddProduct(Product product)
        {
            if (products.Any(p => p.Id == product.Id))
            {
                Console.WriteLine("Продукт с таким ID уже существует.");
                return;
            }
            products.Add(product);
            SaveData();
            Console.WriteLine("Продукт успешно добавлен.");
        }

        // Удаление продукта по ID
        /// <summary>
        /// Удаление продукта по ID
        /// </summary>
        /// <param name="id">id продукта который необходимо удалить</param>
        public void RemoveProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
                SaveData();
                Console.WriteLine("Продукт успешно удален.");
            }
            else
            {
                Console.WriteLine("Продукт с указанным ID не найден.");
            }
        }

        // Просмотр всех продуктов
        /// <summary>
        /// вывод всех продуктов на экран
        /// </summary>
        public void ViewAllProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("Каталог продуктов пуст.");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        // Получить все продукты определенной категории
        /// <summary>
        /// получение продуктов определенной категории
        /// </summary>
        /// <param name="category">строка с названием категории</param>
        /// <returns>список продуктов</returns>
        public List<Product> GetProductsByCategory(string category)
        {
            return products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Получить все продукты с количеством на складе меньше указанного
        /// <summary>
        /// возвращает количество продуктов меньше указанного
        /// </summary>
        /// <param name="threshold">граничное количество продуктов</param>
        /// <returns>список продуктов</returns>
        public List<Product> GetProductsWithLowStock(int threshold)
        {
            return products.Where(p => p.StockQuantity < threshold).ToList();
        }

        // Получить среднюю цену продуктов
        /// <summary>
        /// возвращает среднюю цену продуктов
        /// </summary>
        /// <returns>число с плавающей точкой</returns>
        public decimal GetAveragePrice()
        {
            if (!products.Any())
            {
                return 0;
            }

            var prices = from p in products
                         select p.Price;

            return prices.Average();
        }

        // Получить самый старый продукт
        /// <summary>
        /// возращает самый старый продукт
        /// </summary>
        /// <returns>объект Product</returns>
        public Product GetOldestProduct()
        {
            var orderedProducts = from p in products
                                  orderby p.ProductionDate
                                  select p;

            return orderedProducts.FirstOrDefault();
        }
    }
}