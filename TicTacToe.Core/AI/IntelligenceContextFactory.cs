using System;
using System.Linq;
using TicTacToe.Core.AI.AlphaBetaMiniMax;
using TicTacToe.Core.AI.MiniMax;

namespace TicTacToe.Core.AI {
    public class IntelligenceContextFactory: IIntelligenceContextFactory {
        public IIntelligenceContext Create(IGame game) {
            var ai = game.GetIntelligence();
            if (ai is MiniMaxIntelligence)
                return new MiniMaxContext() {
                    Board = game.Board,
                    MinimizedPlayer = game.CurrentPlayer,
                    MaximizedPlayer = game.Players.First(p => p.Symbol != game.CurrentPlayer.Symbol)
                };

            if (ai is AlphaBetaMiniMaxIntelligence)
                return new AlphaBetaMiniMaxContext();

            throw new ArgumentException("invalid ai");
        }
    }
}
