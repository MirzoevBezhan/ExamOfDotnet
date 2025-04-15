using Domain.DTOs;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ITaskService
{
    Task<Response<GetTaskDto>> CreateTask(CreateTaskDto task);
    Task<Response<GetTaskDto>> UpdateTask(int id,UpdateTaskDto task);
    Task<Response<string>> DeleteTask(int id);
    Task<Response<GetTaskDto>> GetTask(int id);
    Task<Response<List<GetTaskDto>>> GetTasksByProject(int id);
    Task<Response<List<GetTaskDto>>> GetTasksByUser(int id);
    Task<Response<List<GetTaskDto>>> GetTasksByDueSoon(DateTime duedate);
}
