using CustomerLibraryAPI.WebApp.Models;
using Xunit;

namespace CustomerLibraryAPI.WebApp.Tests.ModelsTests
{
    public class ErrorModelTests
    {
        [Fact]
        public void ShouldBeAbleToCreateErrorModel()
        {
            var errorModel = new ErrorModel();
            errorModel.Title = "Error message";
            errorModel.Status = 500;

            Assert.NotNull(errorModel);
        }
    }
}
