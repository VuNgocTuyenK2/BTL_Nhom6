using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using OfficeOpenXml;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Tạo mới sinh viên mượn sách
        /// </summary>
        /// <param name="sinhVien"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,PassWord,Name,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> Login(User login)
        {
            // Kiểm tra xem có người dùng có tên đăng nhập và mật khẩu tương ứng không
            var user = await _context.User
            .FirstOrDefaultAsync(u => u.UserName == login.UserName && u.PassWord == login.PassWord);

            if (user != null)
            {
                // Nếu có một người dùng tương ứng, đăng nhập thành công
                return RedirectToAction("Index", "Clothes");
            }
            else
            {
                // Xử lý tài khoản không tồn tại hoặc mật khẩu không đúng
                return Problem("Đăng nhập thất bại, hãy thử lại");
            }
        }
    }
}
