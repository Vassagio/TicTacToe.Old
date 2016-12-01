using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test {
    public class BoardTest {
        [Fact]
        public void Initializes_A_New_Board() {
            var board = BuildBoard();

            board.Should().NotBeNull();
            board.Should().BeOfType<Board>();
            board.Should().BeAssignableTo<ICloneable>();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void Initializes_A_New_Board_With_A_Size(int size) {
            var board = BuildBoard(size);

            board.Size.Should().Be(size);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Throws_Exception_When_Board_Is_Invalid(int boardSize) {
            Action action = () => BuildBoard(boardSize);
            action.ShouldThrow<ArgumentException>();
        }

        public class GetSpaces {
            [Theory]
            [InlineData(3, 9)]
            [InlineData(4, 16)]
            public void Returns_List_Of_All_Coordinates(int size, int numberOfSpaces) {
                var board = BuildBoard(size);

                var spaces = board.GetAllSpaces();

                spaces.Count().Should().Be(numberOfSpaces);
            }   

            [Fact]
            public void Returns_List_Of_Open_Coordinates() {
                var player = new MockPlayer { Symbol = 'X' };
                var board = BuildBoard();
                board.SetCoordinate(player, new BoardCoordinate(1, 1));
                board.SetCoordinate(player, new BoardCoordinate(1, 2));
                board.SetCoordinate(player, new BoardCoordinate(1, 3));
                board.SetCoordinate(player, new BoardCoordinate(2, 1));
                board.SetCoordinate(player, new BoardCoordinate(2, 3));
                board.SetCoordinate(player, new BoardCoordinate(3, 1));
                board.SetCoordinate(player, new BoardCoordinate(3, 2));

                var spaces = board.GetOpenSpaces();

                spaces.Count().Should().Be(2);
            }

            [Fact]
            public void Returns_List_Of_Closed_Coordinates() {
                var player = new MockPlayer { Symbol = 'X' };
                var board = BuildBoard();
                board.SetCoordinate(player, new BoardCoordinate(1, 2));
                board.SetCoordinate(player, new BoardCoordinate(2, 3));

                var spaces = board.GetClosedSpaces();

                spaces.Count().Should().Be(2);
            }
        }

        public class SetCoordinate {
            [Fact]
            public void Does_Not_Throw_Exception_When_Adding_A_Token_To_An_Unoccupied_Square() {
                var player = new MockPlayer { Symbol = 'X' };
                var board = BuildBoard();

                Action action = () => board.SetCoordinate(player, new BoardCoordinate(2, 1));

                action.ShouldNotThrow<ArgumentException>();
            }

            [Fact]
            public void Throws_Exception_When_Adding_A_Piece_To_An_Occupied_Square() {
                var player = new MockPlayer { Symbol = 'X' };
                var board = BuildBoard();
                var boardCoordinate = new BoardCoordinate(2, 1);

                board.SetCoordinate(player, boardCoordinate);

                Action action = () => board.SetCoordinate(player, boardCoordinate);

                action.ShouldThrow<ArgumentException>();
            }

            [Theory]
            [InlineData(0, 5, 8)]
            [InlineData(-12, 2, 8)]
            [InlineData(9, 5, 8)]
            [InlineData(5, 0, 8)]
            [InlineData(6, -23, 8)]
            [InlineData(7, 20, 8)]
            public void Throws_Exception_When_BoardCoordinate_Is_Not_Within_Limits(int x, int y, int boardSize) {
                var player = new MockPlayer { Symbol = 'X' };
                var board = BuildBoard(boardSize);

                Action action = () => board.SetCoordinate(player, new BoardCoordinate(x, y));

                action.ShouldThrow<ArgumentException>();
            }
        }


        public class GetCurrentPlayer {
            [Fact]
            public void Returns_The_Current_Pattern_Of_The_Board_Based_On_A_Single_Player_Single_Move() {
                var player = new MockPlayer { Symbol = 'X' };
                var board = BuildBoard(3);
                board.SetCoordinate(player, new BoardCoordinate(1, 1));

                var pattern = board.GetCurrentPattern(player);

                pattern.Should().Be("100000000");
            }

            [Fact]
            public void Returns_The_Current_Pattern_Of_The_Board_Based_On_A_Multiple_Players_Single_Move() {
                var board = BuildBoard(3);
                var player1 = new MockPlayer {Symbol = 'X'};
                var player2 = new MockPlayer {Symbol = 'O'};
                board.SetCoordinate(player1, new BoardCoordinate(1, 1));
                board.SetCoordinate(player2, new BoardCoordinate(2, 2));

                var pattern1 = board.GetCurrentPattern(player1);
                var pattern2 = board.GetCurrentPattern(player2);

                pattern1.Should().Be("100000000");
                pattern2.Should().Be("000010000");
            }

            [Fact]
            public void Returns_The_Current_Pattern_Of_The_Board_Based_On_A_Multiple_Players_Multiple_Moves() {
                var board = BuildBoard(3);
                var player1 = new MockPlayer {Symbol = 'X'};
                var player2 = new MockPlayer { Symbol = 'O'};
                board.SetCoordinate(player1, new BoardCoordinate(1, 1));
                board.SetCoordinate(player1, new BoardCoordinate(1, 2));
                board.SetCoordinate(player1, new BoardCoordinate(2, 1));
                board.SetCoordinate(player2, new BoardCoordinate(2, 2));
                board.SetCoordinate(player2, new BoardCoordinate(2, 3));
                board.SetCoordinate(player2, new BoardCoordinate(3, 3));


                var pattern1 = board.GetCurrentPattern(player1);
                var pattern2 = board.GetCurrentPattern(player2);

                pattern1.Should().Be("110100000");
                pattern2.Should().Be("000011001");
            }
        }

        [Fact]
        public void Clones_A_Board() {
            var board = BuildBoard(3);
            var player1 = new MockPlayer { Symbol = 'X' };
            var player2 = new MockPlayer { Symbol = 'O' };
            board.SetCoordinate(player1, new BoardCoordinate(1, 1));
            board.SetCoordinate(player2, new BoardCoordinate(2, 2));

            var newBoard = (Board)board.Clone();

            var closedSpaces = newBoard.GetClosedSpaces();
            closedSpaces.Count().Should().Be(2);
            closedSpaces.First().ToPosition(3).Should().Be(1);
            closedSpaces.Last().ToPosition(3).Should().Be(5);
        }


        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(3, 1, 3)]
        [InlineData(4, 2, 1)]
        [InlineData(5, 2, 2)]
        [InlineData(6, 2, 3)]
        [InlineData(7, 3, 1)]
        [InlineData(8, 3, 2)]
        [InlineData(9, 3, 3)]
        public void Returns_Coordinate_From_Position_3x3(int position, int x, int y) {
            var board = BuildBoard(3);

            var coordinate = board.ToCoordinate(position);

            coordinate.X.Should().Be(x);
            coordinate.Y.Should().Be(y);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(3, 1, 3)]
        [InlineData(4, 1, 4)]
        [InlineData(5, 2, 1)]
        [InlineData(6, 2, 2)]
        [InlineData(7, 2, 3)]
        [InlineData(8, 2, 4)]
        [InlineData(9, 3, 1)]
        [InlineData(10, 3, 2)]
        [InlineData(11, 3, 3)]
        [InlineData(12, 3, 4)]
        [InlineData(13, 4, 1)]
        [InlineData(14, 4, 2)]
        [InlineData(15, 4, 3)]
        [InlineData(16, 4, 4)]
        public void Returns_Coordinate_From_Position_4x4(int position, int x, int y) {
            var board = BuildBoard(4);

            var coordinate = board.ToCoordinate(position);

            coordinate.X.Should().Be(x);
            coordinate.Y.Should().Be(y);
        }

        [Fact]
        public void Returns_None_When_Neither_Player_Has_Won() {
            var board = BuildBoard();
            var player1 = new MockPlayer().HasWonStubbedToReturn(false);
            var player2 = new MockPlayer().HasWonStubbedToReturn(false);

            var winner = board.GetWinner(new List<IPlayer> {player1, player2});

            winner.Should().BeOfType<Nobody>();
            player1.VerifyHasWonCalled(board);
            player2.VerifyHasWonCalled(board);
        }

        [Fact]
        public void Returns_Player1_When_Player1_Has_Won() {
            var board = BuildBoard();
            var player1 = new MockPlayer().HasWonStubbedToReturn(true);
            var player2 = new MockPlayer().HasWonStubbedToReturn(false);

            var winner = board.GetWinner(new List<IPlayer> { player1, player2 });

            winner.Should().Be(player1);
            player1.VerifyHasWonCalled(board);
            player2.VerifyHasWonNotCalled();
        }

        [Fact]
        public void Returns_Player2_When_Player2_Has_Won() {
            var board = BuildBoard();
            var player1 = new MockPlayer().HasWonStubbedToReturn(false);
            var player2 = new MockPlayer().HasWonStubbedToReturn(true);

            var winner = board.GetWinner(new List<IPlayer> { player1, player2 });

            winner.Should().Be(player2);
            player1.VerifyHasWonCalled(board);
            player2.VerifyHasWonCalled(board);
        }

        [Fact]
        public void Clone_A_Board() {
            var board = BuildBoard();
            var newBoard = (IBoard)board.Clone();

            newBoard.Size.Should().Be(board.Size);
            newBoard.WinningPatterns.Should().BeEquivalentTo(board.WinningPatterns);
        }

        private static Board BuildBoard(int? size = null, IPatternFactory patternFactory = null) {
            size = size ?? 3;
            patternFactory = patternFactory ?? new MockPatternFactory();
            return new Board(size.Value, patternFactory);
        }
    }
}
