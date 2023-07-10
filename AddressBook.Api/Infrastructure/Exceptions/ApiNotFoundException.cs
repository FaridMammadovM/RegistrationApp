using System.Runtime.Serialization;

namespace AddressBook.Api.Infrastructure.Exceptions
{
    [Serializable]
    public class ApiNotFoundException : ApiException
    {
        public override int StatusCode => 404;

        public ApiNotFoundException() : base()
        {
        }

        public ApiNotFoundException(string message) : base(message)
        {
        }

        protected ApiNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
