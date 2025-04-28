namespace lab3
{
    /// <summary>
    /// класс содержащий данные о продукте
    /// </summary>
    [Serializable]
    public class Product
    {
        /// <summary>
        /// поля состояния продуктов
        /// </summary>
        private uint id;
        private string name;
        private string category;
        private decimal price;
        private uint stockQuantity;
        private DateTime productionDate;
        private bool isAvailable;

        internal uint Id => id;
        internal string Name => name;
        internal string Category => category;
        internal decimal Price => price;
        internal uint StockQuantity => stockQuantity;
        internal DateTime ProductionDate => productionDate;
        internal bool IsAvailable => isAvailable;

        internal Product() { }
        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="id">id продукта</param>
        /// <param name="name">имя продукта</param>
        /// <param name="category">категория продукта</param>
        /// <param name="price">цена продукта</param>
        /// <param name="stockQuantity">кол-во на складе</param>
        /// <param name="productionDate">дата изготовления</param>
        /// <param name="isAvailable">доступность</param>
        internal Product(uint id, string name, string category, decimal price, uint stockQuantity, DateTime productionDate, bool isAvailable)
        {
            this.id = id;
            this.name = name;
            this.category = category;
            this.price = Math.Abs(price);
            this.stockQuantity = stockQuantity;
            this.productionDate = productionDate;
            this.isAvailable = isAvailable;
        }

        /// <summary>
        /// вывод данных продукта
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID: {id}, Название: {name}, Категория: {category}, Цена: {price}, " +
                   $"Количество на складе: {stockQuantity}, Дата производства: {productionDate:dd.MM.yyyy}, " +
                   $"Доступен: {(isAvailable ? "Да" : "Нет")}";
        }
    }
}