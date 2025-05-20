namespace Domain.Dtos.Table;

public class CreateTableDto
{
    public int Number { get; set; }
    public int Seats { get; set; }
    public bool IsReserved { get; set; }
}