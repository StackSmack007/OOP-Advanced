using GenericScale;
using Xunit;

namespace Testing
{
    public class Sharetests
    {

        [Fact]

        public void Scales_shouldReturnBiggerOfTwo()
        {
            //Arrange:
            Scale<string> trial1 = new Scale<string>("ab", "bb");

            //Act
            string biggerNumber = trial1.GetHeavier();

            //Assert
            Assert.Equal("bb", biggerNumber);

        }
    }
}