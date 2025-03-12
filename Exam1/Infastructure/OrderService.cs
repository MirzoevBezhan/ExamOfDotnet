using System.Data;
using Domain;
using Npgsql;

namespace Infastructure;

public class OrderService : IOrderService
{
    private string connectionString = "Host=localhost,Username=postgres,Password=ipo90,DataBase=Exam_db";
    public void GetOrdersCoutByCustomer()
    {

        List<Customer> customers = new List<Customer>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var cmd = $"select c.firts_name,count(*) from orders as o JOIN customers as c on c.customer_id = o.customer_id group by c.firts_name ";
            var command = new NpgsqlDataAdapter(cmd, connection);
            DataSet dt = new DataSet();
            command.Fill(dt);

            foreach (DataTable dataTable in dt.Tables)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    System.Console.WriteLine($"{column.ColumnName}\t");
                    System.Console.WriteLine();
                }
                foreach (DataRow rows in dataTable.Rows)
                {
                    var cells = rows.ItemArray;
                    foreach (object row in cells)
                    {
                        System.Console.WriteLine($"{row}\t");
                    }
                }

            }

        }
    }
    public List<string> GetCustomerWithMaxOrders()
    {
        var res = new List<string>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var cmd = $"select c.firts_name,max(o.order_id) as max_orders from orders as o JOIN customers as c on c.customer_id = o.customer_id group by c.firts_name,o.order_id order by max_orders desc limit 1";
            var command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                string name;
                while (reader.Read())
                {
                    name = reader.GetString(0);
                    res.Add(name);
                }
            }
        }
                return res;
    }
}

