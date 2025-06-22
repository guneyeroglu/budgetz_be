namespace BudgetZ.API.Models.Response;

// Harcama liste elemanı için response modeli
public class ExpenseListItemResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime ExpenseDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public CategoryListItemResponse Category { get; set; } = null!;
}

// Harcama listesi için response modeli
public class ExpenseListResponse
{
    public List<ExpenseListItemResponse> Items { get; set; } = new();
}
