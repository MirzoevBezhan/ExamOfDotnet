using Domain;

namespace Infastructure;

public interface ICustomerService
{
    public void GetCustomersRegisteredBetween(DateTime startDate,DateTime endDate);

}
