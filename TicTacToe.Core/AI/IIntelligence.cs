using System.Collections.Generic;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public interface IIntelligence {
        BoardCoordinate DetermineBest(IBoard board, IPlayer minimizePlayer, IPlayer maximizedPlayer);
    }
}
