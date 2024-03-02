using Xeptions;

namespace Sheenam.Api.Models.Foundations.Exceptions
{
    public class GuestServiceException : Xeption
    {
        public GuestServiceException(Xeption innerException) : base
            ("Guest service error occurred, contact support", innerException)
        {

        }
    }
}
