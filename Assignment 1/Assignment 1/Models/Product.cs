namespace Assignment_1.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public int Price { get; set; }
        public virtual Company Company { get; set; }
    }
}
