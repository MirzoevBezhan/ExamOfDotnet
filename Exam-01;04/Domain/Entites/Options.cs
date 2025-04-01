namespace Domain.Entites;

public class Options
{
    public int id { get; set; }
    public int questionId { get; set; }
    public string optionText { get; set; }
    public bool isCorrect { get; set; }
    public char optionLetter { get; set; }
}
