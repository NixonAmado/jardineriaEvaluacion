using Domain.Entities;

namespace API.Dtos
{
    public class P_AddressDto
    {

    public string AdressLine1 { get; set; } = null!;

    public string AddressLine2 { get; set; } = null!;

    public string PostalCode { get; set; } = null!;
    public virtual Country? Country { get; set; }
    }
}