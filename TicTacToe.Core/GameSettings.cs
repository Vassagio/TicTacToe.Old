using System.Collections.Generic;
using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class GameSettings {
        public int BoardSize { get; set; }
        public GamePlayerType GamePlayerType { get; set; }
        public PlayerStartType PlayerStartType { get; set; }
        public IEnumerable<PlayerSettings> PlayerSettings { get; set; }
    }
}