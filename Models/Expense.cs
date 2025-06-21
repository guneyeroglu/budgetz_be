using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetZ.API.Models;

[Table("expenses")]
public class Expense
{
    // Otomatik artan benzersiz tanımlayıcı
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    // Kullanıcı ID'si
    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }

    // Kategori ID'si
    [Required]
    [Column("category_id")]
    public int CategoryId { get; set; }

    // Harcama miktarı
    [Required]
    [Column("amount", TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    // Harcama açıklaması
    [Column("description")]
    public string? Description { get; set; }

    // Harcama tarihi
    [Required]
    [Column("expense_date")]
    public DateTime ExpenseDate { get; set; }

    // Oluşturma anında otomatik set edilir, değiştirilemez
    [Column("created_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    // Güncelleme işlemlerinde otomatik set edilir
    [Column("updated_at")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? UpdatedAt { get; set; }

    // Kullanıcı
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    // Kategori
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; } = null!;
} 