using Microsoft.AspNetCore.Mvc;

namespace NguyenThanhPhu_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            // Lấy thông tin đơn hàng theo ID
            return View();
        }

        public IActionResult Delete(int id)
        {
            // Xử lý xóa đơn hàng
            return RedirectToAction("Index");
        }
    }
}
