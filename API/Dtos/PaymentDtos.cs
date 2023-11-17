using Domain.Entities;

namespace API.Dtos
{
    public class P_PaymentDto
    {
        public DateOnly PaymentDate { get; set; }
        public decimal Total { get; set; }
    }
    public class PaymentDataDto
    {
        public string Id { get; set; }

        public DateOnly PaymentDate { get; set; }

        public decimal Total { get; set; }

        public  P_PaymentMethodDto PaymentMethod { get; set; } 
    }
}