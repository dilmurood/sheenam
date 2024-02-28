using Sheenam.Api.Broker.Logging;
using Sheenam.Api.Broker.Storages;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Services.Foundations.Guests.Exceptions;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IStorageBroker _storageBroker;
        private readonly ILoggingBroker _loggingBroker;
        public GuestService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            _storageBroker = storageBroker; 
            _loggingBroker = loggingBroker;
        }
        public async ValueTask<Guest> AddGuestAsync(Guest guest)
        {
            try
            {
                if(guest is null)
                    throw new NullGuestException();
                
                return await _storageBroker.InsertGuestAsync(guest);
            }
            catch (NullGuestException nullGuestException)
            {
                var guestValidationException = new GuestValidationException(nullGuestException);
                this._loggingBroker.LogError(guestValidationException);

                throw guestValidationException;
            }

        }
    }
}
