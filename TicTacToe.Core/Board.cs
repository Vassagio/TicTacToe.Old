﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class Board : IBoard, ICloneable {
        public int Size { get; }
        protected readonly IToken[,] Tokens;
        public IEnumerable<string> WinningPatterns { get; protected set; }
        public Board(int size, IPatternFactory patternsFactory) {
            if (size <= 0)
                throw new ArgumentException("invalid board size");

            Size = size;
            WinningPatterns = patternsFactory.Create(size);
            Tokens = new IToken[size, size];
        }

        protected Board(int size, IToken[,] tokens, IEnumerable<string> winningPatterns) {
            Size = size;
            Tokens = tokens;
            WinningPatterns = winningPatterns;
        }

        private IEnumerable<BoardCoordinate> ConvertTo(IToken[,] tokens) {
            for (var x = 1; x <= tokens.GetLength(0); x++) {
                for (var y = 1; y <= tokens.GetLength(1); y++) {
                    yield return new BoardCoordinate(x, y);
                }
            }
        }

        public IEnumerable<BoardCoordinate> GetAllSpaces() {
            return ConvertTo(Tokens);
        }
        public IEnumerable<BoardCoordinate> GetOpenSpaces() {
            return GetAllSpaces().Where(c => !IsCoordinateOccupied(c));
        }

        public IEnumerable<BoardCoordinate> GetClosedSpaces() {
            return GetAllSpaces().Where(IsCoordinateOccupied);
        }

        public void AddToken(IToken token, BoardCoordinate coordinate) {
            if (!coordinate.IsCoordinateValidForBoardSize(Size))
                throw new ArgumentException("invalid coordinate");

            if (IsCoordinateOccupied(coordinate))
                throw new ArgumentException("coordinate occupied");

            Tokens[coordinate.X - 1, coordinate.Y - 1] = token;
        }

        private bool IsCoordinateOccupied(BoardCoordinate coordinate) {
            return Tokens[coordinate.X - 1, coordinate.Y - 1] != null;
        }

        public string GetCurrentPattern(IPlayer player) {
            Func<int, int, int, int> getPosition = (count, x, y) => count * x + y;

            var pattern = new StringBuilder(new string('0', Tokens.Length));
            for (var x = 0; x < Tokens.GetLength(0); x++) {
                for (var y = 0; y < Tokens.GetLength(1); y++) {
                    if (IsPlayerMatch(Tokens[x, y], player)) {
                        var position = getPosition(Tokens.GetLength(0), x, y);
                        pattern.Remove(position, 1);
                        pattern.Insert(position, "1");
                    }
                }
            }
            return pattern.ToString();
        }

        private static bool IsPlayerMatch(IToken token, IPlayer player) {
            return token?.Symbol != null && token.Symbol.Equals(player.Symbol);
        }

        public object Clone() {
            //need to test
            var tokens = new IToken[Size, Size];
            for (var x = 0; x < Tokens.GetLength(0); x++) {
                for (var y = 0; y < Tokens.GetLength(1); y++) {
                    tokens[x, y] = Tokens[x, y];
                }
            }

            return new Board(Size, tokens, WinningPatterns);
        }

        public IPlayer GetWinner(IEnumerable<IPlayer> players) {
            foreach (var player in players)
                if (player.HasWon(this))
                    return player;

            return new Nobody();
        }
    }
}