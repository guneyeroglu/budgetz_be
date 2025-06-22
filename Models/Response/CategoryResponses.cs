namespace BudgetZ.API.Models.Response;

// Kategori liste elemanı için response modeli
public class CategoryListItemResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

// Kategori listesi için response modeli
public class CategoryListResponse
{
    public List<CategoryListItemResponse> Items { get; set; } = new();
} 