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
using X.PagedList;
namespace MVC.Controllers
{
    public class ClothesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClothesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Nguyen Huy Tuong 2021050718

        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Tạo sản phẩm
        /// </summary>
        /// <param name="clothes"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClothesID,ClothesName,Number,Color")] Clothes clothes)
        {
            if (ModelState.IsValid)
            {
                clothes.Status = 0;
                _context.Add(clothes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        // Nguyen Huy Tuong 2021050718
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0 || _context.Clothes == null)
            {
                return NotFound();
            }

            var clothesData = await _context.Clothes
           .Where(c => c.Id == id) // Adjust the condition as needed
           .FirstOrDefaultAsync();

            if (clothesData == null)
            {
                return NotFound();
            }

            return View(clothesData);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || _context.Clothes == null)
            {
                return NotFound();
            }

            var clothesData = await _context.Clothes
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

            if (clothesData == null)
            {
                return NotFound();
            }

            return View(clothesData);
        }

        /// <summary>
        /// Delete khi click button đồng ý
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clothes == null)
            {
                return Problem("Không tìm thấy sản phẩm");
            }
            var clothes = await _context.Clothes.FindAsync(id);
            if (clothes != null)
            {
                _context.Clothes.Remove(clothes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Màn index
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        
        //Nguyen Huy Tuong 2021050718
        public async Task<IActionResult> Index(ClothesViewModel searchModel, int? page)
        {
            if (searchModel.ClearFilter)
            {
                // Đặt lại mô hình tìm kiếm về trạng thái ban đầu
                searchModel = new ClothesViewModel();
            }
            // Assuming your Clothes class has a navigation property named Book
            List<ClothesViewModel> result = await _context.Clothes
            .Select(clothes => new ClothesViewModel
            {
                Id = clothes.Id,
                ClothesID = clothes.ClothesID,
                ClothesName = clothes.ClothesName,
                Color = clothes.Color,
                Number = clothes.Number,
                Status = clothes.Status
            })
        .ToListAsync();
            // Áp dụng điều kiện tìm kiếm
            if (!string.IsNullOrEmpty(searchModel.ClothesName))
            {
                result = result.Where(s => s.ClothesName.Contains(searchModel.ClothesName)).ToList();
            }

            if (!string.IsNullOrEmpty(searchModel.ClothesID))
            {
                result = result.Where(s => s.ClothesID == searchModel.ClothesID).ToList();
            }
            if (searchModel.Status == 0)
            {

            }
            else if (searchModel.Status == 1)
            {
                result = result.Where(s => s.Status == 0).ToList();
            }
            else if (searchModel.Status == 2)
            {
                result = result.Where(s => s.Status == 1).ToList();
            }
            int pageNumber = page ?? 1;
            int pageSize = 5;

            // Phân trang
            IPagedList<ClothesViewModel> pagedList = await result.ToPagedListAsync(pageNumber, pageSize);
            return View(pagedList);
        }
        /// <summary>
        /// Export excel
        /// </summary>
        /// <returns></returns>
        

        //Vu Ngoc Tuyen 2021050715
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy danh sách sinh viên từ cơ sở dữ liệu
            List<ClothesViewModel> result = await _context.Clothes
            .Select(clothes => new ClothesViewModel
            {
                Id = clothes.Id,
                ClothesID = clothes.ClothesID,
                ClothesName = clothes.ClothesName,
                Color = clothes.Color,
                Number = clothes.Number,
                Status = clothes.Status
            })
            .ToListAsync();
            // Vu Ngoc Tuyen 2021050715

            // Tạo một đối tượng ExcelPackage
            using (var package = new ExcelPackage())
            {
                // Tạo một trang tính mới
                var worksheet = package.Workbook.Worksheets.Add("SinhVien");

                // Ghi dữ liệu vào trang tính
                worksheet.Cells["A1"].Value = "ID";
                worksheet.Cells["B1"].Value = "Mã quần áo";
                worksheet.Cells["C1"].Value = "Tên quần áo";
                worksheet.Cells["D1"].Value = "Số lượng";
                worksheet.Cells["E1"].Value = "Màu sắc";
                worksheet.Cells["F1"].Value = "Trạng thái";

                var row = 2;
                foreach (var sinhVien in result)
                {
                    worksheet.Cells[$"A{row}"].Value = sinhVien.Id;
                    worksheet.Cells[$"B{row}"].Value = sinhVien.ClothesID;
                    worksheet.Cells[$"C{row}"].Value = sinhVien.ClothesName;
                    worksheet.Cells[$"D{row}"].Value = sinhVien.Number;
                    worksheet.Cells[$"E{row}"].Value = sinhVien.Color;
                    if (sinhVien.Status == 0)
                    {
                        worksheet.Cells[$"F{row}"].Value = "Đang bán";
                    }
                    else if (sinhVien.Status == 1)
                    {
                        worksheet.Cells[$"F{row}"].Value = "Đã ngưng bán";
                    }
                    row++;
                }

                // Đặt kích thước cột tự động dựa trên nội dung
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Lưu ExcelPackage vào một mảng byte
                var excelBytes = package.GetAsByteArray();

                // Xác định loại file MIME
                var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                // Trả về FileResult
                return File(excelBytes, mimeType, "SinhVien.xlsx");
            }
        }

        /// <summary>
        /// Edit sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.Clothes == null)
            {
                return NotFound();
            }

            var clothesData = await _context.Clothes
            .Where(c => c.Id == id) // Adjust the condition as needed
            .FirstOrDefaultAsync();


            if (clothesData == null)
            {
                return NotFound();
            }
            return View(clothesData);
        }

        /// <summary>
        /// Edit khi click đồng ý
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clothes"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClothesID,ClothesName,Number,Color,Status")] Clothes clothes)
        {
            if (id != clothes.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(clothes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clothes);
        }
        /// <summary>
        /// Thống kê
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ThongKe()
        {
            // Lấy tổng số sản phẩm từ cơ sở dữ liệu
            int total = await _context.Clothes.CountAsync();
            // Lấy tổng số đang bán từ cơ sở dữ liệu
            int totalCart = await _context.Clothes.CountAsync(s => s.Status == 0);
            // Lấy tổng sản phẩm đã ngưng bán từ cơ sở dữ liệu
            int totalCartStop = await _context.Clothes.CountAsync(s => s.Status == 1);

            // Gán giá trị vào ViewBag để truyền đến view
            ViewBag.Total = total;
            ViewBag.TotalCart = totalCart;
            ViewBag.TotalCartStop = totalCartStop;
            return View();
        }
    }
}
