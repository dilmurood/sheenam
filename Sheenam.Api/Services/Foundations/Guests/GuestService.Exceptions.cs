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
        }
        private GuestValidationException CreateAndLogValidationException(Xeption xeption)
        {
            var guestValidationException = new GuestValidationException(xeption);
            this._loggingBroker.LogError(guestValidationException);

            return guestValidationException;
        }
    }
}
