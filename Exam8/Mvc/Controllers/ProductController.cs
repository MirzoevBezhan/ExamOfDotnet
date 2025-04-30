using System.Diagnostics;
using AutoMapper;
using Domain.Dtos.Customer;
using Domain.Dtos.Product;
using Domain.Entitites;
using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;

namespace MyProj.Controllers;

[Route("Product")]
public class ProductController(IProductService service,IMapper mapper) : Controller
{
    [HttpGet("Index")]
    public async Task<IActionResult> Index([FromQuery] ProductFilter filter)
    {
        var result = await service.GetAll(filter);
        return View(result.Data);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var result = await service.Create(dto);
        if (result.Code != System.Net.HttpStatusCode.OK)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(dto);
        }

        return RedirectToAction("Index");
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await service.GetById(id);
        if (result.Code != System.Net.HttpStatusCode.OK) return NotFound();
       var mapped = mapper.Map<UpdateProductDto>(result);
        return View(mapped);
    }

    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, UpdateProductDto dto)
    {
        var result = await service.Update(id, dto);
        if (result.Code != System.Net.HttpStatusCode.OK)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View(dto);
        }

        return RedirectToAction("Index");
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.Delete(id);
        if (result.Code != System.Net.HttpStatusCode.OK)
        {
            return BadRequest(result.Message);
        }

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
