using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenThanhPhu_3.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Customer")]
public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> MyOrderItems()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login", "Account");

        var orderItems = await _context.OrderDetails
            .Include(od => od.Product)
            .Include(od => od.Order) // Lấy thông tin đơn hàng
            .Where(od => od.Order.UserId == user.Id)
            .OrderByDescending(od => od.Order.OrderDate) // Sắp xếp theo ngày đặt
            .ToListAsync();

        return View(orderItems);
    }

}
