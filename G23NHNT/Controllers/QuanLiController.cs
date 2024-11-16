using G23NHNT.Models;
using G23NHNT.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using G23NHNT.ViewModels;
using G23NHNT.Repository.House;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace G23NHNT.Controllers
{
    public class QuanLiController : Controller
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IHouseDetailRepository _houseDetailRepository;
        private readonly IHouseTypeRepository _houseTypeRepository;
        private readonly IAmenityRepository _amenityRepository;


        public QuanLiController(IHouseRepository houseRepository, IHouseDetailRepository houseDetailRepository,
            IHouseTypeRepository houseTypeRepository, IAmenityRepository amenityRepository)
        {
            _houseRepository = houseRepository;
            _houseDetailRepository = houseDetailRepository;
            _houseTypeRepository = houseTypeRepository;
            _amenityRepository = amenityRepository;
        }

        public async Task<IActionResult> ListHouseRoom()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                string userName = HttpContext.Session.GetString("UserName") ?? "";

                var houses = await _houseRepository.GetHousesByUserId(userId);

                // Sử dụng HomeViewModel
                var viewModel = new HomeViewModel
                {
                    Houses = houses,
                    IsChuTro = true
                };

                ViewBag.UserId = userId;
                ViewBag.UserName = userName;

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditHouse(int id)
        {
            var house = await _houseRepository.GetHouseWithDetailsAsync(id);
            if (house == null)
            {
                return NotFound();
            }

            var viewModel = new HousePostViewModel
            {
                House = house,
                HouseDetail = house.HouseDetails.FirstOrDefault(),
                SelectedAmenities = house.IdAmenities.Select(a => a.IdAmenity).ToList(),
                Amenities = (await _amenityRepository.GetAllAmenitiesAsync()).ToList(),
                SelectedHouseType = house.HouseTypeId, // Lấy loại nhà hiện tại
                HouseTypes = (await _houseTypeRepository.GetAllHouseTypes())
                             .Select(ht => new SelectListItem
                             {
                                 Value = ht.IdHouseType.ToString(),
                                 Text = ht.Name,
                                 Selected = ht.IdHouseType == house.HouseTypeId
                             }).ToList()
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditHouse(HousePostViewModel viewModel, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Amenities = (await _amenityRepository.GetAllAmenitiesAsync()).ToList();
                viewModel.HouseTypes = (await _houseTypeRepository.GetAllHouseTypes())
                                       .Select(ht => new SelectListItem
                                       {
                                           Value = ht.IdHouseType.ToString(),
                                           Text = ht.Name
                                       }).ToList();
                return View(viewModel);
            }

            var existingHouse = await _houseRepository.GetHouseWithDetailsAsync(viewModel.House.IdHouse);
            if (existingHouse == null)
            {
                return NotFound();
            }

            var existingHouseDetail = existingHouse.HouseDetails.FirstOrDefault();
            if (existingHouseDetail == null)
            {
                return NotFound();
            }

            // Cập nhật các trường của House
            existingHouse.NameHouse = !string.IsNullOrEmpty(viewModel.House.NameHouse) ? viewModel.House.NameHouse : existingHouse.NameHouse;
            // **Cập nhật HouseTypeId**
            existingHouse.HouseTypeId = viewModel.SelectedHouseType;

            // Cập nhật các trường của HouseDetail
            existingHouseDetail.Address = !string.IsNullOrEmpty(viewModel.HouseDetail.Address) ? viewModel.HouseDetail.Address : existingHouseDetail.Address;
            existingHouseDetail.Price = viewModel.HouseDetail.Price != 0 ? viewModel.HouseDetail.Price : existingHouseDetail.Price;
            existingHouseDetail.DienTich = viewModel.HouseDetail.DienTich != 0 ? viewModel.HouseDetail.DienTich : existingHouseDetail.DienTich;
            existingHouseDetail.TienDien = !string.IsNullOrEmpty(viewModel.HouseDetail.TienDien) ? viewModel.HouseDetail.TienDien : existingHouseDetail.TienDien;
            existingHouseDetail.TienNuoc = !string.IsNullOrEmpty(viewModel.HouseDetail.TienNuoc) ? viewModel.HouseDetail.TienNuoc : existingHouseDetail.TienNuoc;
            existingHouseDetail.TienDv = !string.IsNullOrEmpty(viewModel.HouseDetail.TienDv) ? viewModel.HouseDetail.TienDv : existingHouseDetail.TienDv;
            existingHouseDetail.Describe = !string.IsNullOrEmpty(viewModel.HouseDetail.Describe) ? viewModel.HouseDetail.Describe : existingHouseDetail.Describe;
            existingHouseDetail.Status = !string.IsNullOrEmpty(viewModel.HouseDetail.Status) ? viewModel.HouseDetail.Status : existingHouseDetail.Status;

            // Xử lý hình ảnh nếu có upload mới
            if (imageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/houses/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingHouseDetail.Image = "/img/houses/" + fileName;
            }

            // Cập nhật vào database
            await _houseRepository.UpdateAsync(existingHouse);
            await _houseDetailRepository.UpdateAsync(existingHouseDetail);

            return RedirectToAction("ListHouseRoom");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHouse(int id)
        {
            try
            {
                var house = await _houseRepository.GetHouseWithDetailsAsync(id);
                if (house == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy nhà trọ!" });
                }

                await _houseRepository.DeleteAsync(id);

                return Json(new { success = true, message = "Xóa nhà trọ và các liên kết liên quan thành công!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting house: {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi khi xóa nhà trọ!" });
            }
        }
    }
}
