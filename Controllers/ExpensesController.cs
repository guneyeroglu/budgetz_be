using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BudgetZ.API.Data;
using BudgetZ.API.Models;
using BudgetZ.API.Models.Response;

namespace BudgetZ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Tüm controller'daki endpoint'ler için authentication gerekli
public class ExpensesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ExpensesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Tüm harcamaları listele
    [HttpGet]
    public async Task<ActionResult<ApiResponse<ExpenseListResponse>>> GetExpenses()
    {
        try
        {
            var expenses = await _context.Expenses
                .Include(e => e.Category)
                .Select(e => new ExpenseListItemResponse
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Description = e.Description,
                    ExpenseDate = e.ExpenseDate,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt,
                    Category = new CategoryListItemResponse
                    {
                        Id = e.Category.Id,
                        Name = e.Category.Name
                    }
                })
                .ToListAsync();

            return Ok(new ApiResponse<ExpenseListResponse>
            {
                Success = true,
                Message = "Harcamalar başarıyla getirildi.",
                Data = new ExpenseListResponse { Items = expenses }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<ExpenseListResponse>
            {
                Success = false,
                Message = "Harcamalar getirilirken bir hata oluştu.",
                Data = null
            });
        }
    }

    // ID'ye göre harcama detayını getir
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ExpenseListItemResponse>>> GetExpenseById(Guid id)
    {
        try
        {
            var expense = await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.Id == id)
                .Select(e => new ExpenseListItemResponse
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Description = e.Description,
                    ExpenseDate = e.ExpenseDate,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt,
                    Category = new CategoryListItemResponse
                    {
                        Id = e.Category.Id,
                        Name = e.Category.Name
                    }
                })
                .FirstOrDefaultAsync();

            if (expense == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, new ApiResponse<ExpenseListItemResponse>
                {
                    Success = false,
                    Message = "Belirtilen ID'ye sahip harcama bulunamadı.",
                    Data = null
                });
            }

            return Ok(new ApiResponse<ExpenseListItemResponse>
            {
                Success = true,
                Message = "Harcama detayı başarıyla getirildi.",
                Data = expense
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<ExpenseListItemResponse>
            {
                Success = false,
                Message = "Harcama detayı getirilirken bir hata oluştu.",
                Data = null
            });
        }
    }
} 