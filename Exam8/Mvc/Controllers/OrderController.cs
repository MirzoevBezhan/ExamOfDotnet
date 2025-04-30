using System.Diagnostics;
using AutoMapper;
using Domain.Dtos.Order;
using Domain.Entitites;
using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;

namespace MyProj.Controllers;

[Route("Order")]
public class OrderController(IOrderService service, IMapper mapper) : Controller
{
    [HttpGet("Index")]
    public async Task<IActionResult> Index([FromQuery] OrderFilter filter)
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
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

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

        var dto = mapper.Map<Order>(result);

        return View(dto);
    }

    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, UpdateOrderDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

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
        var result = await service.GetById(id);
        if (result.Code != System.Net.HttpStatusCode.OK)
        {
            return NotFound();
        }
        var mapped = mapper.Map<Order>(result);
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
