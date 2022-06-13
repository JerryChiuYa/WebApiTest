using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiTest.Model;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[ServiceFilter(typeof(FixedToken))]  ,因已在MiddleWare中設定
    public class StockController : ControllerBase
    {
        private readonly StockServices _services;

        public StockController()
        {
            _services = new StockServices();
        }

        [HttpGet("GetStocks")]
        public ActionResult<List<Stocks>> GetStocks()
        {
            return _services.QueryAll();
        }

        [HttpGet("{Id}")]
        public ActionResult<Stocks> GetSingleStock(int Id)
        {
            var item= _services.SpecificStock(Id);
            if (item==null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public ActionResult CreateStock(Stocks stock)
        {
            _services.Create(stock);
            return Ok();
        }

        [HttpPut("{Id}")]
        public ActionResult<Stocks> UpdateStock(int Id, Stocks stock)
        {
            
            if (Id!=stock.Id)
            {
                return BadRequest();
            }
            _services.Update(stock);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public ActionResult DeleteStock(int Id)
        {
            _services.Delete(Id);
            return Ok();
        }
    }
}
