using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;
using TicTacToe.Core.States;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.States {
    public class GameNotStartedStateTest {
        [Fact]
        public void Initialize_A_New_Game_Not_Stated_State() {
            var state = BuildGameNotStartedState();

            state.Should().NotBeNull().And.BeAssignableTo<IGameState>();
        }

        [Fact]
        public void Handles_Should_Return_Game_Started_State() {
            var state = BuildGameNotStartedState();

            var newState = state.Handle();

            newState.Should().NotBeNull().And.BeOfType<GameStartedState>();
        }

        private static GameNotStartedState BuildGameNotStartedState(GameSettings settings = null, IGameInitializer gameInitializer = null, IIntelligenceContextFactory contextFactory = null) {
            settings = settings ?? new GameSettings {
                BoardSize = 3,
                GamePlayerType = GamePlayerType.HumanVsHuman,
                PlayerStartType = PlayerStartType.FirstPlayerFirst
            };
            gameInitializer = gameInitializer ?? new MockGameInitializer();
            contextFactory = contextFactory ?? new MockIntelligenceContextFactory();
            return new GameNotStartedState(settings, gameInitializer, contextFactory);
        }
    }
}