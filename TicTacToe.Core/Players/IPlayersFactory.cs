using System.Collections.Generic;

namespace TicTacToe.Core.Players {
    public interface IPlayersFactory {
        IEnumerable<IPlayer> Create(GameSettings gameSettings);
    }
}