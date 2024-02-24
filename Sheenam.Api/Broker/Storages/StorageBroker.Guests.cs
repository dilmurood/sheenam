using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations;

namespace Sheenam.Api.Broker.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Guest> Guests { get; set; }
    }
}
