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

            // Tạo view model để truyền dữ liệu vào view
            var viewModel = new HousePostViewModel
            {
                House = house,
                HouseDetail = house.HouseDetails.FirstOrDefault(),
                SelectedAmenities = house.IdAmenities.Select(a => a.IdAmenity).ToList(),
                Amenities = (await _amenityRepository.GetAllAmenitiesAsync()).ToList(),
                HouseTypes = (await _houseTypeRepository.GetAllHouseTypes()).Select(ht => new SelectListItem
                {
                    Value = ht.IdHouseType.ToString(),
                    Text = ht.Name
                }),
            };

            return View(viewModel);
        }


    }
}
