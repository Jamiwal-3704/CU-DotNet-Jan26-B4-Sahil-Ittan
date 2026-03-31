using System.ComponentModel.DataAnnotations;

namespace Week11_Assessement.Models;

public class EditProductInputModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 1000000)]
    public decimal BasePrice { get; set; }
}
