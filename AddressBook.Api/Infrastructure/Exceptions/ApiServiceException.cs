using System.Runtime.Serialization;

namespace AddressBook.Api.Infrastructure.Exceptions
{
    [Serializable]
    public class ApiServiceException : ApiException
    {
        public ApiServiceException() : base()
        {
        }

        public ApiServiceException(string message) : base(message)
        {
        }

        protected ApiServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
