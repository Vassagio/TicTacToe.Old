using System;

namespace TicTacToe.Core {
    public static class BoardExtensions {
        public static BoardCoordinate ToCoordinate(this IBoard board, int position) {
            var x = (int)Math.Ceiling(position / (double)board.Size);
            var y = position % board.Size == 0 ?  board.Size : position % board.Size;
            return new BoardCoordinate(x, y);
        }

        public static int ToPosition(this BoardCoordinate coordinate, int boardSize) {
            return (boardSize * (coordinate.X - 1) + (coordinate.Y - 1)) + 1;
        }
    }
}
