using Sheenam.Api.Models.Foundations;
using System.Threading.Tasks;

namespace Sheenam.Api.Broker.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Guest> Insert(Guest guest);
    }
}
