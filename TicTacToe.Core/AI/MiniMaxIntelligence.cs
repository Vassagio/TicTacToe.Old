using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class MiniMaxIntelligence : IIntelligence {        
        public BoardCoordinate GetBestMove(IBoard board, IEnumerable<IPlayer> players, IPlayer originalPlayer, IPlayer currentPlayer) {
            BoardCoordinate bestChoice = null;
            var openSpaces = board.GetOpenSpaces();
            var opponent = GetOpponent(players, currentPlayer);
            foreach (var space in openSpaces) {
                var newBoard = (IBoard)board.Clone();
                var choice = space;

                newBoard.AddToken(new Token(currentPlayer), choice);

                choice.Rank = GetRank(newBoard, players, originalPlayer, currentPlayer, opponent);
                                    
                if (IsBestChoice(originalPlayer, currentPlayer, opponent, bestChoice, choice))
                    bestChoice = choice;
            }
            return bestChoice;
        }

        private int GetRank(IBoard newBoard, IEnumerable<IPlayer> players, IPlayer originalPlayer, IPlayer currentPlayer, IPlayer opponent) {
            var winner = newBoard.GetWinner(players);
            if (winner is Nobody && newBoard.GetOpenSpaces().Any()) {
                var tempChoice = GetBestMove(newBoard, players, currentPlayer, opponent);
                return tempChoice.Rank;
            }
            
            if (winner is Nobody)
                return 0;

            if (winner == originalPlayer)
                return 10;
            
            return -10;
        }

        private static bool IsBestChoice(IPlayer originalPlayer, IPlayer currentPlayer, IPlayer opponent, BoardCoordinate bestChoice, BoardCoordinate choice) {
            return (bestChoice == null) ||
                   (originalPlayer == currentPlayer && choice.Rank < bestChoice.Rank) ||
                   (originalPlayer == opponent && choice.Rank > bestChoice.Rank);
        }

        private IPlayer GetOpponent(IEnumerable<IPlayer> players, IPlayer player) {
            return players.First(p => p.Symbol != player.Symbol);
        }
    }
}