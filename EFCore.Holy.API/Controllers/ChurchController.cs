using EFCore.Holy.Business;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Holy.API.Controllers
{
    [Route("api/church")]
    [ApiController]
    public class ChurchController : ControllerBase
    {
        private IChurchBusiness _business;
        public ChurchController(IChurchRepository churchRepository)
        {
            _business = new ChurchBusiness(churchRepository);
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            return Ok(_business.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_business.FindById(id));
        }

        [HttpPost("create")]
        public IActionResult Post([FromBody] Church body)
        {
            return Ok(_business.Add(body));
        }
    }
}
