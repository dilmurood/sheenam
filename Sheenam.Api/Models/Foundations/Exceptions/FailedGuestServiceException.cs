using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Exceptions
{
    public class FailedGuestServiceException : Xeption
    {
        public FailedGuestServiceException(Exception innerException) : 
            base ("Failed guest service exception occurred", innerException)
        {
            
        }
    }
}
