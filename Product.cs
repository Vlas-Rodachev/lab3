

namespace lab3
{
    [Serializable]
    public class Product
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public uint StockQuantity { get; set; }
        public DateTime ProductionDate { get; set; }
        public bool IsAvailable { get; set; }

        public Product() { }

        public Product(uint id, string name, string category, decimal price, uint stockQuantity, DateTime productionDate, bool isAvailable)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            StockQuantity = stockQuantity;
            ProductionDate = productionDate;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Название: {Name}, Категория: {Category}, Цена: {Price}, " +
                   $"Количество на складе: {StockQuantity}, Дата производства: {ProductionDate:dd.MM.yyyy}, " +
                   $"Доступен: {(IsAvailable ? "Да" : "Нет")}";
        }
    }
}
