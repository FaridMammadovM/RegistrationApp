using AddressBook.Api.Application.Commands.OrderCommand.Confirm;
using AddressBook.Api.Application.Commands.OrderCommand.ConfirmRoom;
using AddressBook.Api.Application.Commands.OrderCommand.Insert;
using AddressBook.Api.Application.Commands.OrderCommand.InsertRoom;
using AddressBook.Api.Application.Commands.OrderCommand.Update;
using AddressBook.Api.Application.Commands.OrderCommand.UpdateRoom;
using AddressBook.Api.Application.Queries.OrderQuery.GetAllFilter;
using AddressBook.Api.Application.Queries.OrderQuery.GetById;
using AddressBook.Api.Application.Queries.OrderQuery.RoomGetAllByFilter;
using AddressBook.Api.Application.Queries.OrderQuery.RoomGetById;
using AddressBook.Api.Infrastructure.Filters;
using AddressBook.Domain.Dtos.General;
using AddressBook.Domain.Dtos.Order;
using AddressBook.Domain.Dtos.Order.Room;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CheckAccess]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(IConfiguration configuration, ILogger<OrderController> logger, IMediator mediator)
        {
            _configuration = configuration;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("OrderInsert")]
        [Produces("application/json")]
        public async Task<IActionResult> OrderInsert([FromBody] OrderInsertDto model)
        {
            try
            {
                string mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["MailFrom"];
                string password = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["Password"];
                if (mailFrom == null)
                {
                    mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["MailFrom"];
                    password = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["Password"];
                }
                string token = HttpContext.Request.Headers["Authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);


                var Id = int.Parse(claims["Id"]);
                var Username = claims["Username"];
                var Userrole = claims["Userrole"];

                OrderInsertCommand request = new OrderInsertCommand();
                request.EmployeeId = Id;
                request.PassengerType = model.PassengerType;
                request.EmployeeUsingId = model.EmployeeUsingId;
                request.DepartureType = model.DepartureType;
                request.DepartureTimeDay = model.DepartureTimeDay;
                request.ReturnTimeDay = model.ReturnTimeDay;
                request.DepartureTimeHours = model.DepartureTimeHours;
                request.ReturnTimeHours = model.ReturnTimeHours;
                request.Direction = model.Direction;
                request.PassengerCount = model.PassengerCount;
                request.Luggage = model.Luggage;
                request.LuggageSize = model.LuggageSize;
                request.Address = model.Address;
                request.Note = model.Note;
                request.InsertUser = Username;
                request.MailFrom = mailFrom;
                request.Password = password;

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
                    _logger.LogInformation("Inserted successfully order information into database with id : {0}", result);
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
        /// Gets order by Id.
        /// </summary>
        [HttpGet("GetByIdOrder")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdOrder([FromQuery] long id)
        {
            try
            {
                var result = await _mediator.Send(new OrderGetByIdQuery
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

        [HttpPut("UpdateOrder")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDto model)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                var Id = int.Parse(claims["Id"]);
                var Username = claims["Username"];
                var Userrole = claims["Userrole"];

                UpdateOrderCommand request = new UpdateOrderCommand();
                request.Id = model.Id;
                request.EmployeeId = Id;
                request.PassengerType = model.PassengerType;
                request.EmployeeUsingId = model.EmployeeUsingId;
                request.DepartureType = model.DepartureType;
                request.DepartureTimeDay = model.DepartureTimeDay;
                request.ReturnTimeDay = model.ReturnTimeDay;
                request.DepartureTimeHours = model.DepartureTimeHours;
                request.ReturnTimeHours = model.ReturnTimeHours;
                request.Direction = model.Direction;
                request.PassengerCount = model.PassengerCount;
                request.Luggage = model.Luggage;
                request.LuggageSize = model.LuggageSize;
                request.Address = model.Address;
                request.Note = model.Note;
                request.UpdateUser = Username;

                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return Problem("Update failed.");
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = null, Message = "Success" };
                    _logger.LogInformation("Updated information order with request : {0}", request);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }


        [HttpPut("ConfirmOrder")]
        [Produces("application/json")]
        public async Task<IActionResult> ConfirmOrder([FromBody] OrderConfirmDto model)
        {
            try
            {
                string mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["MailFrom"];
                string password = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["Password"];
                if (mailFrom == null)
                {
                    mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["MailFrom"];
                    password = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["Password"];
                }

                string token = HttpContext.Request.Headers["Authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                var Id = int.Parse(claims["Id"]);
                var Username = claims["Username"];
                var Userrole = claims["Userrole"];

                if (!Userrole.Split(", ").Contains("Admin") && !Userrole.Split(", ").Contains("CarAdmin"))
                {
                    return Problem("Order doesn't have Admin role. Confirm failed.");
                }

                ConfirmOrderCommand request = new ConfirmOrderCommand();
                request.Id = model.Id;
                request.CarId = model.CarId;
                request.DriverId = model.DriverId;
                request.RejectionReason = model.RejectionReason;
                request.Status = model.Status;
                request.MailFrom = mailFrom;
                request.Password = password;
                request.UpdateUser = Username;

                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return Problem("Order Confirm failed.");
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = null, Message = "Success" };
                    _logger.LogInformation("Confirm information order with request : {0}", request);
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
        /// Gets all order filter.
        /// </summary>

        [HttpGet("GetAllByFilter")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllByFilter([FromQuery] GetAllByFilterOrderQuery request)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                var Id = int.Parse(claims["Id"]);
                var Userrole = claims["Userrole"];
                request.Id = Id;
                request.Userrole = Userrole;
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


        [HttpPost("OrderRoomInsert")]
        [Produces("application/json")]
        public async Task<IActionResult> OrderRoomInsert([FromBody] OrderRoomInsertDto model)
        {
            try
            {
                string mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["MailFrom"];
                string password = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["Password"];
                if (mailFrom == null)
                {
                    mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["MailFrom"];
                    password = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["Password"];
                }
                string token = HttpContext.Request.Headers["Authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);


                var Id = int.Parse(claims["Id"]);
                var Username = claims["Username"];
                var Userrole = claims["Userrole"];

                InsertOrderRoomCommand request = new InsertOrderRoomCommand();
                request.InsertBy = Id;
                request.InsertUser = Username;
                request.MailFrom = mailFrom;
                request.Password = password;
                request.Time = model.Time;
                request.StartHours = model.StartHours;
                request.EndHours = model.EndHours;
                request.MeetName = model.MeetName;
                request.ParticipantCount = model.ParticipantCount;
                request.RoomId = model.RoomId;
                request.Note = model.Note;

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
                    _logger.LogInformation("Inserted successfully order information into database with id : {0}", result);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }


        [HttpPut("UpdateRoomOrder")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateRoomOrder([FromBody] OrderRoomUpdateDto model)
        {
            try
            {
                string mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["MailFrom"];
                string password = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["Password"];
                if (mailFrom == null)
                {
                    mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["MailFrom"];
                    password = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["Password"];
                }
                string token = HttpContext.Request.Headers["Authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);


                var Id = int.Parse(claims["Id"]);
                var Username = claims["Username"];
                var Userrole = claims["Userrole"];

                UpdateOrderRoomCommand request = new UpdateOrderRoomCommand();
                request.UpdateBy = Id;
                request.InsertUser = Username;
                request.MailFrom = mailFrom;
                request.Password = password;
                request.Id = model.Id;
                request.Time = model.Time;
                request.StartHours = model.StartHours;
                request.EndHours = model.EndHours;
                request.MeetName = model.MeetName;
                request.ParticipantCount = model.ParticipantCount;
                request.RoomId = model.RoomId;
                request.Note = model.Note;

                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return Problem("Update failed.");
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = null, Message = "Success" };
                    _logger.LogInformation("Updated information order with request : {0}", request);
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
        /// Gets order by Id.
        /// </summary>
        [HttpGet("GetByIdRoomOrder")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdRoomOrder([FromQuery] int id)
        {
            try
            {
                var result = await _mediator.Send(new RoomOrderGetByIdQuery
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

        [HttpPut("ConfirmRoomOrder")]
        [Produces("application/json")]
        public async Task<IActionResult> ConfirmRoomOrder([FromBody] OrderRoomConfirmDto model)
        {
            try
            {
                string mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["MailFrom"];
                string password = new ConfigurationBuilder().AddJsonFile("appsettings.Dev.json").Build().GetSection("SendMail")["Password"];
                if (mailFrom == null)
                {
                    mailFrom = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["MailFrom"];
                    password = new ConfigurationBuilder().AddJsonFile("appsettings.Prod.json").Build().GetSection("SendMail")["Password"];
                }

                string token = HttpContext.Request.Headers["Authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                var Id = int.Parse(claims["Id"]);
                var Username = claims["Username"];
                var Userrole = claims["Userrole"];

                if (!Userrole.Split(", ").Contains("Admin") && !Userrole.Split(", ").Contains("RoomAdmin"))
                {
                    return Problem("Order doesn't have Admin role. Confirm failed.");
                }

                RoomConfirmOrderCommand request = new RoomConfirmOrderCommand();
                request.Id = model.Id;
                request.RejectReason = model.RejectionReason;
                request.Status = model.Status;
                request.MailFrom = mailFrom;
                request.Password = password;
                request.UpdateBy = Id;

                var result = await _mediator.Send(request);
                if (result == 0)
                {
                    return Problem("Order Confirm failed.");
                }
                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = null, Message = "Success" };
                    _logger.LogInformation("Confirm information order with request : {0}", request);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPost("GetAllRoomByFilter")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllRoomByFilter([FromBody] GetAllByFilterRoomOrderQuery request)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                var Id = int.Parse(claims["Id"]);
                var Userrole = claims["Userrole"];
                request.Id = Id;
                request.Userrole = Userrole;
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


        [HttpGet("CheckToken")]
        [Produces("application/json")]
        public async Task<IActionResult> CheckToken()
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                var İd = int.Parse(claims["Id"]);
                var Username = claims["Username"];
                var Userrole = claims["Userrole"];

                string result = "token is true";


                if (result == null)
                { return BadRequest(new GeneralResponse() { Result = null, Message = "False" }); }

                else
                {
                    GeneralResponse response = new GeneralResponse() { Result = result, Message = "Success" };
                    _logger.LogInformation("True", result);
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
