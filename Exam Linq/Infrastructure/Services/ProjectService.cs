using System.Net;
using Domain.DTOs.Project;
using Domain.Entites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ProjectService(DataContext context) : IProjectService
{
    public async Task<Response<GetProjectDto>> AddProject(CreateProjectDto Project)
    {
        var project = new Project()
        {
            Description = Project.Description,
            EndDate = Project.EndDate,
            Name = Project.Name,
            StartDate = Project.StartDate,
        };
        await context.Projects.AddAsync(project);
        var res = await context.SaveChangesAsync();

        var result = new GetProjectDto()
        {
        };

        return res == 0
        ? new Response<GetProjectDto>(HttpStatusCode.BadRequest, "Don't Created Project")
        : new Response<GetProjectDto>(result);

    }

    public async Task<Response<string>> DeleteProject(int id)
    {
        var Project = await context.Projects.FindAsync(id);

        if (Project == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found");
        }
        context.Projects.Remove(Project);
        var res = await context.SaveChangesAsync();

        return res == 0
        ? new Response<string>(HttpStatusCode.BadRequest, "Don't Deleted")
        : new Response<string>("Deleted Succsefuly");

    }


    public async Task<Response<GetProjectDto>> GetProject(int id)
    {
        var Project = await context.Projects.FindAsync(id);

        if (Project == null)
        {
            return new Response<GetProjectDto>(HttpStatusCode.BadRequest, " Not found");
        }

        var res = new GetProjectDto()
        {
            id = Project.id,
            Name = Project.Name,
            Description = Project.Description,
            EndDate = Project.EndDate,
            StartDate = Project.StartDate,
        };

        return new Response<GetProjectDto>(res);
    }

    public async Task<Response<GetProjectWithMostTasksDto>> GetProjectWithMostTasks()
    {
        var Project = context.Projects.OrderByDescending(t => t.Tasks.Count()).AsParallel().FirstOrDefault();

        var res = new GetProjectWithMostTasksDto()
        {
            id = Project.id,
            Name = Project.Name,
            Description = Project.Description,
            EndDate = Project.EndDate,
            StartDate = Project.StartDate,
            TasksCount = Project.Tasks.Count(),
        };

        return new Response<GetProjectWithMostTasksDto>(res);
    }

    public async Task<Response<GetProjectDto>> UpdateProject(int id, UpdateProjectDto Project)
    {
        var project = await context.Projects.FindAsync(id);

        if (project==null)
        {
            return new Response<GetProjectDto>(HttpStatusCode.NotFound, "Not found");
        }
        
        project.Name = Project.Name;
        project.Description = project.Description;
        project.EndDate = project.EndDate;
        project.StartDate = project.StartDate;

        var res = await context.SaveChangesAsync();

        var result = new GetProjectDto()
        {
            id = project.id,
            Name = project.Name,
            Description = project.Description,
            EndDate = project.EndDate,
            StartDate = project.StartDate,
        };

        return res == 0
        ? new Response<GetProjectDto>(HttpStatusCode.BadRequest, "Doesn't Updated")
        : new Response<GetProjectDto>(result);
    }

}
