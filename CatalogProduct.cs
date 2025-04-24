

namespace lab3
{
    public class ProductCatalog
    {
        private List<Product> products;
        private readonly string filePath;

        public ProductCatalog(string path)
        {
            filePath = path;
            products = new List<Product>();
        }

        // Загрузка данных из файла
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
                            Product product = new Product
                            {
                                Id = reader.ReadInt32(),
                                Name = reader.ReadString(),
                                Category = reader.ReadString(),
                                Price = reader.ReadDecimal(),
                                StockQuantity = reader.ReadInt32(),
                                ProductionDate = DateTime.FromBinary(reader.ReadInt64()),
                                IsAvailable = reader.ReadBoolean()
                            };
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
        public List<Product> GetProductsByCategory(string category)
        {
            return products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Получить все продукты с количеством на складе меньше указанного
        public List<Product> GetProductsWithLowStock(int threshold)
        {
            return products.Where(p => p.StockQuantity < threshold).ToList();
        }

        // Получить среднюю цену продуктов
        public decimal GetAveragePrice()
        {
            return products.Any() ? products.Average(p => p.Price) : 0;
        }

        // Получить самый старый продукт
        public Product GetOldestProduct()
        {
            return products.OrderBy(p => p.ProductionDate).FirstOrDefault();
        }
    }
}
