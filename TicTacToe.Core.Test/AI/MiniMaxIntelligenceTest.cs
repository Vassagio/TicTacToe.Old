using FluentAssertions;
using TicTacToe.Core.AI;
using Xunit;

namespace TicTacToe.Core.Test.AI {
    public class MiniMaxIntelligenceTest {
        [Fact]
        public void Create_A_New_MiniMaxIntelligence() {
            var ai = new MiniMaxIntelligence();

            ai.Should().NotBeNull();
            ai.Should().BeAssignableTo<IIntelligence>();
        }
    }
}