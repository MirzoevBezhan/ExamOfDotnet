using System.Diagnostics;
using AutoMapper;
using Domain.Dtos.Customer;
using Domain.Entitites;
using Domain.Filters;
using Domain.Responces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;

namespace MyProj.Controllers;

[Route("Customer")]
public class CustomerController(ICustomerService service, IMapper mapper) : Controller
{
    [HttpGet("Index")]
    public async Task<IActionResult> Index([FromQuery] CustomerFilter filter)
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
    public async Task<IActionResult> Create(CreateCustomerDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var customer = mapper.Map<Customer>(dto);
        var response = new Response<Customer>(customer);

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
        if (result.Code != System.Net.HttpStatusCode.OK)
        {
            return NotFound();
        }

        var dto = mapper.Map<Customer>(result);

        return View(dto);
    }

    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, UpdateCustomerDto dto)
    {
        var mapped = mapper.Map<Customer>(dto);
        if (!ModelState.IsValid) return View(mapped);

        var result = await service.Update(id, dto);
        if (result.Code != System.Net.HttpStatusCode.OK)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            var mapped2 = mapper.Map<Customer>(result);
            return View(mapped2);
        }

        return RedirectToAction("Index");
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.GetById(id);
        if (result.Code != System.Net.HttpStatusCode.OK)
        {
            return NotFound();
        }
        var mapped = mapper.Map<Customer>(result);
        return View(mapped);
    }

    [HttpPost("Delete/{id}")]
    public async Task<IActionResult> DeleTe(int id)
    {
        var result = await service.Delete(id);
        if (result.Code != System.Net.HttpStatusCode.OK)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}