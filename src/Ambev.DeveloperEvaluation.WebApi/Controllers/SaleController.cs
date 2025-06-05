using Ambev.DeveloperEvaluation.Application.Dtos;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;

    public SaleController(ISaleService saleService, IMapper mapper)
    {
        _saleService = saleService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleOutputDto request)
    {
        var sale = await _saleService.CreateSaleAsync(
            request.Customer,
            request.Branch,
            request.Date,
            request.Items.Select(i => (
                i.ProductId,
                i.ProductName,
                i.Quantity,
                i.Price
            )).ToList()
        );

        var result = _mapper.Map<SaleDto>(sale);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _saleService.GetAllAsync();
        var result = _mapper.Map<IEnumerable<SaleDto>>(sales);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sale = await _saleService.GetByIdAsync(id);
        if (sale == null)
            return NotFound();

        var result = _mapper.Map<SaleDto>(sale);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _saleService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}