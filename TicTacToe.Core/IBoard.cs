using System.Collections.Generic;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public interface IBoard {
        int Size { get; }
        IEnumerable<string> WinningPatterns { get; }
        IEnumerable<BoardCoordinate> GetAllSpaces();
        IEnumerable<BoardCoordinate> GetOpenSpaces();
        IEnumerable<BoardCoordinate> GetClosedSpaces();
        void SetCoordinate(IPlayer player, BoardCoordinate coordinate);
        string GetCurrentPattern(IPlayer player);
        object Clone();
        IPlayer GetWinner(IEnumerable<IPlayer> players);
        bool IsPositionOpen(int position);
    }
}