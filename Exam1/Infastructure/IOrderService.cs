using Domain;

namespace Infastructure;

public interface IOrderService
{
    public void GetOrdersCoutByCustomer();
    public List<string> GetCustomerWithMaxOrders();
    public List<Orders> GetOrdersAboveAverageTotalPrice();
    public List<Orders> GetOrdersByCustomer(int customerId);
    public List<Orders> GetNewOrdersSortedByDate();
    public List<Orders> GetTotalOrderValueByCustomer();
}
