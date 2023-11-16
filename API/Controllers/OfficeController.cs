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
public class OfficeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OfficeController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_OfficeDto>>> Get()
    {
        var Offices = await _unitOfWork.Offices.GetAllAsync();
        return _mapper.Map<List<P_OfficeDto>>(Offices);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<P_OfficeDto>>> Get([FromQuery] Params OfficeParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Offices.GetAllAsync(OfficeParams.PageIndex,OfficeParams.PageSize,OfficeParams.Search);
        var listaProv = _mapper.Map<List<P_OfficeDto>>(registros);
        return new Pager<P_OfficeDto>(listaProv,totalRegistros,OfficeParams.PageIndex,OfficeParams.PageSize,OfficeParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Office Office)
    {
         if (Office == null)
        {
            return BadRequest();
        }
        _unitOfWork.Offices.Add(Office);
        await _unitOfWork.SaveAsync();
       
        return "Office Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(string id,[FromBody] Office Office)
    {
        if (Office == null|| id != Office.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Offices.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Office, proveedExiste);
        _unitOfWork.Offices.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Office Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Office = await _unitOfWork.Offices.GetByIdAsync(id);
        if (Office == null)
        {
            return NotFound();
        }
        _unitOfWork.Offices.Remove(Office);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Office {Office.Id} se eliminó con éxito." });
    }
}