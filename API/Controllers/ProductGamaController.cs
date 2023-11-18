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
public class ProductGamaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductGamaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    [HttpGet("GetByProductGama")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetByProductGama()
    {
        var ProductGamas = await _unitOfWork.ProductGamas.GetByProductGama();
        return _mapper.Map<List<object>>(ProductGamas);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_ProductGamaDto>>> Get()
    {
        var ProductGamas = await _unitOfWork.ProductGamas.GetAllAsync();
        return _mapper.Map<List<P_ProductGamaDto>>(ProductGamas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<P_ProductGamaDto>>> Get([FromQuery] Params ProductGamaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.ProductGamas.GetAllAsync(ProductGamaParams.PageIndex,ProductGamaParams.PageSize,ProductGamaParams.Search);
        var listaProv = _mapper.Map<List<P_ProductGamaDto>>(registros);
        return new Pager<P_ProductGamaDto>(listaProv,totalRegistros,ProductGamaParams.PageIndex,ProductGamaParams.PageSize,ProductGamaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(ProductGama ProductGama)
    {
         if (ProductGama == null)
        {
            return BadRequest();
        }
        _unitOfWork.ProductGamas.Add(ProductGama);
        await _unitOfWork.SaveAsync();
       
        return "ProductGama Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(string id,[FromBody] ProductGama ProductGama)
    {
        if (ProductGama == null|| id != ProductGama.Gama)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.ProductGamas.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(ProductGama, proveedExiste);
        _unitOfWork.ProductGamas.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "ProductGama Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string id)
    {
        var ProductGama = await _unitOfWork.ProductGamas.GetByIdAsync(id);
        if (ProductGama == null)
        {
            return NotFound();
        }
        _unitOfWork.ProductGamas.Remove(ProductGama);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El ProductGama {ProductGama.Gama} se eliminó con éxito." });
    }
}