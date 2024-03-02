using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Exceptions
{
    public class FailedGuestStorageException : Xeption
    {
        public FailedGuestStorageException(Exception innerException)
            : base("Failed guest storage error occurred, contact support", innerException)
        { }
    }
}
