using System;

namespace TicTacToe.Core {
    public class BoardCoordinate: ICloneable {
        public int X { get; }
        public int Y { get; }

        public int Rank { get; set; }
        public BoardCoordinate(int x, int y) {
            X = x;
            Y = y;
        }

        public bool IsCoordinateValidForBoardSize(int boardSize) {
            return IsValidDimension(X, boardSize) && IsValidDimension(Y, boardSize);
        }

        private bool IsValidDimension(int dimension, int boardSize) {
            return (dimension > 0) && (dimension <= boardSize);
        }

        public override string ToString() {
            return $"({X}, {Y})";
        }

        public object Clone() {
            return new BoardCoordinate(X, Y) {
                Rank = Rank
            };
        }
    }
}