using Xeptions;

namespace Sheenam.Api.Models.Foundations.Exceptions
{
    public class NullGuestException : Xeption
    {
        public NullGuestException() : base(message: "Guest is null") { }
    }
}
