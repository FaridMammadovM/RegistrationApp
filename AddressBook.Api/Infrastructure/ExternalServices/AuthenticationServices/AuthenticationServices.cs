using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace AddressBook.Api.Infrastructure.ExternalServices.AuthenticationServices
{
    public class AuthenticationServices
    {
        private readonly string _connectionString = new ConfigurationBuilder().AddJsonFile($@"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json").Build().GetSection("ConnectionStrings")["AppCon"];

        public string GetUserInfo(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var decodedToken = tokenHandler.ReadJwtToken(token);

                var now = DateTime.UtcNow;
                if (decodedToken.ValidTo < now)
                {
                    return null;
                }

                var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);
                if (claims.Count != 11)
                {
                    return null;
                }

                var firstname = claims["Firstname"];
                var Surname = claims["Surname"];
                var Patronymic = claims["Patronymic"];
                var Userrole = claims["Userrole"];
                var Id = claims["Id"];
                var Username = claims["Username"];

                return JsonConvert.SerializeObject(Id);
            }
            catch (ArgumentException ex)
            {
                return null;
            }
        }

    }
}
