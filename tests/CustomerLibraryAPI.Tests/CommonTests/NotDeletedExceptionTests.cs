using CustomerLibraryAPI.Common;
using System;
using Xunit;

namespace CustomerLibraryAPI.Tests.CommonTests
{
    public class NotDeletedExceptionTests
    {
        [Fact]
        public void ShouldBeAbleToCreateException()
        {
            var exception = new NotDeletedException();
            Assert.NotNull(exception);
        }

        [Fact]
        public void ShouldBeAbleToCreateExceptionWithMessage()
        {
            var exception = new NotDeletedException("Message");
            Assert.NotNull(exception);
            Assert.Equal("Message", exception.Message);
        }

        [Fact]
        public void ShouldBeAbleToCreateExceptionWithMessageAndInnerException()
        {
            var innerException = new Exception();
            var exception = new NotDeletedException("Message", innerException);
            Assert.NotNull(exception);
            Assert.Equal("Message", exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
