using CustomerLibraryAPI.Common;
using System;
using Xunit;

namespace CustomerLibraryAPI.Tests.CommonTests
{
    public class NotFoundExceptionTests
    {
        [Fact]
        public void ShouldBeAbleToCreateException()
        {
            var exception = new NotFoundException();
            Assert.NotNull(exception);
        }

        [Fact]
        public void ShouldBeAbleToCreateExceptionWithMessage()
        {
            var exception = new NotFoundException("Message");
            Assert.NotNull(exception);
            Assert.Equal("Message", exception.Message);
        }

        [Fact]
        public void ShouldBeAbleToCreateExceptionWithMessageAndInnerException()
        {
            var innerException = new Exception();
            var exception = new NotFoundException("Message", innerException);
            Assert.NotNull(exception);
            Assert.Equal("Message", exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
