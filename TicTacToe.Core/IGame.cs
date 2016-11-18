using System.Collections.Generic;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public interface IGame {
        IBoard Board { get; }
        IEnumerable<IPlayer> Players { get; }
        IIntelligence AI { get; }
        IPlayer CurrentPlayer { get; }

        void SwitchPlayer();
    }
}