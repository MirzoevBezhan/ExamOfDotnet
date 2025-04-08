using Domain.DTOs.User;
using Domain.Entities;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    public Task<Response<List<GetUserDto>>> GetAll();
    public Task<Response<GetUserDto>> Get(int id);
    public Task<Response<GetUserDto>> Update(int id,UpdateUserDto updateUserDto);
    public Task<Response<string>> Delete(int id);
    public Task<Response<GetUserDto>> Create(CreateUserDto createUserDto);

}
