namespace LojinhaDaPaulinhaAPI.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public bool IsAvaliable { get; set; }
    }
}
