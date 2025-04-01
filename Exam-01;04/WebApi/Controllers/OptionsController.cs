using Domain.Entites;
using Domain.Responces;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OptionsController(IOptionService optionService)
{
    [HttpGet]
    public async Task<Responce<List<Options>>> GetAll()
    {
        return await optionService.GetAll();
    }
    [HttpGet("id:int")]
    public async Task<Responce<Options>> Get(int id)
    {
        return await optionService.GetOptions(id);
    }
    [HttpPut]
    public async Task<Responce<string>> Update(Options options)
    {
        return await optionService.UpdateOption(options);
    }

    [HttpPost]
    public async Task<Responce<string>> Add(Options options)
    {
        return await optionService.AddOption(options);
    }

    [HttpDelete]
    public async Task<Responce<string>> Delete(int id)
    {
        return await optionService.DeleteOption(id);
    }
    
    [HttpPost]
    public async Task<Responce<string>> Import()
    {
        return await optionService.Import();
    }
    [HttpGet]
    public async Task<Responce<string>> Export()
    {
        return await optionService.Export();
    }
}
