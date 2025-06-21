using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetZ.API.Models;

// Kullanıcıların harcamalarını kategorize etmek için kullanılan model
[Table("categories")]
public class Category
{
    // Otomatik artan benzersiz tanımlayıcı
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Kategori adı
    [Required]
    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
} 