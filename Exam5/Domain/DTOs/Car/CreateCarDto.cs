namespace Domain.DTOs.Car;

public class CreateCarDto
{
  public string Model { get; set; }
  public decimal PricePerDay { get; set; }
  public bool IsAvailable { get; set; }
  public int BookingCount { get; set; }

}

