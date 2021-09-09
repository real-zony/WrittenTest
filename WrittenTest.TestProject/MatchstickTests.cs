using Shouldly;
using Xunit;

namespace WrittenTest.TestProject
{
    public class MatchstickTests
    {
        [Fact]
        public void Matchstick_Initialize_Test()
        {
            var matchstickObj = new Matchstick<int>(100);
            matchstickObj.Identity.ShouldBe(100);
            matchstickObj.IsUsedStr.ShouldBe("未选");
        }
    }
}