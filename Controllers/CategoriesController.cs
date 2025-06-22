using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetZ.API.Data;
using BudgetZ.API.Models;
using BudgetZ.API.Models.Response;

namespace BudgetZ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Tüm kategorileri listele
    [HttpGet]
    public async Task<ActionResult<ApiResponse<CategoryListResponse>>> GetCategories()
    {
        try
        {
            var categories = await _context.Categories
                .Select(c => new CategoryListItemResponse
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return Ok(new ApiResponse<CategoryListResponse>
            {
                Success = true,
                Message = "Kategoriler başarıyla getirildi.",
                Data = new CategoryListResponse { Items = categories }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<CategoryListResponse>
            {
                Success = false,
                Message = "Kategoriler getirilirken bir hata oluştu.",
                Data = null
            });
        }
    }
} 