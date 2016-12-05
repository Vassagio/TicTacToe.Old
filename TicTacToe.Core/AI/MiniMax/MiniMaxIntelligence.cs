using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI.MiniMax {
    public class MiniMaxIntelligence : IComputerIntelligence {       
        public BoardCoordinate DetermineBest(IIntelligenceContext context) {
            var miniMaxContext = (MiniMaxContext)context;
            var openSpaces = miniMaxContext.Board.GetOpenSpaces();
            if (openSpaces.Count() == miniMaxContext.Board.Size* miniMaxContext.Board.Size)
                return miniMaxContext.Board.ToCoordinate(GetRandomCorner(miniMaxContext.Board));
            //if (openSpaces.Count() == 1)
            //    return openSpaces.First();

            var opponent = GetOpponent(miniMaxContext);
            var bestSpace = default(BoardCoordinate);

            foreach (var openSpace in openSpaces) {
                var newBoard = (IBoard)miniMaxContext.Board.Clone();
                var newSpace = (BoardCoordinate) openSpace.Clone();
                miniMaxContext.MinimizedPlayer.ChoosePosition(newBoard, newSpace.ToPosition(newBoard.Size));
                var winner = newBoard.GetWinner(miniMaxContext.Players);
                if (winner is Nobody && newBoard.GetOpenSpaces().Any())
                    newSpace.Rank += GetChildRank(miniMaxContext, newBoard, opponent);
                else
                    newSpace.Rank += GetRank(winner, miniMaxContext);

                if ((bestSpace == null) || 
                    ((miniMaxContext.MinimizedPlayer.Symbol == miniMaxContext.CurrentPlayer.Symbol) && (newSpace.Rank < bestSpace.Rank)) || 
                    ((miniMaxContext.MinimizedPlayer.Symbol == miniMaxContext.Opponent.Symbol) && (newSpace.Rank > bestSpace.Rank)))
                    bestSpace = newSpace;
            }

            return bestSpace;
        }

        private static int GetRank(IPlayer winner, MiniMaxContext content) {
            if (winner is Nobody)
                return 0;
            if (winner.Symbol == content.CurrentPlayer.Symbol)
                return -10;
            if (winner.Symbol == content.Opponent.Symbol)
                return 10;
            throw new ArgumentException();
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

        private int GetChildRank(MiniMaxContext miniMaxContext, IBoard board, IPlayer minimizePlayer) {
            var context = new MiniMaxContext {
                Board = board,
                CurrentPlayer = miniMaxContext.CurrentPlayer,
                Opponent = miniMaxContext.Opponent,
                MinimizedPlayer = minimizePlayer,
                Players = miniMaxContext.Players
            };
            return DetermineBest(context).Rank;
        }

        private IPlayer GetOpponent(MiniMaxContext context) {
            return context.Players.First(p => p.Symbol != context.MinimizedPlayer.Symbol);
        }
    }
}