using G23NHNT.Models;
using G23NHNT.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using G23NHNT.ViewModels;

namespace G23NHNT.Controllers
{
    public class QuanLiController : Controller
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IHouseDetailRepository _houseDetailRepository;
        private readonly IRoomDetailRepository _roomDetailRepository;
        private readonly IRoomRepository _roomRepository;
        


        public QuanLiController(IHouseRepository houseRepository, IRoomRepository roomRepository, IHouseDetailRepository houseDetailRepository, IRoomDetailRepository roomDetailRepository)
        {
            _houseRepository = houseRepository;
            _roomRepository = roomRepository;
            _houseDetailRepository = houseDetailRepository;
            _roomDetailRepository = roomDetailRepository;
        }

        public async Task<IActionResult> ListHouseRoom()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
                string userName = HttpContext.Session.GetString("UserName") ?? "";

                var houses = await _houseRepository.GetHousesByUserId(userId);
                var rooms = await _roomRepository.GetRoomsByUserId(userId);

                // Sử dụng HomeViewModel
                var viewModel = new HomeViewModel
                {
                    Houses = houses,
                    Rooms = rooms,
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
                SelectedAmenities = house.IdAmenities.Select(a => a.IdAmenity).ToList()
            };

            return View(viewModel);
        }

       
    }
}
