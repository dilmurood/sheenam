using Xeptions;

namespace Sheenam.Api.Models.Foundations.Exceptions
{
    public class GuestDependencyValidationException : Xeption
    {
        public GuestDependencyValidationException(Xeption innerException)
            : base("Guest dependency validation error occurred, fix errors and try again", innerException)
        { }
    }
}
