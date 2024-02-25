using FluentAssertions;
using Moq;
using Sheenam.Api.Broker.Storages;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Services.Foundations.Guests;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public class GuestServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;

        private readonly IGuestService guestService;

        public GuestServiceTests()
        {
            storageBrokerMock = new Mock<IStorageBroker>();

            guestService = 
                new GuestService(storageBroker: storageBrokerMock.Object);
        }

        [Fact]
        public async Task ShouldAddGuestAsync()
        {
            //Arrange
            Guest randomGuest = new Guest()
            {
                FirstName = "John",
                LastName = "Shelby",
                Address = "Birmingem",
                DateOfBirth = new DateTimeOffset(),
                Email = "JouhnShelby@gmail.com",
                Gender = GenderType.male,
                PhoneNumber = "1234567890"
            };

            this.storageBrokerMock.Setup(broker => broker.InsertGuestAsync(randomGuest))
                .ReturnsAsync(randomGuest);

            //Act
            Guest actual = await this.guestService.AddGuestAsync(randomGuest);

            //Assert
            actual.Should().BeEquivalentTo(randomGuest);
        }
    }
}
