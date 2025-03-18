using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Npgsql;

namespace Infastructure.Services;

public class LikesService
{
    private const string connectionString = "Host=locahost;Username=postgres;Password=ipo90;Database=Exam2_db";
    public int AddLike(Likes likes)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "insert into likes (LikeDate,UserId,PostId) values (@LikeDate,@UserId,@PostID)";
            var res = con.Execute(cmd, likes);
            return res;
        }
    }
    public List<Likes> GetAllLikes()
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select * from likes";
            var res = con.Query<Likes>(cmd).ToList();
            return res;
        }
    }
    public Likes GetLikeById(int likeId)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select * from likes where likeId = @LikesId";
            var res = con.QuerySingleOrDefault(cmd, new { LikeId = likeId });
            return res;
        }
    }
    public int UpdateLike(Likes like)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "update likes set LikeDate = @LikeDate, UserId = @UserId, PostId = @PostId  where LikeId = @Id";
            var res = con.Execute(cmd, like);
            return res;
        }
    }
    public int DeleteLike(int id)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "delete from likes where LikeId = @id";
            var res = con.Execute(cmd, new { id = id });
            return res;
        }
    }
    
    public int Task2(int id)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select likesCount from posts where postId = @id";
            var res = con.Execute(cmd, new { id = id });
            return res;
        }

    
    }   
}
