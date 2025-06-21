namespace BudgetZ.API.Models.Response;

// API yanıtları için genel response modeli
public class ApiResponse<T>
{
    // API işlemi başarılı olup olmadığını gösterir
    public bool Success { get; set; }
    // API işlemi hakkında mesaj
    public string Message { get; set; } = string.Empty;
    // API işlemi sonucu döndürülecek veri
    public T? Data { get; set; }
} 