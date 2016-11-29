using TicTacToe.Core.AI;

namespace TicTacToe.Core {
    public class PlayerSettings {
        public string Name { get; set; }
        public char Symbol { get; set; }
        public IIntelligence Intelligence { get; set; }
    }
}