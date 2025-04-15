using Domain.DTOs.TaskAssigmentDto;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TaskAssigmentsController(ITaskAssigmentService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetAssignedTaskDto>> GetAssingment(int id)
    {
        return await service.GetAssignment(id);
    }

    [HttpPost]
    public async Task<Response<GetAssignedTaskDto>> AssignTask(AssignTaskDto assignTask)
    {
        return await service.AssignTask(assignTask);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetAssignedTaskDto>> UpdateAssignment(int id, UpdateAssignTaskDto assignTask)
    {
        return await service.UpdateAssignment(id, assignTask);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> DeleteAssignment(int id)
    {
        return await service.DeleteAssignment(id);
    }

    [HttpGet("int:UserId")]
    public async Task<Response<List<GetAssignedTaskDto>>> GetAssignmentsByUser(int userId)
    {
        return await service.GetAssignmentsByUser(userId);
    }

    [HttpGet("int:TaskId")]
    public async Task<Response<List<GetAssignedTaskDto>>> GetAssignmentsByTask(int taskId)
    {
        return await service.GetAssignmentsByTask(taskId);
    }
    
}