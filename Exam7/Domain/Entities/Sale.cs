    using System.ComponentModel.DataAnnotations;

    namespace Domain.Entities;

    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantitySold { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public virtual Product Product { get; set; }
    }
