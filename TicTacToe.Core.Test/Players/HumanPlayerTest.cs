using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.Players {
    public class HumanPlayerTest {
        [Fact]
        public void Initializes_A_New_Human_Player() {
            var playerSettings = new PlayerSettings {
                Name = "Some Name",
                Symbol = '@'
            };
            var player = BuildHumanPlayer(playerSettings);

            player.Should().NotBeNull();
            player.Should().BeAssignableTo<IPlayer>();
            player.Should().BeOfType<HumanPlayer>();
            player.Name.Should().Be("Some Name");
            player.Symbol.Should().Be('@');
        }

        private static HumanPlayer BuildHumanPlayer(PlayerSettings playerSettings = null) {
            playerSettings = playerSettings ?? new PlayerSettings {
                Name = "Computer",
                Symbol = 'O'
            };
            return new HumanPlayer(playerSettings);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(3, 1, 3)]
        [InlineData(5, 2, 2)]
        public void Choose_A_Position(int position, int x, int y) {
            var patternFactory = new MockPatternFactory();
            var board = new Board(3, patternFactory);
            var player = BuildHumanPlayer();

            player.ChoosePosition(board, position);

            var occupiedSpaces = board.GetClosedSpaces();

            occupiedSpaces.Count().Should().Be(1);
            occupiedSpaces.First().X.Should().Be(x);
            occupiedSpaces.First().Y.Should().Be(y);
        }

        public class IsWinner {
            [Fact]
            public void Returns_True_When_Winning_Pattern_Matches() {
                var patternFactory = new MockPatternFactory().CreateStubbedToReturn(new List<string> { "1100" });
                var board = new Board(2, patternFactory);
                var player = BuildHumanPlayer();
                player.ChoosePosition(board, 1);
                player.ChoosePosition(board, 2);

                var isWinner = player.HasWon(board);

                isWinner.Should().BeTrue();
            }

            [Fact]
            public void Returns_False_When_Winning_Pattern_Matches() {
                var patternFactory = new MockPatternFactory().CreateStubbedToReturn(new List<string> { "1010" });
                var board = new Board(2, patternFactory);
                var player = BuildHumanPlayer();

                var isWinner = player.HasWon(board);

                isWinner.Should().BeFalse();
            }
        }
    }
}