
using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;
using TicTacToe.Core.States;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.States {
    public class Player1TurnStateTest {
        [Fact]
        public void Initialize_A_New_Player_1_Turn_State() {
            var state = BuildPlayer1TurnState();

            state.Should().NotBeNull().And.BeAssignableTo<IGameState>();
        }

        [Fact]
        public void Handles_Should_Choose_A_Position() {
            var context = new MockIntelligenceContext();
            var currentPlayer = new MockPlayer();
            var player = new MockPlayer();
            var board = new MockBoard {
                Size = 3
            };
            var game = new MockGame {
                Board = board,
                Players = new List<IPlayer> { currentPlayer, player },
                CurrentPlayer = currentPlayer
            }.IsOverStubbedToReturn(false);
            var newContext = new MockIntelligenceContext();
            var contextFactory = new MockIntelligenceContextFactory().CreateStubbedToReturn(newContext);

            var state = BuildPlayer1TurnState(game, contextFactory, context);

            state.Handle();

            game.VerifyMakeMoveCalled(context);
            game.VerifySwitchPlayerCalled();
            contextFactory.VerifyCreatedCalled(game);
        }

        [Fact]
        public void Handles_Should_Return_Player_2_Turn_When_Game_Has_Not_Been_Won() {
            var game = new MockGame().IsOverStubbedToReturn(false);
            var state = BuildPlayer1TurnState(game);

            var newState = state.Handle();

            newState.Should().NotBeNull().And.BeOfType<Player2TurnState>();
            game.VerifyIsOverCalled();
        }

        [Fact]
        public void Handles_Should_Return_Game_Ended_When_Game_Has_Been_Won() {
            var game = new MockGame().IsOverStubbedToReturn(true);
            var state = BuildPlayer1TurnState(game);

            var newState = state.Handle();

            newState.Should().NotBeNull().And.BeOfType<GameEndedState>();
            game.VerifyIsOverCalled();
        }

        private static Player1TurnState BuildPlayer1TurnState(IGame game = null, IIntelligenceContextFactory contextFactory = null, IIntelligenceContext context = null) {
            game = game ?? new MockGame();
            contextFactory = contextFactory ?? new MockIntelligenceContextFactory();
            context = context ?? new MockIntelligenceContext();
            return new Player1TurnState(game, contextFactory, context);
        }
    }
}
