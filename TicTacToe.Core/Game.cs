using System.Collections.Generic;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class Game : IGame {
        public IBoard Board { get; }
        public IEnumerable<IPlayer> Players { get; }

        public IIntelligence AI { get; }

        public Game(IBoard board, IEnumerable<IPlayer> players, IIntelligence ai) {
            Players = players;
            Board = board;
            AI = ai;
        }
    }
}