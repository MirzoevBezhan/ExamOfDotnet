namespace Domain.DTOs.Booking;

public class CreateBookingDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public int CarId { get; set; }
    public string CarModel { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal TotalPrice { get; set; }

}
