using Domain.Entites;
using Domain.Responces;

namespace Infrastructure.Interface;

public interface IOptionService
{
    public Task<Responce<List<Options>>> GetAll();
    public Task<Responce<Options>> GetOptions(int id);
    public Task<Responce<string>> AddOption(Options options);
    public Task<Responce<string>> UpdateOption(Options options);
    public Task<Responce<string>> DeleteOption(int id);
    public Task<Responce<string>> Import();
    public Task<Responce<string>> Export();
}
