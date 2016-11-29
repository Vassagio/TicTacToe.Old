using System;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;
using TicTacToe.Core.States;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.States {
    public class GameStartedStateTest {
        [Fact]
        public void Initialize_A_New_Game_Stated_State() {
            var state = BuildGameStartedState();

            state.Should().NotBeNull().And.BeAssignableTo<IGameState>();
        }

        [Fact]
        public void Throws_Exception_When_Player_Start_Type_Is_Invalid() {
            var settings = new GameSettings {
                PlayerStartType = 0
            };
            var state = BuildGameStartedState(settings);

            Action action = () => state.Handle();

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Handles_Should_Return_Player_1_Turn_When_Player_1_Starts_The_Game() {
            var settings = new GameSettings {
                PlayerStartType = PlayerStartType.FirstPlayerFirst
            };
            var state = BuildGameStartedState(settings);

            var newState = state.Handle();

            newState.Should().NotBeNull().And.BeOfType<Player1TurnState>();
        }

        [Fact]
        public void Handles_Should_Return_Player_2_Turn_When_Player_2_Starts_The_Game() {
            var settings = new GameSettings {
                PlayerStartType = PlayerStartType.LastPlayerFirst
            };
            var state = BuildGameStartedState(settings);

            var newState = state.Handle();

            newState.Should().NotBeNull().And.BeOfType<Player2TurnState>();
        }

        [Fact]
        public void Verify_New_Context_Created() {
            var gameSettings = new GameSettings() {
                PlayerStartType = PlayerStartType.FirstPlayerFirst
            };
            var game = new MockGame();
            var gameInitializer = new MockGameInitializer().CreateStubbedToReturn(game);
            var contextFactory = new MockIntelligenceContextFactory();
            var state = BuildGameStartedState(gameSettings, gameInitializer, contextFactory);

            state.Handle();

            gameInitializer.VerifyCreateCalled(gameSettings);
            contextFactory.VerifyCreatedCalled(game);
        }

        private static GameStartedState BuildGameStartedState(GameSettings settings = null, IGameInitializer gameInitializer = null, IIntelligenceContextFactory contextFactory = null) {
            settings = settings ?? new GameSettings {
                BoardSize = 3,
                GamePlayerType = GamePlayerType.HumanVsHuman,
                PlayerStartType = PlayerStartType.FirstPlayerFirst
            };
            gameInitializer = gameInitializer ?? new MockGameInitializer();
            contextFactory = contextFactory ?? new MockIntelligenceContextFactory();
            return new GameStartedState(settings, gameInitializer, contextFactory);
        }
    }
}
