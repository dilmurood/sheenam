using Sheenam.Api.Broker.Storages;
using Sheenam.Api.Models.Foundations;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public class GuestService : IGuestService
    {
        private readonly IStorageBroker _storageBroker;
        public GuestService(IStorageBroker storageBroker) =>
            this._storageBroker = storageBroker;
        public ValueTask<Guest> AddGuestAsync(Guest guest)
        {
            return  _storageBroker.InsertGuestAsync(guest);
        }
    }
}
