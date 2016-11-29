using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.AI.AlphaBetaMiniMax;
using TicTacToe.Core.AI.MiniMax;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class IntelligenceContextFactory: IIntelligenceContextFactory {
        public IIntelligenceContext Create(IGame game) {
            var ai = game.CurrentPlayer.GetIntelligence();

            if (ai is HumanIntelligence)
                return new HumanContext();

            if (ai is MiniMaxIntelligence) {
                var minimizedPlayer = game.CurrentPlayer;
                var maximizedPlayer = game.Players.First(p => p.Symbol != game.CurrentPlayer.Symbol);
                return new MiniMaxContext() {
                    Board = game.Board,
                    MinimizedPlayer = minimizedPlayer,
                    MaximizedPlayer = maximizedPlayer,
                    Players = new List<IPlayer> {minimizedPlayer, maximizedPlayer }
                };
            }

            if (ai is AlphaBetaMiniMaxIntelligence)
                return new AlphaBetaMiniMaxContext();

            throw new ArgumentException("invalid ai");
        }
    }
}
