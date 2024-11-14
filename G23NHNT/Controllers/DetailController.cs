using G23NHNT.Models;
using G23NHNT.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class DetailController : ControllerBase
{
    private readonly IHouseRepository _houseRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IReviewRepository _reviewRepository;

    public DetailController(IHouseRepository houseRepository, IRoomRepository roomRepository, IReviewRepository reviewRepository)
    {
        _houseRepository = houseRepository;
        _roomRepository = roomRepository;
        _reviewRepository = reviewRepository;
    }

    [HttpGet("House/Detail/{id}")]
    public async Task<IActionResult> GetHouseDetail(int id)
    {
        var house = await _houseRepository.GetHouseWithDetailsAsync(id);
        if (house == null)
            return NotFound(new { message = "Không tìm thấy nhà trọ." });

        var reviews = await _reviewRepository.GetReviewsByHouseIdAsync(id);
        return Ok(new { House = house, Reviews = reviews });
    }

    [HttpGet("Room/Detail/{id}")]
    public async Task<IActionResult> GetRoomDetail(int id)
    {
        var room = await _roomRepository.GetRoomWithDetailsAsync(id);
        if (room == null)
            return NotFound(new { message = "Không tìm thấy phòng trọ." });

        var reviews = await _reviewRepository.GetReviewsByRoomIdAsync(id);
        return Ok(new { Room = room, Reviews = reviews });
    }
    
    [HttpPost("house/detail/{id}/addreview")]
    public async Task<IActionResult> AddHouseReview(int id, [FromBody] Review review)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return Unauthorized(new { message = "Hãy đăng nhập để được đánh giá." });

        ModelState.Remove("IdUserNavigation");

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { message = "Dữ liệu đánh giá không hợp lệ.", errors = errors });
        }

        review.IdHouse = id;
        review.IdUser = userId.Value;
        review.ReviewDate = DateTime.Now;

        try
        {
            await _reviewRepository.AddReviewAsync(review);
            return Ok(new { message = "Đánh giá đã được thêm thành công." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi máy chủ khi thêm đánh giá.", details = ex.Message });
        }
    }


    [HttpPost("room/detail/{id}/addreview")]
    public async Task<IActionResult> AddRoomReview(int id, [FromBody] Review review)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return Unauthorized(new { message = "Hãy đăng nhập để được đánh giá." });
        ModelState.Remove("IdUserNavigation");

        // Kiểm tra ModelState hợp lệ
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { message = "Dữ liệu đánh giá không hợp lệ.", errors });
        }

        review.IdRoom = id;
        review.IdUser = userId.Value;
        review.ReviewDate = DateTime.Now;

        try
        {
            await _reviewRepository.AddReviewAsync(review);
            return Ok(new { message = "Đánh giá đã được thêm thành công." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi máy chủ khi thêm đánh giá.", details = ex.Message });
        }
    }
}
