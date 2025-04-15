using Domain.DTOs.TaskAssigmentDto;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ITaskAssigmentService
{
        Task<Response<GetAssignedTaskDto>> AssignTask(AssignTaskDto assignTask);
    Task<Response<GetAssignedTaskDto>> UpdateAssignment(int id , UpdateAssignTaskDto assignTask);
    Task<Response<string>> DeleteAssignment(int id);
    Task<Response<GetAssignedTaskDto>> GetAssignment(int id);
    Task<Response<List<GetAssignedTaskDto>>> GetAssignmentsByUser(int id);
    Task<Response<List<GetAssignedTaskDto>>> GetAssignmentsByTask(int id);
}
