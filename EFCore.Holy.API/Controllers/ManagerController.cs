using EFCore.Holy.Business;
using EFCore.Holy.Business.Handling;
using EFCore.Holy.Data.Interfaces;
using EFCore.Holy.Data.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Holy.API.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerBusiness _business;
        private readonly IHttpContextAccessor _contextAccessor;
        public ManagerController(
            IManagerRepository managerRepository,
            IChurchBusiness churchBusiness,
            IHttpContextAccessor contextAccessor)
        {
            _business = new ManagerBusiness(contextAccessor, managerRepository, churchBusiness);
        }


        [HttpGet("all")]
        [Authorize()]
        public IActionResult Get()
        {
            return Ok(_business.FindAll());
        }

        [HttpPost("register")]
        [Authorize()]
        public async Task<IActionResult> Create([FromBody] CreateManager createManager)
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

        [HttpPost("login")]
        [AllowAnonymous()]
        public IActionResult Login([FromBody] Login login)
        {
            try
            {
                var token = _business.Login(login);
                return StatusCode(200, token);
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _business.Delete(id);

                return StatusCode(200, Success.ManagerDeleted);
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
