using Moq;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsNullAndLogInAsync()
        {
            //given
            Guest? nullGuest = null;
            var nullGuestException = new NullGuestException();

            var expectedGuestValidationException =
                new GuestDependencyValidationException(nullGuestException);

            //when
            ValueTask<Guest> addGuestTask = this.guestService.AddGuestAsync(nullGuest);

            //then
            await Assert.ThrowsAsync<GuestDependencyValidationException>(() => addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker => 
                broker.InsertGuestAsync(It.IsAny<Guest>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsInvalidLogItAsync(string invalidText)
        {
            //given
            var invalidGuest = new Guest()
            {
                FirstName = invalidText
            };

            var invalidGuestException = new InvalidGuestException();

            invalidGuestException.AddData(
                key: nameof(Guest.Id),
                values: "Id is required");

            invalidGuestException.AddData(
                key: nameof(Guest.FirstName),
                values: "Text is required");

            invalidGuestException.AddData(
                key: nameof(Guest.LastName),
                values: "Id is required");

            invalidGuestException.AddData(
                key: nameof(Guest.DateOfBirth),
                values: "Date is required");


            invalidGuestException.AddData(
                key: nameof(Guest.Email),
                values: "Id is required");

            invalidGuestException.AddData(
                key: nameof(Guest.PhoneNumber),
                values: "Id is required");

            invalidGuestException.AddData(
                key: nameof(Guest.Address),
                values: "Id is required");

            var expectedGuestGuestValidationException = 
                new GuestDependencyValidationException(invalidGuestException);

            //when
            ValueTask<Guest> addGuestTask = guestService.AddGuestAsync(invalidGuest);

            //then
            await Assert.ThrowsAsync<GuestDependencyValidationException> (() =>
                addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker => broker.LogError(
                It.Is(SameExceptionAs(expectedGuestGuestValidationException))), Times.Once);

            storageBrokerMock.Verify(broker => 
                broker.InsertGuestAsync(It.IsAny<Guest>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        public async Task ShouldThrowValidationExceptionOnAddIfGenderIsInvalidAndLogInAsync()
        {
            //given
            var randomGuest = CreateRandomGuest();
            Guest invalidGuest = randomGuest;
            invalidGuest.Gender = GetInvalidEnum<GenderType>();
            var invalidGuestException = new InvalidGuestException();

            invalidGuestException.AddData(
                key: nameof(Guest.Gender),
                values: "Value is invalid");

            var expectedGuestValidationException = new GuestDependencyValidationException(invalidGuestException);

            //when 
            ValueTask<Guest> addGuestTask = this.guestService.AddGuestAsync(invalidGuest); 

            //then
            await Assert.ThrowsAsync<GuestDependencyValidationException>(() => addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationException))), Times.Once);

            storageBrokerMock.Verify(broker => 
            broker.InsertGuestAsync(It.IsAny<Guest>()), Times.Never());

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();

        }
    }
}
