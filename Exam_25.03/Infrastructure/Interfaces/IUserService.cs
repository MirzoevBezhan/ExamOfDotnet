using Domain.Entitites;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    public Task<Response<List<User>>> GetAll();
    public Task<Response<User>> GetUser(int id);
    public Task<Response<string>> AddUser(User user);
    public Task<Response<string>> UpdateUser(User user);
    public Task<Response<string>> DeleteUser(int id);
    
}
