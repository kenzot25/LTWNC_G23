using G23NHNT.Models;
using G23NHNT.Repositories;
using G23NHNT.Services;
using G23NHNT.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace G23NHNT.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomService _roomService;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly G23_NHNTContext _context;

        public RoomController(RoomService roomService, IAmenityRepository amenityRepository, IWebHostEnvironment webHostEnvironment, G23_NHNTContext context)
        {
            _roomService = roomService;
            _amenityRepository = amenityRepository;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new RoomPostViewModel
            {
                Amenities = (await _amenityRepository.GetAllAmenitiesAsync()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomPostViewModel model, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                model.Amenities = (await _amenityRepository.GetAllAmenitiesAsync()).ToList();
                model.SelectedAmenities = model.SelectedAmenities ?? new List<int>();
                ModelState.AddModelError("", "Vui lòng điền đầy đủ thông tin các trường bắt buộc.");
                var errors = ModelState.Where(ms => ms.Value.Errors.Any())
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                               );

                return Json(new { success = false, errors });
            }
            ModelState.Remove("IdRoomNavigation");
            ModelState.Remove("IdUserNavigation");

            model.RoomDetail.TimePost = DateTime.Now;

            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "rooms");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                model.RoomDetail.Image = Path.Combine("/img", "rooms", uniqueFileName).Replace("\\", "/");
            }
            model.Room.IdUser = HttpContext.Session.GetInt32("UserId") ?? 0;
            var result = await _roomService.CreateRoomAsync(model);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi tạo bài đăng.");
                model.Amenities = (await _amenityRepository.GetAllAmenitiesAsync()).ToList();
                return View(model);
            }
        }
    }
}
