using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //CreateMap<Role, RoleDto>().ReverseMap();
        //CreateMap<PersonType, TypePDto>().ReverseMap();
        // CreateMap<Pet, FullPetDto>().ReverseMap();
        // CreateMap<Pet, PetStatDto>()
        // .ForMember(e => e.Breed, op => op.MapFrom(e => e.Breed.Name))
        // .ForMember(e => e.Species, op => op.MapFrom(e => e.Species.Name))
        CreateMap<Address, P_AddressDto>().ReverseMap();
        CreateMap<City, P_CityDto>().ReverseMap();
        CreateMap<Country, P_CountryDto>().ReverseMap();
        CreateMap<Customer, P_CustomerDto>().ReverseMap();
        CreateMap<Customer, CustumerIdDto>().ReverseMap();
        CreateMap<Customer, CustumerNameDto>().ReverseMap();
        CreateMap<Employee, P_EmployeeDto>().ReverseMap();
        CreateMap<Employee, EmployeeOfficeDto>().ReverseMap();
        CreateMap<Employee, EmployeeBossDto>().ReverseMap();
        CreateMap<Employee, EmployeeNameDto>().ReverseMap();

        CreateMap<Office, P_OfficeDto>().ReverseMap();
        CreateMap<Office, EssencialOfficeDto>().ReverseMap();
        CreateMap<Order, P_OrderDto>().ReverseMap();
        CreateMap<Payment, P_PaymentDto>().ReverseMap();
        CreateMap<Payment, PaymentDataDto>().ReverseMap();
        CreateMap<PaymentMethod, P_PaymentMethodDto>().ReverseMap();
        CreateMap<Product, P_ProductDto>().ReverseMap();
        CreateMap<Product, ProductNameDto>().ReverseMap();
        
        CreateMap<ProductGama, P_ProductGamaDto>().ReverseMap();
        CreateMap<State, P_StateDto>().ReverseMap();
        CreateMap<Order, OrderStatusDto>().ReverseMap();
        CreateMap<Order, EssencialOrderAtrDto>().ReverseMap();





    }
}