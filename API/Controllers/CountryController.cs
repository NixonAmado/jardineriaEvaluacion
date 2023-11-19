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
//[Authorize(Roles = "Empleado, Administrador, Gerente")]
public class CountryController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CountryController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    [HttpGet("GetCustomersQuantityByCountry")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetCustomersQuantityByCountry()
    {
        var Countries = await _unitOfWork.Countries.GetCustomersQuantityByCountry();
        return Ok(Countries);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_CountryDto>>> Get()
    {
        var Countries = await _unitOfWork.Countries.GetAllAsync();
        return _mapper.Map<List<P_CountryDto>>(Countries);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<P_CountryDto>>> Get([FromQuery] Params CountryParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Countries.GetAllAsync(CountryParams.PageIndex,CountryParams.PageSize,CountryParams.Search);
        var listaProv = _mapper.Map<List<P_CountryDto>>(registros);
        return new Pager<P_CountryDto>(listaProv,totalRegistros,CountryParams.PageIndex,CountryParams.PageSize,CountryParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Country Country)
    {
         if (Country == null)
        {
            return BadRequest();
        }
        _unitOfWork.Countries.Add(Country);
        await _unitOfWork.SaveAsync();
       
        return "Country Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Country Country)
    {
        if (Country == null|| id != Country.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Countries.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Country, proveedExiste);
        _unitOfWork.Countries.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Country Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Country = await _unitOfWork.Countries.GetByIdAsync(id);
        if (Country == null)
        {
            return NotFound();
        }
        _unitOfWork.Countries.Remove(Country);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Country {Country.Id} se eliminó con éxito." });
    }
}