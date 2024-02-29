using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Guest someGuest = CreateRandomGuest();
            SqlException sqlException = GetSqlError();
            var failedGuestStorageException = new FailedGuestStorageException(sqlException);

            GuestDependencyException expectedGuestDependencyException =
                new GuestDependencyException(failedGuestStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuestAsync(someGuest)).ThrowsAsync(sqlException);

            //when
            ValueTask<Guest> addGuestTask = this.guestService.AddGuestAsync(someGuest);

            //then
            await Assert.ThrowsAsync<GuestDependencyException>(() =>
                addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(someGuest), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedGuestDependencyException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
