using System.Collections.Generic;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI.MiniMax {
    public class MiniMaxContext : IIntelligenceContext {
        public IBoard Board { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public IPlayer Opponent { get; set; }
        public IPlayer MinimizedPlayer { get; set; }
        public IEnumerable<IPlayer> Players { get; set; }
    }
}