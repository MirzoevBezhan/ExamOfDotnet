using Dapper;
using Domain.Entities;
using Npgsql;

namespace Infastructure.Services;

public class UsersService
{
    private const string connectionString = "Host=locahost;Username=postgres;Password=ipo90;Database=Exam2_db";
    public int AddUser(User user)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "insert into users (Username,Email,FullName,RegistrationDate) values (@Username,@Email,@FullName,@RegistrationDate)";
            var res = con.Execute(cmd, user);
            return res;
        }
    }
    public List<User> GetAllUsers()
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select * from users";
            var res = con.Query<User>(cmd).ToList();
            return res;
        }
    }
    public User GetUserById(int userId)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select * from users where userId = @Id";
            var res = con.QuerySingleOrDefault(cmd, new { Id = userId });
            return res;
        }
    }
    public int UpdateUser(User user)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "update users set Username = @Username, Email = @Email, Fullname = @Fullname , RegistrationDate = @RegistrationDate where userId = @Id";
            var res = con.Execute(cmd, user);
            return res;
        }
    }
    public int DeleteUser(int id)
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "delete from users where userId = @id";
            var res = con.Execute(cmd, new { id = id });
            return res;
        }
    }

    public List<User> Task1()
    {
        using (var con = new NpgsqlConnection(connectionString))
        {
            con.Open();
            var cmd = "select * from users order by RegistrationDate";
            var res = con.Query<User>(cmd).ToList();
            return res;
        }
    }
    
}
