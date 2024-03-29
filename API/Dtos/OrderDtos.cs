namespace API.Dtos
{
    public class P_OrderDto
    {
        public DateOnly OrderDate { get; set; }

        public DateOnly ExpectedDate { get; set; }

        public DateOnly DeliveryDate { get; set; }

        public string Status { get; set; }

        public string Comments { get; set; }

        public int? CustomerId { get; set; }

        public string? PaymentId { get; set; }

        public int? EmployeeId { get; set; }

    }
    public class OrderStatusDto
    {    
        public string Status { get; set; }

    }

    public class EssencialOrderAtrDto
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public DateOnly ExpectedDate { get; set; }
        public DateOnly? DeliveryDate { get; set; }
    }
}