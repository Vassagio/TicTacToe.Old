using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.Players {
    public class ComputerPlayerTest {
        [Fact]
        public void Initializes_A_New_Computer_Player() {
            var playerSettings = new PlayerSettings {
                Name = "Some Name",
                Symbol = '#'
            };
            var player = BuildComputerPlayer(playerSettings);

            player.Should().NotBeNull();
            player.Should().BeAssignableTo<IPlayer>();
            player.Should().BeOfType<ComputerPlayer>();
            player.Name.Should().Be("Some Name");
            player.Symbol.Should().Be('#');
        }

        private static ComputerPlayer BuildComputerPlayer(PlayerSettings playerSettings = null) {
            playerSettings = playerSettings ?? new PlayerSettings {
                Name = "Computer",
                Symbol = 'O'
            };
            return new ComputerPlayer(playerSettings);
        }

        [Theory]
        [InlineData(2, 1, 2)]
        [InlineData(6, 2, 3)]
        [InlineData(9, 3, 3)]
        public void Choose_A_Position(int position, int x, int y) {
            var patternFactory = new MockPatternFactory();
            var board = new Board(3, patternFactory);
            var player = BuildComputerPlayer();

            player.ChoosePosition(board, position);

            var occupiedSpaces = board.GetClosedSpaces();

            occupiedSpaces.Count().Should().Be(1);
            occupiedSpaces.First().X.Should().Be(x);
            occupiedSpaces.First().Y.Should().Be(y);
        }

        public class IsWinner {
            [Fact]
            public void Returns_True_When_Winning_Pattern_Matches() {
                var patternFactory = new MockPatternFactory().CreateStubbedToReturn(new List<string> {"1100"});
                var board = new Board(2, patternFactory);
                var player = BuildComputerPlayer();
                player.ChoosePosition(board, 1);
                player.ChoosePosition(board, 2);

                var isWinner = player.HasWon(board);

                isWinner.Should().BeTrue();
            }

            [Fact]
            public void Returns_False_When_Winning_Pattern_Matches() {
                var patternFactory = new MockPatternFactory().CreateStubbedToReturn(new List<string> { "1010" });
                var board = new Board(2, patternFactory);
                var player = BuildComputerPlayer();

                var isWinner = player.HasWon(board);

                isWinner.Should().BeFalse();
            }
        }
    }
}