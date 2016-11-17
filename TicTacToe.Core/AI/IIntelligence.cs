using System.Collections.Generic;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public interface IIntelligence {
        BoardCoordinate GetBestMove(IBoard board, IEnumerable<IPlayer> players, IPlayer originalPlayer, IPlayer currentPlayer);
    }
}
