using System.Collections.Generic;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class EmptyIntelligence: IIntelligence {
        public BoardCoordinate GetBestMove(IBoard board, IEnumerable<IPlayer> players, IPlayer originalPlayer, IPlayer currentPlayer) {
            throw new System.NotImplementedException();
        }
    }
}
