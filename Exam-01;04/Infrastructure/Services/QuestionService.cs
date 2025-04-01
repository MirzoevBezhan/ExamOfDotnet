using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Entites;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interface;

namespace Infrastructure.Services;

public class QuestionService(DataContext Context) : IQuestionService
{
    public async Task<Responce<string>> AddQuestion(Question question)
    {
        using var connection = await Context.GetConnection();
        var cmd = "insert into questions (questionText) values (@questionText)";
        var res = await connection.ExecuteAsync(cmd, question);
        return res == 0 ? new Responce<string>(HttpStatusCode.BadRequest, "Maybe something went wrong") :
        new Responce<string>("Question Added");
    }

    public async Task<Responce<string>> DeleteQuestion(int id)
    {
        using var connection = await Context.GetConnection();
        var cmd = "delete from questions where id = @id";
        var res = await connection.ExecuteAsync(cmd, new { id = id });
        return res == 0 ? new Responce<string>(HttpStatusCode.BadRequest, "Not found") :
        new Responce<string>("Question Deleted");
    }

    public async Task<Responce<List<Question>>> GetAll()
    {
        using var connection = await Context.GetConnection();
        var cmd = "select * from questions";
        var res = await connection.QueryAsync<Question>(cmd);
        return new Responce<List<Question>>(res.ToList());
    }

    public async Task<Responce<Question>> GetQuestion(int id)
    {
        using var connection = await Context.GetConnection();
        var cmd = "select * from questions where id = @id";
        var res = await connection.QueryFirstOrDefaultAsync<Question>(cmd, new { id = id });
        return res == null ? new Responce<Question>(HttpStatusCode.BadRequest, "Not found") :
         new Responce<Question>(res);
    }

    public async Task<Responce<string>> UpdateQuestion(Question question)
    {
        using var connection = await Context.GetConnection();
        var cmd = "update questions set questionText = @questionText where id = @id"; var res = await connection.ExecuteAsync(cmd, question);
        return res == 0 ? new Responce<string>(HttpStatusCode.BadRequest, "Maybe something went wrong") :
        new Responce<string>("Question Updated");
    }
    public async Task<Responce<string>> Import()
    {
        const string way = "D:\\Dotnet\\Exams\\Exam-01;04\\Domain\\anatomy.txt";
        var cmd = "insert into questions (QuestionText) values (@QuestionText)";
        if (File.Exists(way))
        {
            return new Responce<string>(HttpStatusCode.BadRequest, "Not found");
        }
        var lines = await File.ReadAllLinesAsync(way);

        using var connection = await Context.GetConnection();

        var cnt = 0;
        foreach (var line in lines)
        {
            var values = line.Split(',');
            var question = new Question
            {
                QuestionText = values[0],
            };
            var result = await connection.ExecuteAsync(cmd, question);
            if (result == 1)
            {
                cnt++;
            }
        }
        return new Responce<string>("Operation finished");
    }
    public async Task<Responce<string>> Export()
    {
        const string way = "D:\\Dotnet\\Exams\\Exam-01;04\\Domain\\anatomy.txt";
        var cmd = "select * from questions";

        var list = new List<string>();

        if (File.Exists(way))
        {
            return new Responce<string>(HttpStatusCode.BadRequest, "Not found");
        }
        using var connection = await Context.GetConnection();
        var questions = await connection.QueryAsync<Question>(cmd);
        foreach (var question in questions.ToList())
        {
            var value = $"{question.id},{question.QuestionText}";
            list.Add(value);
        }
        await File.WriteAllLinesAsync(way, list);
        return new Responce<string>("All is okay");
    }
    
    public async Task<Responce<string>> Analize()
    {
        const string way = "D:\\Dotnet\\Exams\\Exam-01;04\\Domain\\anatomy.txt";
        var cmd = @"select count(q.*),count(o.*) from questions as q
         join options as o on q.id = o.questionId ";
         
        var cmd2 = "select optionLetter, count(*) from options group by optionLetter";

        var list = new List<string>();

        if (File.Exists(way))
        {
            return new Responce<string>(HttpStatusCode.BadRequest, "Not found");
        }

        
        using var connection = await Context.GetConnection();
        

        var question = await connection.QueryFirstOrDefaultAsync<CountQuestionAndOptions>(cmd);
        
        await File.WriteAllLinesAsync(way, list);

        System.Console.WriteLine($"All questions : {question.id}");
        System.Console.WriteLine($"All options : {question.QuestionId}");
       
        return new Responce<string>("All is okay");
        
    }
    

}
