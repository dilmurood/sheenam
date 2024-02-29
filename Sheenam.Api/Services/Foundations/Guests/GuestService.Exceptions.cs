using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Exceptions;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private delegate ValueTask<Guest> returningGuestFunction();
        private async ValueTask<Guest> TryCatch(returningGuestFunction returningGuestFunction)
        {
            try
            {
                return await returningGuestFunction();
            }
            catch (NullGuestException nullGuestException)
            {
                throw CreateAndLogValidationException(nullGuestException);
            }
            catch (InvalidGuestException invalidGuestException)
            {
                throw CreateAndLogValidationException(invalidGuestException);
            }
            catch (SqlException sqlException)
            {
                var failedGuestStorageExcpetion = new FailedGuestStorageException(sqlException);
                throw CreateAndLogCriticalDependecyException(failedGuestStorageExcpetion);
            }
        }
        private GuestValidationException CreateAndLogValidationException(Xeption xeption)
        {
            var guestValidationException = new GuestValidationException(xeption);
            _loggingBroker.LogError(guestValidationException);

            return guestValidationException;
        }

        private GuestDependencyException CreateAndLogCriticalDependecyException(Xeption xeption)
        {
            var guestDependencyException = new GuestDependencyException(xeption);
            this._loggingBroker.LogCritical(guestDependencyException);

            return guestDependencyException;
        }
    }
}
