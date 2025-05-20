using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Branch
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Location { get; set; }
    
    public virtual List<Car> Cars { get; set; }
}