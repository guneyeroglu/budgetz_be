using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetZ.API.Models;

[Table("users")]
public class User
{
    // Otomatik artan benzersiz tanımlayıcı
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    // Kullanıcı adı
    [Required]
    [Column("username")]
    [StringLength(20, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    // Şifre
    [Required]
    [Column("password")]
    [StringLength(16, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    // Admin yetkisi
    [Required]
    [Column("is_admin")]
    public bool IsAdmin { get; set; }

    // Oluşturma anında otomatik set edilir, değiştirilemez
    [Column("created_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    // Güncelleme işlemlerinde otomatik set edilir
    [Column("updated_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedAt { get; set; }
} 