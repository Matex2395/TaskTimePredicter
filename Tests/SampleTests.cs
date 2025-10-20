using Xunit;

namespace TaskTimePredicter.Tests
{
    public class SampleTests
    {
        [Fact]
        public void SimpleAdditionTest()
        {
            int a = 1;
            int b = 1;
            int result = a + b;

            Xunit.Assert.Equal(2, result);
        }
    }
}