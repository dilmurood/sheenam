<<<<<<< Updated upstream
﻿using Sheenam.Api.Broker.Logging;
using Sheenam.Api.Broker.Storages;
=======
﻿    using Sheenam.Api.Broker.Storages;
>>>>>>> Stashed changes
using Sheenam.Api.Models.Foundations;
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
        public ValueTask<Guest> AddGuestAsync(Guest guest)
        {
            return  _storageBroker.InsertGuestAsync(guest);
        }
    }
}
