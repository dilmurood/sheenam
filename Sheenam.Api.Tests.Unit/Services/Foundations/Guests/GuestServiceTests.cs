using FluentAssertions;
using Moq;
using Sheenam.Api.Broker.Logging;
using Sheenam.Api.Broker.Storages;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Services.Foundations.Guests;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

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

        private static T GetInvalidEnum<T>()
        {
            int randomNumber = GetRundomNumber();
            while(Enum.IsDefined(typeof(T), randomNumber) is true)
            {
                randomNumber = GetRundomNumber();
            }

            return (T)(object)randomNumber;
        }

        private static int GetRundomNumber() =>
            new IntRange(2, 9).GetValue();

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException => actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }
        private static Filler<Guest> CreateGuestFiller(DateTimeOffset date)
        {
            var filler = new Filler<Guest>();

            filler.Setup().OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
