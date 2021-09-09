namespace WrittenTest
{
    /// <summary>
    /// 火柴棍的对象定义。
    /// </summary>
    public class Matchstick<TValue>
    {
        /// <summary>
        /// 火柴棍的唯一标识。
        /// </summary>
        public TValue Identity { get; }

        /// <summary>
        /// 当前火柴棍是否已经被玩家选择。
        /// </summary>
        public bool IsUsed { get; set; } = false;

        /// <summary>
        /// 火柴棍选择状态的文字说明。
        /// </summary>
        public string IsUsedStr => IsUsed ? "已选" : "未选";

        /// <summary>
        /// 构建一个新的 <see cref="Matchstick{TValue}"/> 对象。
        /// </summary>
        /// <param name="identity">火柴棍的唯一标识，</param>
        public Matchstick(TValue identity)
        {
            Identity = identity;
        }
    }
}