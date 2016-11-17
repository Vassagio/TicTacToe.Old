using FluentAssertions;
using Xunit;

namespace TicTacToe.Core.Test {
    public class BoardCoordinateTest {
        [Fact]
        public void Initializes_A_New_Board_Coordinate() {
            var board = BuildBoardCoordinate();

            board.Should().NotBeNull();
            board.Should().BeOfType<BoardCoordinate>();
        }

        [Theory]
        [InlineData(1, 1, 8, 1)]
        [InlineData(8, 8, 8, 64)]
        [InlineData(2, 2, 3, 5)]
        [InlineData(2, 2, 6, 8)]
        [InlineData(20, 1, 20, 381)]
        [InlineData(1, 20, 20, 20)]
        public void Coordinate_Is_Within_Board_Size(int x, int y, int boardSize, int position) {
            var coordinate = BuildBoardCoordinate(x, y);

            coordinate.IsCoordinateValidForBoardSize(boardSize).Should().BeTrue();
            coordinate.ToPosition(boardSize).Should().Be(position);
        }


        [Theory]
        [InlineData(0, 5, 8)]
        [InlineData(-12, 2, 8)]
        [InlineData(9, 5, 6)]
        [InlineData(5, 0, 8)]
        [InlineData(6, -23, 10)]
        [InlineData(7, 20, 9)]
        public void Coordinate_Is_Not_Within_Board_Size(int x, int y, int boardSize) {
            var coordinate = BuildBoardCoordinate(x, y);

            coordinate.IsCoordinateValidForBoardSize(boardSize).Should().BeFalse();
        }

        private static BoardCoordinate BuildBoardCoordinate(int? x = null, int? y = null) {
            x = x ?? 3;
            y = y ?? 3;
            return new BoardCoordinate(x.Value, y.Value);
        }
    }
}