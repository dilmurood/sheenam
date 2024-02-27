using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Services.Foundations.Guests.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsNullAndLogInAsync()
        {
            //given
            Guest nullGuest = null;
            var nullGuestException = new NullGuestException();

            var expectedGuestValidationException = 
                new GuestValidationException(nullGuestException);

            //when
            ValueTask<Guest> addGuestTask = this.guestService.AddGuestAsync(nullGuest);

            //then
            await Assert.ThrowsAsync<GuestValidationException> (() => addGuestTask.AsTask());
        }
    }
}
