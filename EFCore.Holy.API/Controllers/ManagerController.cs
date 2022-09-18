using EFCore.Holy.Business;
using EFCore.Holy.Business.Handling;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Holy.API.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerBusiness _business;
        public ManagerController(IManagerRepository managerRepository, IChurchBusiness churchBusiness)
        {
            _business = new ManagerBusiness(managerRepository, churchBusiness);
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            return Ok(_business.FindAll());
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateManager createManager)
        {
            try
            {
                await _business.Add(createManager);

                return StatusCode(201, Success.ManagerCreated);
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

        }
    }
}
