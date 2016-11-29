using System;
using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.AI.AlphaBetaMiniMax;
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
            var game = new MockGame().GetIntelligenceStubbedToReturn(null);
            var factory = new IntelligenceContextFactory();

            Action action = () => factory.Create(game);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Creates_MiniMax_Context_When_AI_Is_MiniMax() {
            var ai = new MiniMaxIntelligence(new List<IPlayer>());
            var board = new MockBoard();
            var player1 = new MockPlayer { Symbol = 'X' };
            var player2 = new MockPlayer { Symbol = 'O' };
            var game = new MockGame {
                Board = board,
                CurrentPlayer = player1,
                Players = new List<IPlayer> {
                    player1,
                    player2
                }
            }.GetIntelligenceStubbedToReturn(ai);
            var factory = new IntelligenceContextFactory();

            var context = factory.Create(game);

            context.Should().BeOfType<MiniMaxContext>();
            var minimaxContext = (MiniMaxContext)context;
            minimaxContext.Board.Should().Be(board);
            minimaxContext.MinimizedPlayer.Should().Be(player1);
            minimaxContext.MaximizedPlayer.Should().Be(player2);
        }

        [Fact]
        public void Creates_AlphaBetaMiniMax_Context_When_AI_Is_MiniMax() {
            var ai = new AlphaBetaMiniMaxIntelligence();
            var game = new MockGame().GetIntelligenceStubbedToReturn(ai);
            var factory = new IntelligenceContextFactory();

            var context = factory.Create(game);

            context.Should().BeOfType<AlphaBetaMiniMaxContext>();
        }
    }
}
