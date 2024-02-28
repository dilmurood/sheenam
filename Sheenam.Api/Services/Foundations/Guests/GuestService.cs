using Sheenam.Api.Broker.Logging;
using Sheenam.Api.Broker.Storages;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Exceptions;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public partial class GuestService : IGuestService
    {
        private readonly IStorageBroker _storageBroker;
        private readonly ILoggingBroker _loggingBroker;
        public GuestService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            _storageBroker = storageBroker; 
            _loggingBroker = loggingBroker;
        }
        public ValueTask<Guest> AddGuestAsync(Guest guest) => TryCatch(async () =>
        {
            ValidateGuestNotNull(guest);

            return await _storageBroker.InsertGuestAsync(guest);
        });
    }
}
