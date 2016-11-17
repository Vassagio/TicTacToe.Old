using System.Collections.Generic;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public interface IIntelligenceFactory {
        IIntelligence Create(GameSettings gameSettings, IEnumerable<IPlayer> players);
    }
}
