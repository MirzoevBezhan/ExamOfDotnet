using Domain.Entites;
using Domain.Responces;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController(IQuestionService questionService)
{
    [HttpGet]
    public async Task<Responce<List<Question>>> GetAll()
    {
        return await questionService.GetAll();
    }
    [HttpGet("id:int")]
    public async Task<Responce<Question>> Get(int id)
    {
        return await questionService.GetQuestion(id);
    }
    [HttpPut]
    public async Task<Responce<string>> Update(Question question)
    {
        return await questionService.UpdateQuestion(question);
    }

    [HttpPost]
    public async Task<Responce<string>> Add(Question question)
    {
        return await questionService.AddQuestion(question);
    }

    [HttpDelete]
    public async Task<Responce<string>> Delete(int id)
    {
        return await questionService.DeleteQuestion(id);
    }

    [HttpPost]
    public async Task<Responce<string>> Import()
    {
        return await questionService.Import();
    }
    [HttpGet]
    public async Task<Responce<string>> Export()
    {
        return await questionService.Export();
    }
}
