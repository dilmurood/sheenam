using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Exceptions
{
    public class AlreadyExistGuestException : Xeption
    {
        public AlreadyExistGuestException(Exception innerException) : 
            base("Guest already exist", innerException) 
        { }
    }
}
