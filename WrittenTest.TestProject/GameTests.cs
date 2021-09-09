using System.Linq;
using Shouldly;
using Xunit;

namespace WrittenTest.TestProject
{
    public class GameTests
    {
        private readonly Game _game;

        public GameTests()
        {
            _game = new Game();
        }

        [Fact]
        public void InitializeGame_Test()
        {
            _game.InitializeGameResource();
            _game.Data.ShouldNotBeNull();
            _game.Data.Count.ShouldBe(3);

            foreach (var row in _game.Data)
            {
                row.Count.ShouldBeGreaterThan(1);
            }
        }

        [Fact]
        public void Pick_Test()
        {
            _game.InitializeGameResource();
            _game.Pick(3, 50).ShouldBeFalse();
            _game.Pick(2, 50).ShouldBeFalse();

            _game.Pick(2, 1).ShouldBeTrue();
            _game.Data[2].Count(d => !d.IsUsed).ShouldBe(6);

            _game.Pick(0, 3);
            _game.Pick(0, 1).ShouldBeFalse();
        }

        [Fact]
        public void AddPlayer_Test()
        {
            _game.InitializeGameResource();
            _game.AddPlayer(new[] { new Player(1), new Player(2) }).ShouldBeTrue();

            _game.AddPlayer(new[] { new Player(1) }).ShouldBeFalse();
        }

        [Fact]
        public void PrintCurrentStatus_Test()
        {
            _game.InitializeGameResource();
            _game.PrintCurrentStatus();
        }

        [Fact]
        public void CheckIsOver_Test()
        {
            _game.InitializeGameResource();
            _game.CheckIsOver().ShouldBeFalse();

            _game.Pick(0, 3);
            _game.Pick(1, 5);
            _game.Pick(2, 7);
            _game.CheckIsOver().ShouldBeTrue();
        }
    }
}