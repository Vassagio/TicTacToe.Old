using FluentAssertions;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test {
    public class TokenTest {
        [Fact]
        public void Initializes_A_New_Token() {
            var token = BuildToken();

            token.Should().NotBeNull();
            token.Should().BeOfType<Token>();
            token.Symbol.Should().NotBeNull();
        }

        private static Token BuildToken(IPlayer player = null) {
            player = player ?? new MockPlayer();
            return new Token(player);
        }
    }
}
