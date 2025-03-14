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
    public List<Orders> GetOrdersAboveAverageTotalPrice()
    {
        var res = new List<Orders>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var cmd = "select * from orders where total_cost > (select avg(total_cost) from orders)";
            var command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                var order = new Orders();
                while (reader.Read())
                {
                    order.order_id = reader.GetInt32(0);
                    order.customer_id = reader.GetInt32(1);
                    order.product_id = reader.GetInt32(2);
                    order.order_date = reader.GetDateTime(3);
                    order.quantity = reader.GetInt32(4);
                    order.total_price = reader.GetDecimal(5);
                    order.status = reader.GetString(6);
                    res.Add(order);
                }
            }
        }
        return res;
    }

    public List<Orders> GetOrdersByCustomer(int customerId)
    {
        var res = new List<Orders>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var cmd = "SELECT o.order_id, o.total_cost, c.customer_name FROM orders as o join customers as c ON o.customer_id = c.customer_id where c.customer_name = @customerName";
            var command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                var order = new Orders();
                while (reader.Read())
                {
                    order.order_id = reader.GetInt32(0);
                    order.customer_id = reader.GetInt32(1);
                    order.product_id = reader.GetInt32(2);
                    order.order_date = reader.GetDateTime(3);
                    order.quantity = reader.GetInt32(4);
                    order.total_price = reader.GetDecimal(5);
                    order.status = reader.GetString(6);
                    res.Add(order);
                }
            }
        }
        return res;
    }

    public List<Orders> GetNewOrdersSortedByDate()
    {
        var res = new List<Orders>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var cmd = "select * from orders where status = 'новый' order by order_date";
            var command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                var order = new Orders();
                while (reader.Read())
                {
                    order.order_id = reader.GetInt32(0);
                    order.customer_id = reader.GetInt32(1);
                    order.product_id = reader.GetInt32(2);
                    order.order_date = reader.GetDateTime(3);
                    order.quantity = reader.GetInt32(4);
                    order.total_price = reader.GetDecimal(5);
                    order.status = reader.GetString(6);
                    res.Add(order);
                }
            }
        }
        return res;
    }
    public List<Orders> GetTotalOrderValueByCustomer()
    {
        var res = new List<Orders>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var cmd = "select c.customer_id, c.customer_name, sum(o.total_cost) as hamagish from customers c join orders o on c.customer_id = o.customer_id group by c.customer_id, c.customer_name order by total_order_cost desc";
            var command = new NpgsqlCommand(cmd, connection);
            using (var reader = command.ExecuteReader())
            {
                var order = new Orders();
                while (reader.Read())
                {
                    order.order_id = reader.GetInt32(0);
                    order.customer_id = reader.GetInt32(1);
                    order.product_id = reader.GetInt32(2);
                    order.order_date = reader.GetDateTime(3);
                    order.quantity = reader.GetInt32(4);
                    order.total_price = reader.GetDecimal(5);
                    order.status = reader.GetString(6);
                    res.Add(order);
                }
            }
        }
        return res;
    }


}

