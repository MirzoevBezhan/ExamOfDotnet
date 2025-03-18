using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Npgsql;

namespace Infastructure.Services;

public class PostsService
{
    private const string connectionString = "Host=locahost;Username=postgres;Password=ipo90;Database=Exam2_db";
    public int AddPost(Post post)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "insert into posts (Content,CreationDate,LikesCount,UserId) values (@Content,@CreationDate,@LikesCount,@UserId)";
            var res = con.Execute(cmd, post);
            return res;
        }
    }
    public List<Post> GetAllPosts()
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select * from posts";
            var res = con.Query<Post>(cmd).ToList();
            return res;
        }
    }
    public User GetPostById(int PostId)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select * from posts where PostId = @postId";
            var res = con.QuerySingleOrDefault(cmd, new { postId = PostId });
            return res;
        }
    }
    public int UpdatePost(Post post)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "update posts set Content = @Content, CreationDate = @CreationDate, LikesCount = @LikesCount , UserId = @UserId where PostId = @Id";
            var res = con.Execute(cmd, post);
            return res;
        }
    }
    public int DeletePost(int id)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "delete from posts where PostId = @id";
            var res = con.Execute(cmd, new { id = id });
            return res;
        }
    }
      public List<PostsWithCommentsCount> Task3(int Id)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select p.*,count(c.UserId) as CountComments from posts as p join users as u on u.UserId = p.UserId join comments as c on c.UserId = u.UserId where u.userId = @Id group by p.PostId";
            var res = con.Query<PostsWithCommentsCount>(cmd, new { Id = Id }).ToList();
            return res;
        }
    }
    
      public List<TopComments> Task4()
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = @"select  p.Content,count(l.likeId),count(c.CommentId) from posts AS p join comments as c on c.PostId = p.PostId join likes as l on l.PostId = p.PostId group by p.Content order by count(l.LikeId)desc limit 5";
             var res = con.Query<TopComments>(cmd).ToList();
            return res;
        }
    }
    


}
