namespace BudgetZ.API.Models.Response;

// Kullanıcı kaydı yanıtı için response modeli
public class UserRegisterResponse
{
    // Kullanıcının benzersiz tanımlayıcısı
    public Guid Id { get; set; }
    // Kullanıcı adı
    public string Username { get; set; } = string.Empty;
    // Kullanıcının admin olup olmadığı
    public bool IsAdmin { get; set; }
}

// Kullanıcı girişi sonrası döndürülecek response modeli
public class UserLoginResponse : UserRegisterResponse
{
    // JWT token
    public string Token { get; set; } = string.Empty;
} 