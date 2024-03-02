using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Exceptions;
using System;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private delegate ValueTask<Guest> ReturningGuestFunction();
        private async ValueTask<Guest> TryCatch(ReturningGuestFunction returningGuestFunction)
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
                var failedGuestStorageException = new FailedGuestStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedGuestStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistGuestException = 
                    new AlreadyExistGuestException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistGuestException);
            }
            catch (Exception ex) 
            {
                var failedGuestServiceException = new FailedGuestServiceException(ex);

                throw CreateAndLogServiceException(failedGuestServiceException);
            }
        }

        private GuestDependencyValidationException CreateAndLogValidationException(Xeption xeption)
        {
            var guestDependencyValidationException = new GuestDependencyValidationException(xeption);
            _loggingBroker.LogError(guestDependencyValidationException);

            return guestDependencyValidationException;
        }
        private GuestDependencyException CreateAndLogCriticalDependencyException(Xeption xeption)
        {
            var guestDependencyException = new GuestDependencyException(xeption);
            _loggingBroker.LogCritical(guestDependencyException);

            return guestDependencyException;
        }
        private GuestDependencyValidationException CreateAndLogDependencyValidationException(Xeption xeption)
        {
            GuestDependencyValidationException guestValidationException = new GuestDependencyValidationException(xeption);
            _loggingBroker.LogError(guestValidationException);

            return guestValidationException;
        }
        private GuestServiceException CreateAndLogServiceException(Xeption xeption)
        {
            var guestServiceException = new GuestServiceException(xeption);
            _loggingBroker.LogError(guestServiceException);

            return guestServiceException;
        }

    }
}
