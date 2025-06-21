using BudgetZ.API.Models;

namespace BudgetZ.API.Services;

// JWT token işlemleri için servis arayüzü
public interface ITokenService
{
    // Verilen kullanıcı için JWT token oluşturur
    string CreateToken(User user);
} 