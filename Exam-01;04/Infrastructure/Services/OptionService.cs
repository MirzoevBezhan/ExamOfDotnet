using System.Net;
using Dapper;
using Domain.Entites;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interface;

namespace Infrastructure.Services;

public class OptionService(DataContext Context) : IOptionService
{
    public async Task<Responce<string>> AddOption(Options options)
    {
        using var connection = await Context.GetConnection();
        var cmd = "insert into options (questionId,optionText,isCorrect,optionLetter) values (@questionId,@optionText,@isCorrect,@optionLetter)";
        var res = await connection.ExecuteAsync(cmd, options);
        return res == 0 ? new Responce<string>(HttpStatusCode.BadRequest, "Maybe something went wrong") :
        new Responce<string>("Option Added");
    }
    public async Task<Responce<string>> DeleteOption(int id)
    {
        using var connection = await Context.GetConnection();
        var cmd = "delete from options where id = @id";
        var res = await connection.ExecuteAsync(cmd, new { id = id });
        return res == 0 ? new Responce<string>(HttpStatusCode.BadRequest, "Not found") :
        new Responce<string>("Option Deleted");
    }
    public async Task<Responce<List<Options>>> GetAll()
    {
        using var connection = await Context.GetConnection();
        var cmd = "select * from options";
        var res = await connection.QueryAsync<Options>(cmd);
        return new Responce<List<Options>>(res.ToList());
    }
    public async Task<Responce<Options>> GetOptions(int id)
    {
        using var connection = await Context.GetConnection();
        var cmd = "select * from options where id = @id";
        var res = await connection.QueryFirstOrDefaultAsync<Options>(cmd, new { id = id });
        return res == null ? new Responce<Options>(HttpStatusCode.BadRequest, "Not found") :
         new Responce<Options>(res);
    }

    public async Task<Responce<string>> UpdateOption(Options options)
    {
        using var connection = await Context.GetConnection();
        var cmd = "update options set questionId = @questionId , optionText = @optionText , isCorrect = @isCorrect , optionLetter = @optionLetter where id = @id"; var res = await connection.ExecuteAsync(cmd, options);
        return res == 0 ? new Responce<string>(HttpStatusCode.BadRequest, "Maybe something went wrong") :
        new Responce<string>("Option Updated");
    }

    public async Task<Responce<string>> Import()
    {
        const string way = "D:\\Dotnet\\Exams\\Exam-01;04\\Domain\\anatomy.txt";
        var cmd = "insert into options (questionId,optionText,isCorrect,optionLetter) values (@questionId,@optionText,@isCorrect,@optionLetter) returning id ";
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
            var option = new Options
            {
                id = line[0],
                questionId = line[1],
                // optionText = line[2],
                // isCorrect = line[3],
                optionLetter = line[4],
            };
            var result = await connection.ExecuteAsync(cmd, option);
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
        var cmd = "select * from options";

        var list = new List<string>();

        if (File.Exists(way))
        {
            return new Responce<string>(HttpStatusCode.BadRequest, "Not found");
        }
        using var connection = await Context.GetConnection();
        var options = await connection.QueryAsync<Options>(cmd);
        foreach (var option in options.ToList())
        {
            var value = $"{option.id},{option.questionId},{option.optionText},{option.isCorrect},{option.optionLetter}";
            list.Add(value);
        }
        await File.WriteAllLinesAsync(way, list);
        return new Responce<string>("All is okay");
    }
    public async Task<Responce<string>> Analyze()
    {
        string path = "D:\\Dotnet\\Exams\\Exam-01;04";
        var CountAllQuestions = "select count(*) from questions";

    }
}
