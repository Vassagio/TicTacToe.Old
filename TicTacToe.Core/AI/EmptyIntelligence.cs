using System;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class EmptyIntelligence : IIntelligence {
        public BoardCoordinate DetermineBest(IBoard board, IPlayer minimizePlayer, IPlayer maximizedPlayer) {
            throw new NotImplementedException();
        }
    }
}