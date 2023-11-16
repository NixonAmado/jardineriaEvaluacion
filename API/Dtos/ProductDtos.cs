namespace API.Dtos
{
    public class P_ProductDto
    {
    
        public string Name { get; set; }

        public string? Gama { get; set; }

        public string Supplier { get; set; }

        public string Dimentions { get; set; }

        public string Description { get; set; }

        public decimal SalePrice { get; set; }

        public decimal SupplierPrice { get; set; }

    }
}