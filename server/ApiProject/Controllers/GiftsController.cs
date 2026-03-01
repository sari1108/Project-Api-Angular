using ApiProject.DTO;
using ApiProject.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly IGiftsService _service;

        public GiftsController(IGiftsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDto.GiftModelDto>>> GetGifts()
        {
            var gifts = await _service.getAllGifts();
            return Ok(gifts);
        }
      //  [Authorize(Roles = "manager")]
        [HttpGet("sortByName")]
        public async Task<ActionResult<IEnumerable<GiftDto.GiftModelDto>>> SortByName([FromQuery]  string name)
        {
            var gifts = await _service.SortByName(name);
            return Ok(gifts);
        }
       // [Authorize(Roles = "manager")]

        [HttpGet("sortByBuyers")]
        public async Task<ActionResult<IEnumerable<GiftDto.GiftToManager>>> SortByBuyers()
        {
            var gifts = await _service.SortByBuyers();
            return Ok(gifts);
        }
       // [Authorize(Roles = "manager")]

        [HttpGet("sortByDonorName")]
        public async Task<ActionResult<IEnumerable<GiftDto.GiftModelDto>>> SortByDonorName([FromQuery]  string name)
        {
            var gifts = await _service.SortByDonorName(name);
            return Ok(gifts);
        }
        [HttpGet("OrderByPrice")]
        public async Task<ActionResult<IEnumerable<GiftDto.GiftModelDto>>> OrderByPrice()
        {
            var gifts = await _service.OrderByPrice();
            return Ok(gifts);
        }
        [HttpGet("OrderByCategory")]
        public async Task<ActionResult<IEnumerable<GiftDto.GiftModelDto>>> OrderByCategory([FromQuery]int id)
        {
            var gift = await _service.SortByCategory(id);
            if (!gift.Any())
                return NotFound(new { message = "קטגוריה לא נמצאה" });

            return Ok(gift);
        }





        [HttpGet("{id}")]
        public async Task<ActionResult<GiftDto.GiftModelDto>> GetGiftById(int id)
        {
            var gift = await _service.GetGiftById(id);
            if (gift == null)
                return NotFound(new { message = $"מתנה עם מזהה {id} לא נמצאה" });

            return Ok(gift);
        }
       // [Authorize(Roles = "manager")]

        [HttpPost]
        
        public async Task<ActionResult<string>> AddGift([FromBody] GiftDto.AddGiftDto gift)
        {
            // בדיקת ולידציה בסיסית
            if (gift == null)
                return BadRequest(new { message = "נתוני מתנה לא תקינים" });
            var result = await _service.AddGift(gift);
            if(result == null)
                return BadRequest(new { message = "נתוני מתנה לא תקינים או שהשם כבר קיים" });

            return Ok(new { message = result });
        }
       // [Authorize(Roles = "manager")]

        [HttpPut("{id}")]
        public async Task<ActionResult<GiftDto.GiftModelDto>> UpdateGift(int id, [FromBody] GiftDto.UpdateGiftDto gift)
        {
            // שליחת ה-id מה-URL לסרוויס כדי להבטיח עדכון של האובייקט הנכון
            var updatedGift = await _service.UpdateGift(gift, id);

            if (updatedGift == null)
                // return NotFound(new { message = "עדכון נכשל: המתנה או הקטגוריה לא נמצאו" });
                return Ok(null);

            return Ok(updatedGift);
        }
       // [Authorize(Roles = "manager")]

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<GiftDto.GiftModelDto>>> DeleteGift(int id)
        {
            var result = await _service.DeleteGift(id);

            if (result == null)
                return NotFound(new { message = "  מחיקה נכשלה: המתנה לא נמצאה או שכבר שולמה" });

            return Ok(result);
        }
    }
}