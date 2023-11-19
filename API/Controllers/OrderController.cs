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
public class OrderController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrderController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_OrderDto>>> Get()
    {
        var Orders = await _unitOfWork.Orders.GetAllAsync();
        return _mapper.Map<List<P_OrderDto>>(Orders);
    }
    [HttpGet("GetAllStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetAllStatus()
    {
        var Orders = await _unitOfWork.Orders.GetAllStatus();
        return _mapper.Map<List<object>>(Orders);
    }
    [HttpGet("GetAllDeliveredEarlier")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EssencialOrderAtrDto>>> GetAllDeliveredEarlier()
    {
        var Orders = await _unitOfWork.Orders.GetAllDeliveredEarlier();
        return _mapper.Map<List<EssencialOrderAtrDto>>(Orders);
    }
    [HttpGet("GetAllNotDeliveredOnTime")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EssencialOrderAtrDto>>> GetAllNotDeliveredOnTime()
    {
        var Orders = await _unitOfWork.Orders.GetAllNotDeliveredOnTime();
        return _mapper.Map<List<EssencialOrderAtrDto>>(Orders);
    }    
    [HttpGet("GetOrderByStatusYear/{status}/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_OrderDto>>> GetOrderByStatusYear(string status, int year)
    {
        var Orders = await _unitOfWork.Orders.GetOrderByStatusYear(status, year);
        return _mapper.Map<List<P_OrderDto>>(Orders);
    }
    [HttpGet("GetAllByMonth/{status}/{month}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_OrderDto>>> GetAllByMonth(string status, string month)
    {
        var Orders = await _unitOfWork.Orders.GetAllByMonth(status, month);
        return _mapper.Map<List<P_OrderDto>>(Orders);
    }    

    [HttpGet("GetOrderPaymentAverangeInYear/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetOrderPaymentAverangeInYear( int year)
    {
        var Orders = await _unitOfWork.Orders.GetOrderPaymentAverangeInYear(year);
        return Ok(Orders);
    }
    
    [HttpGet("GetOrdersQuantityByStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetOrdersQuantityByStatus()
    {
        var Orders = await _unitOfWork.Orders.GetOrdersQuantityByStatus();
        return Ok(Orders);
    }
    
    [HttpGet("GetByDifferentProdQuantity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetByDifferentProdQuantity()
    {
        var Orders = await _unitOfWork.Orders.GetByDifferentProdQuantity();
        return Ok(Orders);
    }

    [HttpGet("GetTotalSumProdInOrder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> GetTotalSumProdInOrder()
    {
        var Orders = await _unitOfWork.Orders.GetTotalSumProdInOrder();
        return Ok(Orders);
    }

    [HttpGet("GetMostSold")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetMostSold()
    {
        var Orders = await _unitOfWork.Orders.GetMostSold();
        return Ok(Orders);
    }

    [HttpGet("GetMostSoldGroupedByCod")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetMostSoldGroupedByCod()
    {
        var Orders = await _unitOfWork.Orders.GetMostSoldGroupedByCod();
        return Ok(Orders);
    }


    [HttpGet("GetMostSoldGroupedByCodFiltered/{letters}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetMostSoldGroupedByCodFiltered(string letters)
    {
        var Orders = await _unitOfWork.Orders.GetMostSoldGroupedByCodFiltered(letters);
        return Ok(Orders);
    }   
    
    [HttpGet("GetTotalSaleByQuantityRange/{range}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetTotalSaleByQuantityRange(int range)
    {
        var Orders = await _unitOfWork.Orders.GetTotalSaleByQuantityRange(range);
        return Ok(Orders);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<P_OrderDto>>> Get([FromQuery] Params OrderParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Orders.GetAllAsync(OrderParams.PageIndex,OrderParams.PageSize,OrderParams.Search);
        var listaProv = _mapper.Map<List<P_OrderDto>>(registros);
        return new Pager<P_OrderDto>(listaProv,totalRegistros,OrderParams.PageIndex,OrderParams.PageSize,OrderParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Order Order)
    {
         if (Order == null)
        {
            return BadRequest();
        }
        _unitOfWork.Orders.Add(Order);
        await _unitOfWork.SaveAsync();
       
        return "Order Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Order Order)
    {
        if (Order == null|| id != Order.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Orders.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Order, proveedExiste);
        _unitOfWork.Orders.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Order Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Order = await _unitOfWork.Orders.GetByIdAsync(id);
        if (Order == null)
        {
            return NotFound();
        }
        _unitOfWork.Orders.Remove(Order);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Order {Order.Id} se eliminó con éxito." });
    }
}