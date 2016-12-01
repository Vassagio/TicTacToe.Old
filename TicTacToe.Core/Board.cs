using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class Board : IBoard, ICloneable {
        public int Size { get; }
        public IEnumerable<string> WinningPatterns { get; protected set; }
        protected readonly BoardCoordinate[] Coordinates;

        public Board(int size, IPatternFactory patternsFactory) {
            if (size <= 0)
                throw new ArgumentException("invalid board size");

            Size = size;
            WinningPatterns = patternsFactory.Create(size);
            Coordinates = new BoardCoordinate[size * size];
            for (int i = 0; i < Coordinates.Length; i++)
                Coordinates[i] = this.ToCoordinate(i);
        }

        protected Board(int size, BoardCoordinate[] coordinates, IEnumerable<string> winningPatterns) {
            Size = size;
            var clonedCoordinates = new BoardCoordinate[size * size];
            for (var i = 0; i < coordinates.Length; i++) 
                clonedCoordinates[i] = (BoardCoordinate)coordinates[i].Clone();
            
            Coordinates = clonedCoordinates;
            WinningPatterns = winningPatterns;
        }

        public IEnumerable<BoardCoordinate> GetAllSpaces() {
            return Coordinates.ToList();
        }

        public IEnumerable<BoardCoordinate> GetOpenSpaces() {
            return GetAllSpaces().Where(c => !IsCoordinateOccupied(c));
        }

        public IEnumerable<BoardCoordinate> GetClosedSpaces() {
            return GetAllSpaces().Where(IsCoordinateOccupied);
        }

        public void SetCoordinate(IPlayer player, BoardCoordinate coordinate) {
            if (!coordinate.IsCoordinateValidForBoardSize(Size))
                throw new ArgumentException("invalid coordinate");

            if (IsCoordinateOccupied(coordinate))
                throw new ArgumentException("coordinate occupied");

            coordinate.Symbol = player.Symbol;
            Coordinates[coordinate.ToPosition(Size) - 1] = coordinate;
        }

        public string GetCurrentPattern(IPlayer player) {
            var pattern = new StringBuilder(new string('0', Coordinates.Length));
            foreach (var coordinate in Coordinates) {
                if (coordinate != null && coordinate.Symbol == player.Symbol) {
                    var position = coordinate.ToPosition(Size);
                    pattern.Remove(position-1, 1);
                    pattern.Insert(position-1, "1");
                }
            }

            return pattern.ToString();
        }

        public object Clone() {
            return new Board(Size, Coordinates, WinningPatterns);
        }

        public IPlayer GetWinner(IEnumerable<IPlayer> players) {
            foreach (var player in players)
                if (player.HasWon(this))
                    return player;

            return new Nobody();
        }

        public bool IsPositionOpen(int position) {
            var coordinate = this.ToCoordinate(position);
            return IsCoordinateOccupied(coordinate);
        }

        public override string ToString() {
            var board = new StringBuilder();
            for (var i = 0; i < Coordinates.Length; i += 3) {
                board.Append("|");
                for (var j = 0; j < Size; j++) {
                    board.Append("- ");
                    board.Append(Coordinates[i + j].Symbol);
                    board.Append(" -");
                    board.Append("|");
                }                
            }
            
            return board.ToString();
        }

        private bool IsCoordinateOccupied(BoardCoordinate coordinate) {
            if (coordinate == null)
                return false;

            return coordinate.Symbol != '\0';
        }
    }
}