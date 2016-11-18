using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.AI.AlphaBetaMiniMax;
using Xunit;

namespace TicTacToe.Core.Test.AI {
    public class AlphaBetaMiniMaxIntelligenceTest {
        [Fact]
        public void Create_A_New_MiniMaxIntelligence() {

            var ai = BuildMiniMaxIntelligence();

            ai.Should().NotBeNull();
            ai.Should().BeAssignableTo<IIntelligence>();
        }

        private static AlphaBetaMiniMaxIntelligence BuildMiniMaxIntelligence() {
            return new AlphaBetaMiniMaxIntelligence();
        }
    }
}