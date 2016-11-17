using System;
using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.AI {
    public class MiniMaxIntelligenceTest {

        [Fact]
        public void Create_A_New_MiniMaxIntelligence() {
            var ai = new MiniMaxIntelligence();

            ai.Should().NotBeNull();
            ai.Should().BeAssignableTo<IIntelligence>();
        }

        public class GetBestMove {
            [Fact]
            public void On_A_Full_Board_Should_Be_Null() {
                var player1 = new MockPlayer { Symbol = 'X' };
                var player2 = new MockPlayer { Symbol = 'O' };
                var players = new List<IPlayer> { player1, player2 };
                var board = new MockBoard()
                    .GetOpenSpacesStubbedToReturn(new List<BoardCoordinate>());

                var ai = new MiniMaxIntelligence();

                var move = ai.GetBestMove(board, players, player1, player1);

                move.Should().BeNull();
            }

            [Fact]
            public void On_A_Board_That_The_Current_Player_Can_Win() {
                var player1 = new MockPlayer { Symbol = 'X' };
                var player2 = new MockPlayer { Symbol = 'O' };
                var players = new List<IPlayer> { player1, player2 };
                var clonedBoard = new MockBoard().GetWinnerStubbedToReturn(player1);
                var board = new MockBoard()
                    .GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> {new BoardCoordinate(1, 1)})
                    .CloneStubbedToReturn(clonedBoard);

                var ai = new MiniMaxIntelligence();

                var move = ai.GetBestMove(board, players, player1, player1);

                move.X.Should().Be(1);
                move.Y.Should().Be(1);
            }

            [Fact]
            public void On_A_Board_That_The_Current_Player_Can_Lose_And_One_Space_Left_Return_Space() {
                var player1 = new MockPlayer { Symbol = 'X' };
                var player2 = new MockPlayer { Symbol = 'O' };
                var players = new List<IPlayer> { player1, player2 };
                var clonedBoard = new MockBoard().GetWinnerStubbedToReturn(player2);
                var board = new MockBoard()
                    .GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> { new BoardCoordinate(1, 1) })
                    .CloneStubbedToReturn(clonedBoard);

                var ai = new MiniMaxIntelligence();

                var move = ai.GetBestMove(board, players, player1, player1);

                move.X.Should().Be(1);
                move.Y.Should().Be(1);
            }

            [Fact]
            public void On_A_Board_That_The_Current_Player_Can_Win_With_A_Different_Coordinate() {
                var player1 = new MockPlayer { Symbol = 'X' };
                var player2 = new MockPlayer { Symbol = 'O' };
                var players = new List<IPlayer> { player1, player2 };
                var clonedBoard1 = new MockBoard().GetWinnerStubbedToReturn(player2);
                var clonedBoard2 = new MockBoard().GetWinnerStubbedToReturn(player1);
                var board = new MockBoard()
                    .GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> { new BoardCoordinate(1, 1), new BoardCoordinate(2, 1) })
                    .CloneStubbedToReturn(clonedBoard1, clonedBoard2);

                var ai = new MiniMaxIntelligence();

                var move = ai.GetBestMove(board, players, player1, player1);

                move.X.Should().Be(1);
                move.Y.Should().Be(1);
            }

            [Fact]
            public void Test() {
                var player1 = new MockPlayer { Symbol = 'X' };
                var player2 = new MockPlayer { Symbol = 'O' };
                var players = new List<IPlayer> { player1, player2 };
                var clonedBoard2 = new MockBoard().GetWinnerStubbedToReturn(player1);
                var clonedBoard1 = new MockBoard()
                    .GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> { new BoardCoordinate(2, 1) })
                    .CloneStubbedToReturn(clonedBoard2)
                    .GetWinnerStubbedToReturn(new Nobody(), player2);
                var board = new MockBoard()
                    .GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> { new BoardCoordinate(1, 1), new BoardCoordinate(2, 1) })
                    .CloneStubbedToReturn(clonedBoard1);

                var ai = new MiniMaxIntelligence();

                var move = ai.GetBestMove(board, players, player1, player1);

                move.X.Should().Be(2);
                move.Y.Should().Be(1);
            }

            //[Fact]
            //public void Test2() {
            //    var player1 = new MockPlayer { Symbol = 'X' };
            //    var player2 = new MockPlayer { Symbol = 'O' };
            //    var players = new List<IPlayer> { player1, player2 };
            //    var clonedBoard2 = new MockBoard()
            //        .GetWinnerStubbedToReturn(player1);
            //    var clonedBoard1 = new MockBoard()
            //        .GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> { new BoardCoordinate(2, 1) })
            //        .CloneStubbedToReturn(clonedBoard2)
            //        .GetWinnerStubbedToReturn(player2);
            //    var board = new MockBoard()
            //        .GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> { new BoardCoordinate(1, 1), new BoardCoordinate(2, 1) })
            //        .CloneStubbedToReturn(clonedBoard1);

            //    var ai = new MiniMaxIntelligence();

            //    var move = ai.GetBestMove(board, players, player1, player1);

            //    move.X.Should().Be(2);
            //    move.Y.Should().Be(1);
            //}
        }        
    }
}
