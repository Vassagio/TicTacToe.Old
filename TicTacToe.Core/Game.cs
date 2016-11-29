using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class Game : IGame {
        public IBoard Board { get; }

        public IEnumerable<IPlayer> Players { get; }

        public IPlayer CurrentPlayer { get; private set; }

        public Game(IBoard board, IEnumerable<IPlayer> players, IPlayer startingPlayer) {
            Players = players;
            Board = board;
            CurrentPlayer = startingPlayer;
        }

        public void SwitchPlayer() {
            CurrentPlayer = Players.First(p => p.Symbol != CurrentPlayer.Symbol);
        }

        public bool IsOver() {
            return !(Board.GetWinner(Players) is Nobody) || !Board.GetOpenSpaces().Any();
        }

        public void MakeMove(IIntelligenceContext context) {
            var move = CurrentPlayer.GetBestMove(context);
            CurrentPlayer.ChoosePosition(Board, move.ToPosition(Board.Size));
        }
    }
}