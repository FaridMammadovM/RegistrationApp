using Newtonsoft.Json;

namespace AddressBook.Api.Models.AuthenticationServicesModels
{
    public class GetUserInfoResponse
    {
        public string sub { get; set; }
        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public List<string> role { get; set; }
        public string preferred_username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
    }
}
