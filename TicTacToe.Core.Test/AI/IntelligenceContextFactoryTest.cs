using System;
using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.AI.AlphaBetaMiniMax;
using TicTacToe.Core.AI.Human;
using TicTacToe.Core.AI.MiniMax;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.AI {
    public class IntelligenceContextFactoryTest {
        [Fact]
        public void Creates_A_New_Intelligence_Factory() {
            var factory = new IntelligenceContextFactory();
            factory.Should().BeAssignableTo<IIntelligenceContextFactory>();
        }

        [Fact]
        public void Throws_Exception_With_Invalid_AI() {
            var game = new MockGame {
                CurrentPlayer = new MockPlayer()
            };
            var factory = new IntelligenceContextFactory();

            Action action = () => factory.Create(game);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Creates_Human_Context_When_AI_Is_Human() {
            var ai = new MockHumanIntelligence();
            var board = new MockBoard();
            var player1 = new MockPlayer { Symbol = 'X' }.GetIntelligenceStubbedToReturn(ai);
            var player2 = new MockPlayer { Symbol = 'O' }.GetIntelligenceStubbedToReturn(ai);
            var players = new List<IPlayer> { player1, player2 };
            var game = new MockGame {
                Board = board,
                CurrentPlayer = player1,
                Players = players
            };
            var factory = new IntelligenceContextFactory();

            var context = factory.Create(game);

            context.Should().BeOfType<HumanContext>();
            var humanContext = (HumanContext)context;
            humanContext.Board.Should().Be(board);
        }

        [Fact]
        public void Creates_MiniMax_Context_When_AI_Is_MiniMax() {
            var ai = new MiniMaxIntelligence();
            var board = new MockBoard();
            var player1 = new MockPlayer { Symbol = 'X' }.GetIntelligenceStubbedToReturn(ai);
            var player2 = new MockPlayer { Symbol = 'O' }.GetIntelligenceStubbedToReturn(ai);
            var players = new List<IPlayer> { player1, player2 };
            var game = new MockGame {
                Board = board,
                CurrentPlayer = player1,
                Players = players
            };
            var factory = new IntelligenceContextFactory();

            var context = factory.Create(game);

            context.Should().BeOfType<MiniMaxContext>();
            var minimaxContext = (MiniMaxContext)context;
            minimaxContext.Board.Should().Be(board);
            minimaxContext.MinimizedPlayer.Should().Be(player1);
            minimaxContext.Players.Should().BeEquivalentTo(players);
        }

        [Fact]
        public void Creates_AlphaBetaMiniMax_Context_When_AI_Is_MiniMax() {
            var ai = new AlphaBetaMiniMaxIntelligence();
            var board = new MockBoard();
            var player1 = new MockPlayer { Symbol = 'X' }.GetIntelligenceStubbedToReturn(ai);
            var player2 = new MockPlayer { Symbol = 'O' }.GetIntelligenceStubbedToReturn(ai);
            var players = new List<IPlayer> { player1, player2 };
            var game = new MockGame {
                Board = board,
                CurrentPlayer = player1,
                Players = players
            };
            var factory = new IntelligenceContextFactory();

            var context = factory.Create(game);

            context.Should().BeOfType<AlphaBetaMiniMaxContext>();
        }
    }
}
