namespace CourseManagerInterface.Presentation.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
