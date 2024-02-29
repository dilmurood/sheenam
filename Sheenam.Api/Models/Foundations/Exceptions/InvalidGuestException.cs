using Xeptions;

namespace Sheenam.Api.Models.Foundations.Exceptions
{
    public class InvalidGuestException : Xeption
    {
        public InvalidGuestException() 
            : base(message: "Guest is invalid") { }

    }
}
