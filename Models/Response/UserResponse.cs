namespace BudgetZ.API.Models.Response;

// Kullanıcı bilgilerini döndürmek için kullanılan response modeli
public class UserResponse
{
    // Kullanıcının benzersiz tanımlayıcısı
    public Guid Id { get; set; }
    // Kullanıcı adı
    public string Username { get; set; } = string.Empty;
    // Kullanıcının admin olup olmadığı
    public bool IsAdmin { get; set; }
} 