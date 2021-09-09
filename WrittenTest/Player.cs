namespace WrittenTest
{
    /// <summary>
    /// 玩家的对象定义。
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 玩家的编号，在同一场游戏中，玩家的编号必须是唯一的。
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// 构建一个新的 <see cref="Player"/> 对象。
        /// </summary>
        /// <param name="number">玩家的编号。</param>
        public Player(int number)
        {
            Number = number;
        }
    }
}