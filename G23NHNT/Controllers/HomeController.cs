// HomeController.cs
using G23NHNT.Models;
using G23NHNT.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace G23NHNT.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IRoomRepository _roomRepository;

        public HomeController(IHouseRepository houseRepository, IRoomRepository roomRepository)
        {
            _houseRepository = houseRepository;
            _roomRepository = roomRepository;
        }

        public async Task<IActionResult> Index()
        {
            var houses = await _houseRepository.GetAllHousesAsync();
            var rooms = await _roomRepository.GetAllRoomsAsync();

            var viewModel = new HomeViewModel
            {
                Houses = houses,
                Rooms = rooms,
                IsChuTro = User.IsInRole("ChuTro") 
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id, string type)
        {
            if (type == "house")
            {
                var house = await _houseRepository.GetHouseWithDetailsAsync(id);
                if (house != null)
                {
                    return View("HouseDetails", house);
                }
            }
            else if (type == "room")
            {
                var room = await _roomRepository.GetRoomWithDetailsAsync(id);
                if (room != null)
                {
                    return View("RoomDetails", room);
                }
            }
            return NotFound();
        }

    }
}
