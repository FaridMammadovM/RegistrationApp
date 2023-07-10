using AddressBook.Api.Application.Commands.DepartmentCommand.Delete;
using AddressBook.Api.Application.Commands.DepartmentCommand.Insert;
using AddressBook.Api.Application.Commands.DepartmentCommand.Update;
using AddressBook.Api.Application.Queries.CarQuery;
using AddressBook.Api.Application.Queries.DepartmentQuery.DepartmentById;
using AddressBook.Api.Application.Queries.DepartmentQuery.DepartmentGetAll;
using AddressBook.Api.Application.Queries.PositionQuery;
using AddressBook.Domain.Dtos.General;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMediator _mediator;

        public DepartmentController(IConfiguration configuration, ILogger<DepartmentController> logger, IMediator mediator)
        {
            _configuration = configuration;
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Inserts department into database.
        /// </summary>

        [HttpPost("InsertDepartment")]
        [Produces("application/json")]
        public async Task<IActionResult> InsertDepartment([FromBody] DepartmentInsertCommand request)
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
                    _logger.LogInformation("Inserted successfully Department information into database with id : {0}", result);
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
        /// Updates department.
        /// </summary>

        [HttpPut("UpdateDepartment")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentUpdateCommand request)
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
        /// Gets department by Id.
        /// </summary>

        [HttpGet("GetByIdDepartment")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdDepartment([FromQuery] long id)
        {
            try
            {
                var result = await _mediator.Send(new GetByIdDepartmentQuery
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
        /// Gets all department by filter.
        /// </summary>

        [HttpGet("GetAllDepartment")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var result = await _mediator.Send(new GetAllDepartmentQuery
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


        [HttpDelete("DeleteDepartment")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteDepartment([FromQuery] long id)
        {
            try
            {
                var result = await _mediator.Send(new DepartmentDeleteCommand
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
        /// Gets all Position.
        /// </summary>
        [HttpGet("GetAllPosition")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllPosition()
        {
            try
            {
                var result = await _mediator.Send(new PositionAllQuery
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
        /// Gets all Car.
        /// </summary>
        [HttpGet("GetAllCar")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllCar()
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
