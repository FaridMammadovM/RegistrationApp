using AddressBook.Api.Application.Commands.CarCommand.Delete;
using AddressBook.Api.Application.Commands.CarCommand.Insert;
using AddressBook.Api.Application.Commands.CarCommand.Update;
using AddressBook.Api.Application.Queries.CarQuery;
using AddressBook.Api.Application.Queries.CarQuery.GetAllData;
using AddressBook.Api.Application.Queries.CarQuery.GetById;
using AddressBook.Api.Infrastructure.Filters;
using AddressBook.Domain.Dtos.General;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CheckAccess]
    public class CarController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CarController> _logger;
        private readonly IMediator _mediator;

        public CarController(IConfiguration configuration, ILogger<CarController> logger, IMediator mediator)
        {
            _configuration = configuration;
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Inserts car into database.
        /// </summary>

        [HttpPost("InsertCar")]
        [Produces("application/json")]
        public async Task<IActionResult> InsertCar([FromBody] CarInsertCommand request)
        {
            try
            {

                var result = await _mediator.Send(request);
                if (result == null)
                { return BadRequest(new GeneralResponse() { Result = null, Message = "Tempname is not unique" }); }
                else if (result == 0)
                {
                    return Problem("Insert process is failed.");
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = result, Message = "Success" };
                    _logger.LogInformation("Inserted successfully car information into database with id : {0}", result);
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
        /// Updates car.
        /// </summary>

        [HttpPut("UpdateCar")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateCar([FromBody] CarUpdateCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return Problem("Update failed.");
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = null, Message = "Success" };
                    _logger.LogInformation("Updated information Department with request : {0}", request);
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
        /// Gets car by Id.
        /// </summary>

        [HttpGet("GetByIdCar")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdCar([FromQuery] int id)
        {
            try
            {
                var result = await _mediator.Send(new CarGetByIdQuery
                {
                    Id = id

                });
                if (result == null)
                {
                    return BadRequest(new GeneralResponse() { Result = null, Message = "There is no data for this id" });
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = result, Message = "Success" };
                    _logger.LogInformation("Got information from database with id : {0}", id);
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
        /// Gets all car.
        /// </summary>

        [HttpGet("GetAllCar")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllCarData()
        {
            try
            {
                var result = await _mediator.Send(new GetAllCarDataQuery
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


        [HttpDelete("DeleteCar")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteCar([FromQuery] int id)
        {
            try
            {
                var result = await _mediator.Send(new CarDeleteCommand
                {
                    Id = id

                });
                if (result == null)
                {
                    return BadRequest(new GeneralResponse() { Result = null, Message = "There is no data for this id" });
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = result, Message = "Success" };
                    _logger.LogInformation("Got information from database with id : {0}", id);
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
        /// Gets all Car.
        /// </summary>
        [HttpGet("GetAllCarName")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllCarName()
        {
            try
            {
                var result = await _mediator.Send(new CarAllQuery
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
    }
}
