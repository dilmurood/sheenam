﻿using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Exceptions;

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

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            //given
            var someGuest = CreateRandomGuest();
            string message = GetRandomString();
            var duplicateKeyException = new DuplicateKeyException(message);

            var alreadyExistGuestException = new AlreadyExistGuestException(duplicateKeyException);

            var expectedGuestDependencyValidationException =
                new GuestDependencyValidationException(alreadyExistGuestException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuestAsync(someGuest)).ThrowsAsync(duplicateKeyException);

            //when
            ValueTask<Guest> addGuestTask = this.guestService.AddGuestAsync(someGuest);

            //then
            await Assert.ThrowsAsync<GuestDependencyValidationException>(() =>
                addGuestTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
               broker.InsertGuestAsync(someGuest), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedGuestDependencyValidationException))),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given
            Guest someGuest = CreateRandomGuest();
            var serviceException = new Exception();
            var failedGuestServiceException = new FailedGuestServiceException(serviceException);
            var expectedGuestServiceException = new GuestServiceException(failedGuestServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuestAsync(someGuest)).ThrowsAsync(serviceException);

            //when
            ValueTask<Guest> addGuestTask = this.guestService.AddGuestAsync(someGuest);

            //then
            await Assert.ThrowsAsync<GuestServiceException>(() =>
                addGuestTask.AsTask());

            storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(someGuest), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker
                .LogError(It.Is(SameExceptionAs(expectedGuestServiceException))), Times.Once);

            storageBrokerMock.VerifyNoOtherCalls();
            loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
