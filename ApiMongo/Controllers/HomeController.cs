using ApiMongo.Models;
using ApiMongo.Services;
using DomainServices.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ApiMongo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
    private readonly ICustomerService _customerService;

    public HomeController(ICustomerService customerService)
    {
        _customerService = customerService ?? throw new System.ArgumentNullException(nameof(customerService));
    }

    [HttpPost]
    public IActionResult Post(Customer customer)
    {
        try
        {
            var createdCustomer = _customerService.CreateCustomer(customer);
            return Created("~http://localhost:5160/api/Customers", createdCustomer);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var customersFound = _customerService.GetAll();
        return Ok(customersFound);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        try
        {
            var customerFoundId = _customerService.GetById(id);
            return Ok(customerFoundId);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(long id, Customer customer)
    {
        try
        {
            _customerService.Update(id, customer);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        try
        {
            _customerService.Delete(id);
            return NoContent();
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}