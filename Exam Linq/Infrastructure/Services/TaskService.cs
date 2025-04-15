using Domain.Entites;
using Domain.DTOs;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using System.Net;

namespace Infrastructure.Services;

public class TaskService(DataContext context) : ITaskService
{
    public async Task<Response<GetTaskDto>> CreateTask(CreateTaskDto task)
    {
        var Task = new Domain.Entites.Task()
        {
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            ProjectId = task.ProjectId,
            UserId = task.UserId,
        };
        await context.Tasks.AddAsync(Task);
        var res = await context.SaveChangesAsync();

        var result = new GetTaskDto()
        {
            id = Task.id,
            Title = Task.Title,
            Description = Task.Description,
            DueDate = Task.DueDate,
            ProjectId = Task.ProjectId,
            UserId = Task.UserId,
        };

        return res == 0
        ? new Response<GetTaskDto>(HttpStatusCode.BadRequest, "Don't Created Task")
        : new Response<GetTaskDto>(result);

    }

    public async Task<Response<string>> DeleteTask(int id)
    {
        var Task = await context.Tasks.FindAsync(id);

        if (Task == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest,"Not Found");
        }
        
        context.Tasks.Remove(Task);
        var res = await context.SaveChangesAsync();

        return res == 0
        ? new Response<string>(HttpStatusCode.BadRequest, "Don't Deleted")
        : new Response<string>("Deleted Succsefuly");

    }


    public async Task<Response<GetTaskDto>> GetTask(int id)
    {
        var Task = await context.Tasks.FindAsync(id);

        if (Task == null)
        {
            return new Response<GetTaskDto>(HttpStatusCode.BadRequest, " Not found");
        }

        var res = new GetTaskDto()
        {
            id = Task.id,
            Description = Task.Description,
            DueDate = Task.DueDate,
            ProjectId = Task.ProjectId,
            Title = Task.Title,
            UserId = Task.UserId,
        };

        return new Response<GetTaskDto>(res);

    }

    public async Task<Response<List<GetTaskDto>>> GetTasksByDueSoon(DateTime duedate)
    {
        var Tasks = context.Tasks.Where(t => t.DueDate == duedate).ToList().AsParallel();

        if (Tasks == null)
        {
            return new Response<List<GetTaskDto>>(HttpStatusCode.BadRequest, "Didn't find with this Date");
        }
        
        var res = Tasks.Select(t => new GetTaskDto()
        {
            id = t.id,
            Description = t.Description,
            DueDate = t.DueDate,
            ProjectId = t.ProjectId,
            UserId = t.UserId,
            Title = t.Title,
        }).AsParallel().ToList();

        return new Response<List<GetTaskDto>>(res);
    }

    public async Task<Response<List<GetTaskDto>>> GetTasksByProject(int id)
    {
        var Tasks = context.Tasks.Where(t => t.ProjectId == id).AsParallel().ToList();

        var res = Tasks.Select(t => new GetTaskDto()
        {
            id = t.id,
            Description = t.Description,
            DueDate = t.DueDate,
            ProjectId = t.ProjectId,
            Title = t.Title,
            UserId = t.UserId,
        }).AsParallel().ToList();

        return new Response<List<GetTaskDto>>(res);
    }

    public async Task<Response<List<GetTaskDto>>> GetTasksByUser(int id)
    {
        var Tasks = context.Tasks.Where(t => t.UserId == id).AsParallel().ToList();

        var res = Tasks.Select(t => new GetTaskDto()
        {
            id = t.id,
            Description = t.Description,
            DueDate = t.DueDate,
            ProjectId = t.ProjectId,
            Title = t.Title,
            UserId = t.UserId,
        }).AsParallel().ToList();

        return new Response<List<GetTaskDto>>(res);
    }

    public async Task<Response<GetTaskDto>> UpdateTask(int id, UpdateTaskDto task)
    {
        var Task = await context.Tasks.FindAsync(id);

        if (Task == null) 
        {
            return new Response<GetTaskDto>(HttpStatusCode.BadRequest,"Not Found");
        }
        
        Task.Description = task.Description;
        Task.DueDate = task.DueDate;
        Task.ProjectId = task.ProjectId;
        Task.UserId = task.UserId;
        Task.Title = task.Title;

        var res = await context.SaveChangesAsync();

        var result = new GetTaskDto()
        {
            id = Task.id,
            Description = Task.Description,
            DueDate = Task.DueDate,
            ProjectId = Task.ProjectId,
            UserId = Task.UserId,
            Title = Task.Title,
        };

        return res == 0
        ? new Response<GetTaskDto>(HttpStatusCode.BadRequest, "Doesn't Updated")
        : new Response<GetTaskDto>(result);
    }

}
