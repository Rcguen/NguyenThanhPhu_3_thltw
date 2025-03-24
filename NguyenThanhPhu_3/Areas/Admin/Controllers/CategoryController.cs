using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NguyenThanhPhu_3.Models;
using NguyenThanhPhu_3.Repositories;
using System.Threading.Tasks;

namespace NguyenThanhPhu_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Chỉ Admin mới có quyền
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }

        // Hiển thị form thêm danh mục
        public IActionResult Add()
        {
            return View();
        }

        // Xử lý thêm danh mục
        [HttpPost]
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest("Tên danh mục không được để trống.");
            }

            var category = new Category { Name = categoryName };
            await _categoryRepository.AddAsync(category);

            // Trả về danh sách danh mục cập nhật
            var categories = await _categoryRepository.GetAllAsync();
            return Json(categories);
        }
        // Hiển thị form cập nhật danh mục
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // Xử lý cập nhật danh mục
        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // Hiển thị trang xác nhận xóa danh mục
        [HttpPost("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound("Danh mục không tồn tại.");
            }

            await _categoryRepository.DeleteAsync(categoryId);
            var categories = await _categoryRepository.GetAllAsync();
            return Json(categories);
        }


        // Xử lý xóa danh mục
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
