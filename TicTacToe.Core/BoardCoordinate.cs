using System;

namespace TicTacToe.Core {
    public class BoardCoordinate : ICloneable {
        public int X { get; }
        public int Y { get; }

        public int Rank { get; set; }
        public char Symbol { get; set; }

        public BoardCoordinate(int x, int y) {
            X = x;
            Y = y;
        }

        public object Clone() {
            return new BoardCoordinate(X, Y) {
                Rank = Rank,
                Symbol = Symbol
            };
        }

        public bool IsCoordinateValidForBoardSize(int boardSize) {
            return IsValidDimension(X, boardSize) && IsValidDimension(Y, boardSize);
        }

        public override string ToString() {
            return $"({X}, {Y})";
        }

        private bool IsValidDimension(int dimension, int boardSize) {
            return (dimension > 0) && (dimension <= boardSize);
        }
    }
}