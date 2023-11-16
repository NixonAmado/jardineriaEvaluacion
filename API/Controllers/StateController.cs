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
public class StateController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public StateController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_StateDto>>> Get()
    {
        var States = await _unitOfWork.States.GetAllAsync();
        return _mapper.Map<List<P_StateDto>>(States);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<P_StateDto>>> Get([FromQuery] Params StateParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.States.GetAllAsync(StateParams.PageIndex,StateParams.PageSize,StateParams.Search);
        var listaProv = _mapper.Map<List<P_StateDto>>(registros);
        return new Pager<P_StateDto>(listaProv,totalRegistros,StateParams.PageIndex,StateParams.PageSize,StateParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(State State)
    {
         if (State == null)
        {
            return BadRequest();
        }
        _unitOfWork.States.Add(State);
        await _unitOfWork.SaveAsync();
       
        return "State Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] State State)
    {
        if (State == null|| id != State.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.States.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(State, proveedExiste);
        _unitOfWork.States.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "State Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var State = await _unitOfWork.States.GetByIdAsync(id);
        if (State == null)
        {
            return NotFound();
        }
        _unitOfWork.States.Remove(State);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El State {State.Id} se eliminó con éxito." });
    }
}