using System.Net;
using Domain.DTOs.TaskAssigmentDto;
using Domain.Entites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class TaskAssignmentService(DataContext context) : ITaskAssigmentService
{
    public async Task<Response<GetAssignedTaskDto>> AssignTask(AssignTaskDto assignTask)
    {
        var assign = new TaskAssignment()
        {
            TaskId = assignTask.TaskId,
            UserId = assignTask.UserId,
            AssignedDate = assignTask.AssignedDate,
        };

        await context.TaskAssignments.AddAsync(assign);
        var res = await context.SaveChangesAsync();

        var result = new GetAssignedTaskDto()
        {
            AssignedDate = assign.AssignedDate,
            TaskId = assign.TaskId,
            id = assign.id,
            UserId = assign.UserId,
        };

        return res == 0
        ? new Response<GetAssignedTaskDto>(HttpStatusCode.BadRequest, "Don't Assign")
        : new Response<GetAssignedTaskDto>(result);

    }

    public async Task<Response<string>> DeleteAssignment(int id)
    {
        var assign = await context.TaskAssignments.FindAsync(id);
        context.TaskAssignments.Remove(assign);

        if (assign==null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Not found");
        }

        var res = await context.SaveChangesAsync();

        return res == 0
        ? new Response<string>(HttpStatusCode.BadRequest, "Did't Deleted")
        : new Response<string>("Deleted Successfuly");
    }

    public async Task<Response<List<GetAssignedTaskDto>>> GetAssignmentsByUser(int id)
    {
        var assign = context.TaskAssignments.Where(t => t.UserId == id).AsParallel().ToList();

        var res = assign.Select(t => new GetAssignedTaskDto()
        {
            AssignedDate = t.AssignedDate,
            id = t.id,
            TaskId = t.TaskId,
            UserId = t.UserId,
        }).AsParallel().ToList();

        return new Response<List<GetAssignedTaskDto>>(res);
    }

    public async Task<Response<GetAssignedTaskDto>> GetAssignment(int id)
    {
        var assign = await context.TaskAssignments.FindAsync(id);

        if (assign == null)
        {
            return new Response<GetAssignedTaskDto>(HttpStatusCode.BadRequest, "Not found");
        }

        var res = new GetAssignedTaskDto()
        {
            AssignedDate = assign.AssignedDate,
            id = assign.id,
            TaskId = assign.TaskId,
            UserId = assign.UserId,
        };

        return new Response<GetAssignedTaskDto>(res);

    }

    public async Task<Response<List<GetAssignedTaskDto>>> GetAssignmentsByTask(int id)
    {
        var assigns = context.TaskAssignments.Where(t => t.TaskId == id).AsParallel().ToList();
        var result = assigns.Select(t => new GetAssignedTaskDto()
        {
            AssignedDate = t.AssignedDate,
            id = t.id,
            TaskId = t.TaskId,
            UserId = t.UserId,
        }).AsParallel().ToList();

        return new Response<List<GetAssignedTaskDto>>(result);
    }

    public async Task<Response<GetAssignedTaskDto>> UpdateAssignment(int id, UpdateAssignTaskDto assignTask)
    {
        var assign = await context.TaskAssignments.FindAsync(id);

        if (assign == null)
        {
            return new Response<GetAssignedTaskDto>(HttpStatusCode.BadRequest, "Not found");
        }
        
        assign.AssignedDate = assignTask.AssignedDate;
        assign.TaskId = assignTask.TaskId;
        assign.UserId = assignTask.UserId;

        var res = await context.SaveChangesAsync();

        var result = new GetAssignedTaskDto()
        {
            AssignedDate = assign.AssignedDate,
            id = assign.id,
            TaskId = assign.TaskId,
            UserId = assign.UserId,
        };

        return res == 0
        ? new Response<GetAssignedTaskDto>(HttpStatusCode.BadRequest, "Doesn't Updated")
        : new Response<GetAssignedTaskDto>(result);

    }

}
