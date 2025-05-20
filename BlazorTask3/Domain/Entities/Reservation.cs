    using System.ComponentModel.DataAnnotations;

    namespace Domain.Entities;

    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TableId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        
        public virtual Customer Customer { get; set; }
        public virtual Table Table { get; set; }
    }