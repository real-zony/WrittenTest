using System;

namespace WrittenTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 初始化游戏，并打印默认情况。
            var game = new Game();
            game.InitializeGameResource();
            game.PrintCurrentStatus();

            // 初始化玩家。
            if (!game.AddPlayer(new[]
            {
                new Player(1),
                new Player(2)
            }))
            {
                Console.WriteLine("玩家初始化失败，退出游戏。");
                Environment.Exit(-1);
            }

            game.Start();
        }
    }
}