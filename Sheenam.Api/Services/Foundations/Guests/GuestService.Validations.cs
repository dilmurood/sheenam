using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Exceptions;

namespace Sheenam.Api.Services.Foundations.Guests
{
    public partial class GuestService
    {
        private void ValidateGuestNotNull(Guest guest)
        {
            if (guest is null)
                throw new NullGuestException();
        }
    }
}
