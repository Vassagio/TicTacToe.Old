
using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;
using TicTacToe.Core.States;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.States {
    public class Player2TurnStateTest {
        [Fact]
        public void Initialize_A_New_Player_2_Turn_State() {
            var state = BuildPlayer2TurnState();

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
            var boardCoordinate = new BoardCoordinate(1, 1);
            var game = new MockGame {
                Board = board,
                Players = new List<IPlayer> { currentPlayer, player },
                CurrentPlayer = currentPlayer
            }.IsOverStubbedToReturn(false);
            var newContext = new MockIntelligenceContext();
            var contextFactory = new MockIntelligenceContextFactory().CreateStubbedToReturn(newContext);

            var state = BuildPlayer2TurnState(game, contextFactory, context);

            state.Handle();

            game.VerifyMakeMoveCalled(context);
            game.VerifySwitchPlayerCalled();
            contextFactory.VerifyCreatedCalled(game);
        }

        [Fact]
        public void Handles_Should_Return_Player_1_Turn_When_Game_Has_Not_Been_Won() {
            var game = new MockGame {
                Board = new MockBoard()
            }.IsOverStubbedToReturn(false);
            var state = BuildPlayer2TurnState(game);

            var newState = state.Handle();

            newState.Should().NotBeNull().And.BeOfType<Player1TurnState>();
            game.VerifyIsOverCalled();
        }

        [Fact]
        public void Handles_Should_Return_Game_Ended_When_Game_Has_Been_Won() {
            var game = new MockGame {
                Board = new MockBoard()
            }.IsOverStubbedToReturn(true);
            var state = BuildPlayer2TurnState(game);

            var newState = state.Handle();

            newState.Should().NotBeNull().And.BeOfType<GameEndedState>();
            game.VerifyIsOverCalled();
        }

        private static Player2TurnState BuildPlayer2TurnState(IGame game = null, IIntelligenceContextFactory contextFactory = null, IIntelligenceContext context = null, IInputOutput io = null) {
            game = game ?? new MockGame();
            contextFactory = contextFactory ?? new MockIntelligenceContextFactory();
            context = context ?? new MockIntelligenceContext();
            io = io ?? new MockInputOutput();
            return new Player2TurnState(game, contextFactory, context, io);
        }
    }
}
