using AddressBook.Api.Application.Commands.EmployeeCommand.Delete;
using AddressBook.Api.Application.Commands.EmployeeCommand.Insert;
using AddressBook.Api.Application.Commands.EmployeeCommand.Update;
using AddressBook.Api.Application.Queries.EmployeeeQuery.Driver;
using AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeAll;
using AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeById;
using AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeGetAllFilter;
using AddressBook.Api.Application.Queries.EmployeeeQuery.Login;
using AddressBook.Api.Infrastructure.Filters;
using AddressBook.Domain.Dtos.General;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMediator _mediator;


        public EmployeeController(IConfiguration configuration, ILogger<EmployeeController> logger, IMediator mediator)
        {
            _configuration = configuration;
            _logger = logger;
            _mediator = mediator;
        }


        /// <summary>
        /// Inserts employee into database.
        /// </summary>
        /// <returns></returns>

        [HttpPost("InsertEmployee")]
        [Produces("application/json")]
        [CheckAccess]
        public async Task<IActionResult> InsertEmployee([FromBody] InsertEmployeeCommand request)
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
                else if (result == 1)
                {
                    return Problem("Sıra nömrəsi düzgün qeyd edilməyib.");
                }
                else if (result == 2)
                {
                    return Ok("Qeyd edilmiş istifadəçi adı mövcuddur.");
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = result, Message = "Success" };
                    _logger.LogInformation("Inserted successfully employee information into database with id : {0}", result);
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
        /// Updates employee.
        /// </summary>

        [HttpPut("UpdateEmployee")]
        [Produces("application/json")]
        [CheckAccess]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return Problem("Update failed.");
                }
                else if (result == 1)
                {
                    return Problem("Sıra nömrəsi düzgün qeyd edilməyib.");
                }
                else if (result == 2)
                {
                    return Ok("Qeyd edilmiş istifadəçi adı mövcuddur.");
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = null, Message = "Success" };
                    _logger.LogInformation("Updated information employee with request : {0}", request);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }


        [HttpDelete("DeleteEmployee")]
        [Produces("application/json")]
        [CheckAccess]
        public async Task<IActionResult> DeleteEmployee([FromQuery] int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteEmployeeCommand
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
        /// Gets employee by Id.
        /// </summary>
        [HttpGet("GetByIdEmployee")]
        [Produces("application/json")]
        [CheckAccess]
        public async Task<IActionResult> GetByIdEmployee([FromQuery] int id)
        {
            try
            {

                var result = await _mediator.Send(new GetByIdEmployeeQuery
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
        /// Gets all employee by filter.
        /// </summary>

        [HttpGet("GetAllByFilter")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllByFilter([FromQuery] GetEmployeeFilterQuery request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result == null)
                {
                    return NotFound(new GeneralResponse() { Result = null, Message = "There is no data." });
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = result, Message = "Success" };
                    _logger.LogInformation("Got information from database with request : {0}", request);
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
        /// Gets all employee.
        /// </summary>
        [HttpGet("GetAll")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new AllEmployeeQuery
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
        /// Gets all employee.
        /// </summary>
        [HttpGet("GetAllDriver")]
        [Produces("application/json")]
        [CheckAccess]
        public async Task<IActionResult> GetAllDriver()
        {
            try
            {
                var result = await _mediator.Send(new DriverQuery
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
        /// login employee.
        /// </summary>

        [HttpGet("Login")]
        [Produces("application/json")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {

                string Key = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("Jwt")["Key"];
                string Audience = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("Jwt")["Audience"];
                string Issuer = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("Jwt")["Issuer"];

                if (Key == null)
                {
                    Key = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("Jwt")["Key"];
                    Audience = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("Jwt")["Audience"];
                    Issuer = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("Jwt")["Issuer"];
                }
                var loginQuery = new LoginQuery
                {
                    Username = username,
                    Password = password,
                };

                loginQuery.SetJwtConfiguration(Key, Audience, Issuer);

                var result = await _mediator.Send(loginQuery);
                if (result.Token == null)
                { return BadRequest(new GeneralResponse() { Result = null, Message = "İstifadəçi adı və ya parol yanlışdır." }); }

                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = result, Message = "Success" };
                    _logger.LogInformation("Inserted successfully employee information into database with id : {0}", result);
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
