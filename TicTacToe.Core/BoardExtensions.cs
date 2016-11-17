using System;

namespace TicTacToe.Core {
    public static class BoardExtensions {
        public static BoardCoordinate ToCoordinate(this IBoard board, int position) {
            var x = position % board.Size == 0 ?  board.Size : position % board.Size;
            var y = (int)Math.Ceiling(position / (double)board.Size);
            return new BoardCoordinate(x, y);
        }
    }
}
