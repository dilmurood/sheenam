using System;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Guests.Exceptions
{
    public class NullGuestException : Xeption
    {
        public NullGuestException() : base(message: "Guest is null"){}
    }
}
