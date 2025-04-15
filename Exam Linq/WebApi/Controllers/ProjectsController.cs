using Domain.DTOs.Project;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController (IProjectService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetProjectDto>> GetProject(int id)
    {
        return await service.GetProject(id);
    }

    [HttpGet]
    public async Task<Response<GetProjectWithMostTasksDto>> GetProjectWithMostTasks()
    {
        return await service.GetProjectWithMostTasks();
    }

    [HttpPut("int:id")]
    public async Task<Response<GetProjectDto>> UpdateProject(int id, UpdateProjectDto project)
    {
        return await service.UpdateProject(id, project);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> DeleteProject(int id)
    {
        return await service.DeleteProject(id);
    }

    [HttpPost]
    public async Task<Response<GetProjectDto>> AddProject(CreateProjectDto project)
    {
        return await service.AddProject(project);
    }
}