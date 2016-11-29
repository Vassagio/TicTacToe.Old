using FluentAssertions;
using TicTacToe.Core.States;
using Xunit;

namespace TicTacToe.Core.Test.States {
    public class GameEndedStateTest {
        [Fact]
        public void Initialize_A_New_Game_Ended_State() {
            var state = BuildGameEndedState();

            state.Should().NotBeNull().And.BeAssignableTo<IGameState>();
        }

        private static GameEndedState BuildGameEndedState() {
            return new GameEndedState();
        }
    }
}