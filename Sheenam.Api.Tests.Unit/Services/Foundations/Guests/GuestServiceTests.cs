using FluentAssertions;
using Moq;
using Sheenam.Api.Broker.Logging;
using Sheenam.Api.Broker.Storages;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Services.Foundations.Guests;
using Tynamix.ObjectFiller;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;

        private readonly IGuestService guestService;

        public GuestServiceTests()
        {
            storageBrokerMock = new Mock<IStorageBroker>();
            loggingBrokerMock = new Mock<ILoggingBroker>();

            guestService = 
                new GuestService(storageBroker: storageBrokerMock.Object,
                loggingBroker: loggingBrokerMock.Object);
        }

        private static Guest CreateRandomGuest() =>
            CreateGuestFiller(date: GetRandomDateTimeOffset()).Create();


        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Guest> CreateGuestFiller(DateTimeOffset date)
        {
            var filler = new Filler<Guest>();

            filler.Setup().OnType<DateTimeOffset>().Use(date);

            return filler;
        }

        //[Fact]
        /*public async Task ShouldAddGuestAsync()
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
        }*/
    }
}
