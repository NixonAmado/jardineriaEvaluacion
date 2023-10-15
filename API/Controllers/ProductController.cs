using API.Controllers;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    //[Authorize(Roles = "Adimistrador , gerente")]
public class ProductController : BaseApiController
{    
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
}
    //Listar Todos Los Productos
    // [HttpGet]
    // [Authorize(Roles = "Cajero")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    // {
    //     var products = await _unitOfWork.Products.GetAllAsync();
    //     return _mapper.Map<List<ProductDto>>(products);
    // }

    // [HttpGet]
    // [AllowAnonymous]
    // [MapToApiVersion("1.1")]
    // [Authorize(Roles = "Cajero")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<Pager<ProductDto>>> Get11([FromQuery] Params productParams)
    // {
    //     var products = await _unitOfWork.Products.GetAllAsync(productParams.PageIndex,productParams.PageSize,productParams.Search);
    //     var lstProductDto = _mapper.Map<List<ProductDto>>(products.registros);
    //     return new Pager<ProductDto>(lstProductDto,products.totalRegistros,productParams.PageIndex,productParams.PageSize,productParams.Search);
    // }



//     [HttpGet("{id}")]
//     [Authorize(Roles = "Cajero")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     public async Task<IActionResult> Get(int id)
//     {
//         var products = await _unitOfWork.Products.GetByIdAsync(id);
//         return Ok(products);
//     }

//     [HttpPost]
//     [Authorize(Roles = "Cajero")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     public async Task<ActionResult<Product>> Post(ProductDto productDto)
//     {
//         var product = _mapper.Map<Product>(productDto);
//         _unitOfWork.Products.Add(product);
//         await _unitOfWork.SaveAsync();
//         if (productDto == null)
//         {
//             return BadRequest();
//         }
//         productDto.Id = product.Id;
//         return CreatedAtAction(nameof(Post), new { id = productDto.Id }, productDto);
//     }


//     [HttpPut("{id}")]
//     [AllowAnonymous]
//     [Authorize(Roles = "Cajero")]
//     [ProducesResponseType(StatusCodes.Status204NoContent)]
//     public async Task<ActionResult<ProductDto>> Put(int id, [FromBody] ProductDto productDto)
//     {
//         if (productDto == null)
//         {
//             return NotFound();
//         }
//         var product = _mapper.Map<Product>(productDto);
//         _unitOfWork.Products.Update(product);
//         await _unitOfWork.SaveAsync();

//         return productDto;
//     }

//     [HttpDelete("{id}")]
//     [AllowAnonymous]
//     [Authorize(Roles = "Cajero")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     public async Task<IActionResult> Delete(int id)
//     {
//         var product = await _unitOfWork.Products.GetByIdAsync(id);
//         if (product == null)
//         {
//             return NotFound();
//         }

//         _unitOfWork.Products.Remove(product);
//         await _unitOfWork.SaveAsync();

//         return NoContent();
//     }
// }