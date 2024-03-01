using Xeptions;

namespace Sheenam.Api.Models.Foundations.Exceptions
{
    public class GuestDependencyException : Xeption
    {
        public GuestDependencyException(Xeption innerException) 
            : base("Guest dependency error occurred, contact support", innerException) 
        { }
    }
}
