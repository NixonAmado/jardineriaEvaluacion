namespace API.Dtos
{
    public class P_PaymentDto
    {
        public DateOnly PaymentDate { get; set; }
        public decimal Total { get; set; }
    }
}