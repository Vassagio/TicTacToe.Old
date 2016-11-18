using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class Game : IGame {
        public IBoard Board { get; }

        public IEnumerable<IPlayer> Players { get; }

        public IIntelligence AI { get; }

        public IPlayer CurrentPlayer { get; private set; }

        public Game(IBoard board, IEnumerable<IPlayer> players, IIntelligence ai, IPlayer startingPlayer) {
            Players = players;
            Board = board;
            AI = ai;
            CurrentPlayer = startingPlayer;
        }

        public void SwitchPlayer() {
            CurrentPlayer = Players.First(p => p.Symbol != CurrentPlayer.Symbol);
        }
    }
}