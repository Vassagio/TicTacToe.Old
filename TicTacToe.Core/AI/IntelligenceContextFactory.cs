using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.AI.AlphaBetaMiniMax;
using TicTacToe.Core.AI.Human;
using TicTacToe.Core.AI.MiniMax;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class IntelligenceContextFactory: IIntelligenceContextFactory {
        public IIntelligenceContext Create(IGame game) {
            var ai = game.CurrentPlayer.GetIntelligence();

            if (ai is IHumanIntelligence)
                return new HumanContext {
                    Board = game.Board
                };

            if (ai is MiniMaxIntelligence) {
                var currentPlayer = game.CurrentPlayer;
                var opponent = game.Players.First(p => p.Symbol != game.CurrentPlayer.Symbol);
                return new MiniMaxContext() {
                    Board = game.Board,
                    CurrentPlayer = currentPlayer,
                    Opponent = opponent,
                    MinimizedPlayer = currentPlayer,
                    Players = new List<IPlayer> { currentPlayer, opponent }
                };
            }

            if (ai is AlphaBetaMiniMaxIntelligence)
                return new AlphaBetaMiniMaxContext();

            throw new ArgumentException("invalid ai");
        }
    }
}
