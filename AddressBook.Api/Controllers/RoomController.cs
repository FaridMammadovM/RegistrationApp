using AddressBook.Api.Application.Queries.RoomQuery;
using AddressBook.Api.Application.Queries.RoomQuery.GetByRoomIdWithEquipment;
using AddressBook.Api.Infrastructure.Filters;
using AddressBook.Domain.Dtos.General;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CheckAccess]

    public class RoomController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RoomController> _logger;
        private readonly IMediator _mediator;

        public RoomController(IConfiguration configuration, ILogger<RoomController> logger, IMediator mediator)
        {
            _configuration = configuration;
            _logger = logger;
            _mediator = mediator;
        }


        /// <summary>
        /// Gets all room.
        /// </summary>
        [HttpGet("GetAll")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new GetAllRoomQuery
                {

                });
                if (result == null)
                {
                    return NotFound(new GeneralResponse() { Result = null, Message = "There is no data." });
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = result, Message = "Success" };
                    _logger.LogInformation("Got information from database with request : {0}");
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Gets all room.
        /// </summary>
        [HttpGet("GetByRoomIdWithEquipment")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByRoomIdWithEquipment(GetByRoomIdWithEquipmentQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    return NotFound(new GeneralResponse() { Result = null, Message = "There is no data." });
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = result, Message = "Success" };
                    _logger.LogInformation("Got information from database with request : {0}");
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }


    }
}
