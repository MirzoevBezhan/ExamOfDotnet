using Domain.DTOs.Project;
using Domain.Entites;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IProjectService
{
    Task<Response<GetProjectDto>> AddProject(CreateProjectDto project);
    Task<Response<GetProjectDto>> UpdateProject(int id,UpdateProjectDto project);
    Task<Response<string>> DeleteProject(int id);
    Task<Response<GetProjectDto>> GetProject(int id);
    Task<Response<GetProjectWithMostTasksDto>> GetProjectWithMostTasks();
}
