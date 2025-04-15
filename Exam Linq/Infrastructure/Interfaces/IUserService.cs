using Domain.DTOs.User;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<GetUserDto>> AddUser(CreateUserDto User);
    Task<Response<GetUserDto>> UpdateUser(int id,UpdateUserDto User);
    Task<Response<string>> DeleteUser(int id);
    Task<Response<GetUserDto>> GetUser(int id);
    Task<Response<GetUserWithMostTasksDto>> GetUserWithMostTasks();
  
}
