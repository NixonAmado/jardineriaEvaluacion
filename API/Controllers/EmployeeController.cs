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
public class EmployeeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmployeeController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_EmployeeDto>>> Get()
    {
        var Employees = await _unitOfWork.Employees.GetAllAsync();
        return _mapper.Map<List<P_EmployeeDto>>(Employees);
    }

    [HttpGet("GetNameAndBossChief")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetNameAndBossChief()
    {
        var Employees = await _unitOfWork.Employees.GetNameAndBossChief();
        return _mapper.Map<List<object>>(Employees);
    }
    
    [HttpGet("GetNotAssoEmployeeAndOffice")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<P_EmployeeDto>>> GetNotAssociatedEmployeeAndOffice()
    {
        var Employees = await _unitOfWork.Employees.GetNotAssociatedEmployeeAndOffice();
        return _mapper.Map<List<P_EmployeeDto>>(Employees);
    }

    [HttpGet("GetNotAssoCustomerBossName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmployeeBossDto>>> GetNotAssociatedcustomerBossName()
    {
        var Employees = await _unitOfWork.Employees.GetNotAssociatedcustomerBossName();
        return _mapper.Map<List<EmployeeBossDto>>(Employees);
    }

    [HttpGet("GetEmployeesQuantity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetEmployeesQuantity()
    {
        var Employees = await _unitOfWork.Employees.GetEmployeesQuantity();
        return Ok(Employees);
    }

    [HttpGet("GetEmployeesCustomerQuantity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetEmployeesCustomerQuantity()
    {
        var Employees = await _unitOfWork.Employees.GetEmployeesCustomerQuantity();
        return Ok(Employees);
    }
    [HttpGet("GetEmployeesWithoutOrder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetEmployeesWithoutOrder()
    {
        var Employees = await _unitOfWork.Employees.GetEmployeesWithoutOrder();
        return Ok(Employees);
    }
    
    [HttpGet("GetDataByJobTitle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetDataByJobTitle()
    {
        var Employees = await _unitOfWork.Employees.GetDataByJobTitle();
        return Ok(Employees);
    }
    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<P_EmployeeDto>>> Get([FromQuery] Params EmployeeParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Employees.GetAllAsync(EmployeeParams.PageIndex,EmployeeParams.PageSize,EmployeeParams.Search);
        var listaProv = _mapper.Map<List<P_EmployeeDto>>(registros);
        return new Pager<P_EmployeeDto>(listaProv,totalRegistros,EmployeeParams.PageIndex,EmployeeParams.PageSize,EmployeeParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(Employee Employee)
    {
         if (Employee == null)
        {
            return BadRequest();
        }
        _unitOfWork.Employees.Add(Employee);
        await _unitOfWork.SaveAsync();
       
        return "Employee Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] Employee Employee)
    {
        if (Employee == null|| id != Employee.Id)
        {
            return BadRequest();
        }
        var proveedExiste = await _unitOfWork.Employees.GetByIdAsync(id);

        if (proveedExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(Employee, proveedExiste);
        _unitOfWork.Employees.Update(proveedExiste);
        await _unitOfWork.SaveAsync();

        return "Employee Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var Employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (Employee == null)
        {
            return NotFound();
        }
        _unitOfWork.Employees.Remove(Employee);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El Employee {Employee.Id} se eliminó con éxito." });
    }
}