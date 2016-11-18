using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI.MiniMax {
    public class MiniMaxContext : IIntelligenceContext {
        public IBoard Board { get; set; }
        public IPlayer MinimizedPlayer { get; set; }
        public IPlayer MaximizedPlayer { get; set; }
    }
}