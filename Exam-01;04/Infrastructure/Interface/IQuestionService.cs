using Domain.Entites;
using Domain.Responces;

namespace Infrastructure.Interface;

public interface IQuestionService
{
    public Task<Responce<List<Question>>> GetAll();
    public Task<Responce<Question>> GetQuestion(int id);
    public Task<Responce<string>> AddQuestion(Question question);
    public Task<Responce<string>> UpdateQuestion(Question question);
    public Task<Responce<string>> DeleteQuestion(int id);
    public Task<Responce<string>> Import();
    public Task<Responce<string>> Export();
}
