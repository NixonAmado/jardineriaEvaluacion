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
public class CustomerController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CustomerController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_CustomerDto>>> Get()
    {
        var Customers = await _unitOfWork.Customers.GetAllAsync();
        return _mapper.Map<List<P_CustomerDto>>(Customers);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<P_CustomerDto>>> Get([FromQuery] Params CustomerParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Customers.GetAllAsync(CustomerParams.PageIndex,CustomerParams.PageSize,CustomerParams.Search);
        var listaProv = _mapper.Map<List<P_CustomerDto>>(registros);
        return new Pager<P_CustomerDto>(listaProv,totalRegistros,CustomerParams.PageIndex,CustomerParams.PageSize,CustomerParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Customer Customer)
    {
         if (Customer == null)
        {
            return BadRequest();
        }
        _unitOfWork.Customers.Add(Customer);
        await _unitOfWork.SaveAsync();
       
        return "Customer Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Customer Customer)
    {
        if (Customer == null|| id != Customer.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Customers.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Customer, proveedExiste);
        _unitOfWork.Customers.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Customer Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Customer = await _unitOfWork.Customers.GetByIdAsync(id);
        if (Customer == null)
        {
            return NotFound();
        }
        _unitOfWork.Customers.Remove(Customer);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Customer {Customer.Id} se eliminó con éxito." });
    }
}