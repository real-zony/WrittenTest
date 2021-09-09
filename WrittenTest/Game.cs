using System;
using System.Collections.Generic;
using System.Linq;

namespace WrittenTest
{
    public class Game
    {
        public List<List<Matchstick<int>>> Data;

        public List<Player> Players = new List<Player>();

        protected Player CurrentPlayer;
        protected int CurrentPlayerIndex;

        /// <summary>
        /// 根据参数，拿取指定行、指定数量的火柴。
        /// </summary>
        /// <param name="rowIndex">需要拿取火柴的行数。</param>
        /// <param name="number">需要拿取的火柴数量。</param>
        /// <returns>拿取成功返回 True，失败返回 False。</returns>
        public bool Pick(int rowIndex, int number)
        {
            if (rowIndex is >= 3 or < 0)
            {
                Console.WriteLine("选择的行不存在，请重新选择。");
                return false;
            }

            var rowObj = Data[rowIndex];
            var availableNumber = rowObj.Count(r => !r.IsUsed);

            if (availableNumber <= 0)
            {
                Console.WriteLine("当前选择的行，已经没有可用火柴，请重新选择。");
                return false;
            }

            if (availableNumber < number)
            {
                Console.WriteLine($"当前选择的行，可用火柴不足(余 {availableNumber} 根，想要拿取 {number} 根。)，请重新选择。");
                return false;
            }

            foreach (var matchstick in rowObj.Where(r => !r.IsUsed).Take(number))
            {
                matchstick.IsUsed = true;
            }

            return true;
        }

        /// <summary>
        /// 初始化火柴堆。
        /// </summary>
        public void InitializeGameResource()
        {
            Data = new List<List<Matchstick<int>>>
            {
                new List<Matchstick<int>>
                {
                    new(1), new(2), new(3)
                },
                new List<Matchstick<int>>
                {
                    new(4), new(5), new(6), new(7), new(8)
                },
                new List<Matchstick<int>>
                {
                    new(9), new(10), new(11), new(12), new(13), new(14), new(15)
                }
            };
        }

        /// <summary>
        /// 打印当前的火柴堆状态。
        /// </summary>
        public void PrintCurrentStatus()
        {
            Console.WriteLine("当前火柴堆的状态:");

            for (var rowIndex = 0; rowIndex < Data.Count; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < Data[rowIndex].Count; columnIndex++)
                {
                    var currentMatchstick = Data[rowIndex][columnIndex];
                    Console.Write($"火柴: {currentMatchstick.Identity}({currentMatchstick.IsUsedStr})  ");
                }

                Console.Write('\n');
                Console.WriteLine("----------------------------------------");
            }
        }

        /// <summary>
        /// 向本局游戏添加新的玩家。
        /// </summary>
        /// <param name="players">需要添加的玩家集合。</param>
        public bool AddPlayer(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                if (Players.Any(p => p.Number == player.Number))
                {
                    Console.WriteLine($"本场游戏已经存在了编号为 {player.Number} 的玩家。");
                    return false;
                }

                Players.Add(player);
                Console.WriteLine($"{player.Number} 号玩家加入游戏。");
            }

            return true;
        }

        /// <summary>
        /// 开始进行游戏。
        /// </summary>
        public bool Start()
        {
            if (Players.Count != 2 || Data == null)
            {
                Console.WriteLine("游戏尚未准备就绪，无法开始游戏, 目前只支持 2 位玩家。");
                return false;
            }

            CurrentPlayerIndex = new Random().Next(0, 1);
            CurrentPlayer = Players[CurrentPlayerIndex];

            while (true)
            {
                Console.WriteLine($"当前玩家为 Player: {CurrentPlayer.Number}");
                Console.WriteLine("请输入你要拿取的行数与火柴数目，以逗号(',')分割:");

                var userInput = Console.ReadLine()?.Split(',');
                if (userInput == null || userInput.Length != 2)
                {
                    continue;
                }

                if (!Pick(int.Parse(userInput[0]), int.Parse(userInput[1])))
                {
                    // 选择失败，当前玩家继续重试。
                    continue;
                }

                // 如果仍有可用火柴，继续游戏。
                if (!CheckIsOver())
                {
                    // 切换玩家。
                    CurrentPlayerIndex = CurrentPlayerIndex == 0 ? 1 : 0;
                    CurrentPlayer = Players[CurrentPlayerIndex];

                    continue;
                }

                // 没有可用火柴，游戏结束。
                Console.WriteLine($"游戏结束，输家为 {CurrentPlayer.Number}.");
                Environment.Exit(0);
            }

            return true;
        }

        /// <summary>
        /// 检测是否还有可用的火柴，没有则表示游戏已经结束。
        /// </summary>
        /// <returns>有火柴时返回 True，没有时返回 False。</returns>
        public bool CheckIsOver()
        {
            return Data.SelectMany(d => d).Count(d => !d.IsUsed) == 0;
        }
    }
}