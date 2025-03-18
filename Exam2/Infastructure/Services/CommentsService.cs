using Dapper;
using Domain.Entities;
using Npgsql;

namespace Infastructure.Services;

public class CommentsService
{
    private const string connectionString = "Host=locahost;Username=postgres;Password=ipo90;Database=Exam2_db";
    public int AddComment(Comment comment)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "insert into comments (Content,CreationDate,UserId,PostId) values (@Content,@CreationDate,@UserId,@PostId)";
            var res = con.Execute(cmd, comment);
            return res;
        }
    }
    public List<Comment> GetAllComments()
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select * from comments";
            var res = con.Query<Comment>(cmd).ToList();
            return res;
        }
    }
    public Comment GetCommentById(int commentId)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select * from comments where commentId = @commentId";
            var res = con.QuerySingleOrDefault(cmd, new { commentId = commentId });
            return res;
        }
    }
    public int UpdateComment(Comment comment)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "update comments set Content = @Content, CreationDate = @CreationDate, UserId = @UserId,PostId =@PostId  where commentId = @Id";
            var res = con.Execute(cmd, comment);
            return res;
        }
    }
    public int DeleteComment(int id)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "delete from comments where commentId = @id";
            var res = con.Execute(cmd, new { id = id });
            return res;
        }
    }
}
