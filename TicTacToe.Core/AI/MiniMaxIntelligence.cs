using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class MiniMaxIntelligence : IIntelligence {
        private readonly IEnumerable<IPlayer> _players;

        public MiniMaxIntelligence(IEnumerable<IPlayer> players) {
            _players = players;
        }

        public BoardCoordinate DetermineBest(IBoard board, IPlayer minimizePlayer, IPlayer maximizedPlayer) {
            var openSpaces = board.GetOpenSpaces();
            if (openSpaces.Count() == board.Size * board.Size)
                return board.ToCoordinate(GetRandomCorner(board));
            if (openSpaces.Count() == 1)
                return openSpaces.First();

            var opponent = GetOpponent(minimizePlayer);
            var bestSpace = default(BoardCoordinate);

            foreach (var openSpace in openSpaces) {
                var newBoard = (IBoard)board.Clone();
                var currentSpace = (BoardCoordinate)openSpace.Clone();
                minimizePlayer.ChoosePosition(newBoard, openSpace.ToPosition(board.Size));
                var winner = newBoard.GetWinner(_players);
                if (winner is Nobody && newBoard.GetOpenSpaces().Any())
                    currentSpace.Rank = GetChildRank(newBoard, opponent, minimizePlayer);
                else
                    currentSpace.Rank = GetRank(winner, minimizePlayer);

                if (bestSpace == null)
                    bestSpace = currentSpace;
                else if (minimizePlayer == opponent && currentSpace.Rank > bestSpace.Rank)
                    bestSpace = currentSpace;
                else if (minimizePlayer == maximizedPlayer && currentSpace.Rank < bestSpace.Rank)
                    bestSpace = currentSpace;
            }
            
            return bestSpace;
        }

        private int GetRandomCorner(IBoard board) {
            var corner1 = 1;
            var corner2 = board.Size;
            var corner3 = board.Size * (board.Size - 1) + 1;
            var corner4 = board.Size * board.Size;
            var corners = new[] { corner1, corner2, corner3, corner4 };
            var random = new Random();
            return Enumerable.Range(1, 4).Select(i => corners[random.Next(4)]).First();
        }

        private int GetChildRank(IBoard board, IPlayer minimizePlayer, IPlayer maximizedPlayer) {
            return DetermineBest(board, maximizedPlayer, minimizePlayer).Rank;
        }

        private static int GetRank(IPlayer winner, IPlayer player) {
            if (winner is Nobody)
                return 0;
            if (winner == player)
                return 10;

            return -10;
        }

        private IPlayer GetOpponent(IPlayer player) {
            return _players.First(p => p.Symbol != player.Symbol);
        }
    }
}