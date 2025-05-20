namespace Domain.Dtos.Reservation;

public class CreateReservationDto 
{
    public int TableId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}