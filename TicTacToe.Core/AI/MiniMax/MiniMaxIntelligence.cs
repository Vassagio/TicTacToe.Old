using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI.MiniMax {
    public class MiniMaxIntelligence : IIntelligence {
        private readonly IEnumerable<IPlayer> _players;

        public MiniMaxIntelligence(IEnumerable<IPlayer> players) {
            _players = players;
        }

        public BoardCoordinate DetermineBest(IIntelligenceContext context) {
            var miniMaxContext = (MiniMaxContext)context;
            var openSpaces = miniMaxContext.Board.GetOpenSpaces();
            if (openSpaces.Count() == miniMaxContext.Board.Size* miniMaxContext.Board.Size)
                return miniMaxContext.Board.ToCoordinate(GetRandomCorner(miniMaxContext.Board));
            if (openSpaces.Count() == 1)
                return openSpaces.First();

            var opponent = GetOpponent(miniMaxContext.MinimizedPlayer);
            var bestSpace = default(BoardCoordinate);

            foreach (var openSpace in openSpaces) {
                var newBoard = (IBoard)miniMaxContext.Board.Clone();
                var currentSpace = (BoardCoordinate) openSpace.Clone();
                miniMaxContext.MinimizedPlayer.ChoosePosition(newBoard, openSpace.ToPosition(miniMaxContext.Board.Size));
                var winner = newBoard.GetWinner(_players);
                if (winner is Nobody && newBoard.GetOpenSpaces().Any())
                    currentSpace.Rank = GetChildRank(newBoard, opponent, miniMaxContext.MinimizedPlayer);
                else
                    currentSpace.Rank = GetRank(winner, miniMaxContext.MinimizedPlayer);

                if (bestSpace == null)
                    bestSpace = currentSpace;
                else if ((miniMaxContext.MinimizedPlayer == opponent) && (currentSpace.Rank > bestSpace.Rank))
                    bestSpace = currentSpace;
                else if ((miniMaxContext.MinimizedPlayer == miniMaxContext.MaximizedPlayer) && (currentSpace.Rank < bestSpace.Rank))
                    bestSpace = currentSpace;
            }

            return bestSpace;
        }

        private static int GetRank(IPlayer winner, IPlayer player) {
            if (winner is Nobody)
                return 0;
            if (winner == player)
                return 10;

            return -10;
        }

        private int GetRandomCorner(IBoard board) {
            var corner1 = 1;
            var corner2 = board.Size;
            var corner3 = board.Size*(board.Size - 1) + 1;
            var corner4 = board.Size*board.Size;
            var corners = new[] {
                corner1,
                corner2,
                corner3,
                corner4
            };
            var random = new Random();
            return Enumerable.Range(1, 4).Select(i => corners[random.Next(4)]).First();
        }

        private int GetChildRank(IBoard board, IPlayer minimizePlayer, IPlayer maximizedPlayer) {
            var context = new MiniMaxContext {
                Board = board,
                MinimizedPlayer = minimizePlayer,
                MaximizedPlayer = maximizedPlayer
            };
            return DetermineBest(context).Rank;
        }

        private IPlayer GetOpponent(IPlayer player) {
            return _players.First(p => p.Symbol != player.Symbol);
        }
    }
}