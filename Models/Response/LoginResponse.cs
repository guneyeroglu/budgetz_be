namespace BudgetZ.API.Models.Response;

// Kullanıcı girişi sonrası döndürülecek response modeli
public class LoginResponse
{
    // Kullanıcı bilgileri
    public UserResponse User { get; set; } = null!;
    // JWT token
    public string Token { get; set; } = string.Empty;
} 