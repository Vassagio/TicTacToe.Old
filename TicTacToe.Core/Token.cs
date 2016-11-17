using TicTacToe.Core.Players;

namespace TicTacToe.Core {
    public class Token : IToken {
        public char Symbol { get; }

        public Token(IPlayer player) {
            Symbol = player.Symbol;
        }

        public override string ToString() {
            return Symbol.ToString();
        }
    }
}