using Domain.DTOs;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ITaskService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetTaskDto>> GetTask(int id)
    {
        return await service.GetTask(id);
    }

    [HttpPost]
    public async Task<Response<GetTaskDto>> CreateTask(CreateTaskDto task)
    {
        return await service.CreateTask(task);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetTaskDto>> UpdateTask(int id, UpdateTaskDto task)
    {
        return await service.UpdateTask(id, task);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> DeleteTask(int id)
    {
        return await service.DeleteTask(id);
    }

    [HttpGet("int:id")]
    public async Task<Response<List<GetTaskDto>>> GetTasksByProject(int id)
    {
        return await service.GetTasksByProject(id);
    }

    [HttpGet("int:id")]
    public async Task<Response<List<GetTaskDto>>> GetTasksByUser(int id)
    {
        return await service.GetTasksByUser(id);
    }

    [HttpGet("datetime:DueDate")]
    public async Task<Response<List<GetTaskDto>>> GetTasksByDueDate(DateTime dueDate)
    {
        return await service.GetTasksByDueSoon(dueDate);
    }
}