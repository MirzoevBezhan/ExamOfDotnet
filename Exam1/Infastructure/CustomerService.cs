using System.Data;
using Domain;
using Npgsql;

namespace Infastructure;

public class CustomerService : ICustomerService
{
        private string connectionString = "Host=localhost,Username=postgres,Password=ipo90,DataBase=Exam_db";

    public void GetCustomersRegisteredBetween(DateTime startDate, DateTime endDate)
    {
        List<Customer> customers = new List<Customer>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var cmd = $"select * from customers where registration_date between {startDate} and {endDate} order by registration_date";
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

}