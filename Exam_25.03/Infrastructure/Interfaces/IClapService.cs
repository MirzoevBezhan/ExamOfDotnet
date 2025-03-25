using Domain.Entitites;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IClapService
{
    public Task<Response<List<Clap>>> GetAll();
    public Task<Response<Clap>> GetClap(int id);
    public Task<Response<string>> AddClap(Clap clap);
    public Task<Response<string>> UpdateClap(Clap clap);
    public Task<Response<string>> DeleteClap(int id);

}
