using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class Game : IGame {
        public IBoard Board { get; }

        public IEnumerable<IPlayer> Players { get; }

        private readonly IIntelligence _ai;

        public IPlayer CurrentPlayer { get; private set; }

        public Game(IBoard board, IEnumerable<IPlayer> players, IIntelligence ai, IPlayer startingPlayer) {
            Players = players;
            Board = board;
            _ai = ai;
            CurrentPlayer = startingPlayer;
        }

        public void SwitchPlayer() {
            CurrentPlayer = Players.First(p => p.Symbol != CurrentPlayer.Symbol);
        }

        public bool IsOver() {
            return !(Board.GetWinner(Players) is Nobody) || !Board.GetOpenSpaces().Any();
        }

        public void MakeMove(IIntelligenceContext context) {
            var move = _ai.DetermineBest(context);
            CurrentPlayer.ChoosePosition(Board, move.ToPosition(Board.Size));
        }

        public IIntelligence GetIntelligence() {
            return _ai;
        }
    }
}