using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations;
using System.Threading.Tasks;

namespace Sheenam.Api.Broker.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Guest> Guests { get; set; }

        public async ValueTask<Guest> InsertGuestAsync(Guest guest)
        {
            using var broker = new StorageBroker(this.configuration);

            EntityEntry<Guest> guestEntityEntry = await broker.Guests.AddAsync(guest);

            await broker.SaveChangesAsync(guest);

            return guestEntityEntry.Entity;
        }

    }
}
