using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public static class PlayersExtension {
        public static IEnumerable<IPlayer> OrderBy(this IEnumerable<IPlayer> players, PlayerStartType playerStartType) {
            switch (playerStartType) {
                case PlayerStartType.FirstPlayerFirst:
                    return players;
                case PlayerStartType.LastPlayerFirst:
                    return players.Reverse();
                default:
                    throw new ArgumentException("invalid player start type");
            }
        }
    }
}