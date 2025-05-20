using Domain.DTOs.Auth;
using Domain.DTOs.User;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<List<GetUserDto>>> GetAll();
    Task<Response<GetUserDto>> Get(string id);
    Task<Response<GetUserDto>> Update(string id, UpdateUserDto request);
    Task<Response<string>> Delete(string id);

}