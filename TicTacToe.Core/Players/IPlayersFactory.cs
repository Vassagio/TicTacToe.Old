using System.Collections.Generic;
using TicTacToe.Core.AI;

namespace TicTacToe.Core.Players {
    public interface IPlayersFactory {
        IEnumerable<IPlayer> Create(GameSettings gameSettings, IIntelligence ai);
    }
}