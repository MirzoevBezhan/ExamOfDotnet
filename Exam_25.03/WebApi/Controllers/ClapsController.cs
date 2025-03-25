using Domain.Entitites;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClapsController
{
    private readonly ClapService clapService = new ();

    [HttpGet]
    public async Task<Response<List<Clap>>> GetAll()
    {
        return await clapService.GetAll();
    }
    [HttpGet("id:int")]
    public async Task<Response<Clap>> GetClap(int id)
    {
        return await clapService.GetClap(id);
    }
    [HttpPost]
    public async Task<Response<string>> AddClap(Clap clap)
    {
        return await clapService.AddClap(clap);
    }

    [HttpDelete("id:int")]
    public async Task<Response<string>> DeleteClap(int id)
    {
        return await clapService.DeleteClap(id);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateClap(Clap clap)
    {
        return await clapService.UpdateClap(clap);
    }


}
