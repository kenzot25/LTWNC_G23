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

        public HomeController(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var houses = await _houseRepository.GetAllHousesAsync();

            var viewModel = new HomeViewModel
            {
                Houses = houses,
                IsChuTro = User.IsInRole("ChuTro") 
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
           
                var house = await _houseRepository.GetHouseWithDetailsAsync(id);
                if (house != null)
                {
                    return View("HouseDetails", house);
                }
            return NotFound();
        }

    }
}
