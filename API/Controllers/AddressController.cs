using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize(Roles = "Empleado, Administrador, Gerente")]
public class AddressController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AddressController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_AddressDto>>> Get()
    {
        var Addresses = await _unitOfWork.Addresses.GetAllAsync();
        return _mapper.Map<List<P_AddressDto>>(Addresses);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<P_AddressDto>>> Get([FromQuery] Params AddressParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Addresses.GetAllAsync(AddressParams.PageIndex,AddressParams.PageSize,AddressParams.Search);
        var listaProv = _mapper.Map<List<P_AddressDto>>(registros);
        return new Pager<P_AddressDto>(listaProv,totalRegistros,AddressParams.PageIndex,AddressParams.PageSize,AddressParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Address Address)
    {
         if (Address == null)
        {
            return BadRequest();
        }
        _unitOfWork.Addresses.Add(Address);
        await _unitOfWork.SaveAsync();
       
        return "Address Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Address Address)
    {
        if (Address == null|| id != Address.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Addresses.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Address, proveedExiste);
        _unitOfWork.Addresses.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Address Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Address = await _unitOfWork.Addresses.GetByIdAsync(id);
        if (Address == null)
        {
            return NotFound();
        }
        _unitOfWork.Addresses.Remove(Address);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Address {Address.Id} se eliminó con éxito." });
    }
}