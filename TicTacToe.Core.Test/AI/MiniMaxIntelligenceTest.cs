﻿using FluentAssertions;
using System.Collections.Generic;
using TicTacToe.Core.AI;
using TicTacToe.Core.AI.MiniMax;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.AI {
    public class MiniMaxIntelligenceTest {
        [Fact]
        public void Create_A_New_MiniMaxIntelligence() {

            var ai = BuildMiniMaxIntelligence();

            ai.Should().NotBeNull();
            ai.Should().BeAssignableTo<IIntelligence>();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(3)]
        [InlineData(3)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(4)]
        [InlineData(4)]
        [InlineData(4)]
        public void Returns_Random_Corner_When_No_Moves_Have_Been_Made(int boardSize) {
            var board = new MockBoard { Size = boardSize }.GetOpenSpacesStubbedToReturn(GetBoardCoordinates(boardSize));
            var context = BuildMiniMaxContext(board: board);
            var ai = BuildMiniMaxIntelligence();

            var move = ai.DetermineBest(context);

            var corners = new List<int> { 1, board.Size, board.Size * (board.Size - 1) + 1, board.Size * board.Size };
            corners.Should().Contain(move.ToPosition(boardSize));
        }

        //[Fact]
        //public void Returns_Last_Open_Space_When_No_Moves_Are_Left() {
        //    var lastBoardCoordinate = new BoardCoordinate(2, 2);
        //    var board = new MockBoard { Size = 3 }.GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> { lastBoardCoordinate });
        //    var context = BuildMiniMaxContext(board: board);
        //    var ai = BuildMiniMaxIntelligence();

        //    var move = ai.DetermineBest(context);

        //    move.Should().Be(lastBoardCoordinate);
        //}

        private static IEnumerable<BoardCoordinate> GetBoardCoordinates(int boardSize) {
            for (var x = 1; x <= boardSize; x++)
                for (var y = 1; y <= boardSize; y++)
                    yield return new BoardCoordinate(x, y);
        }
    
        private static MiniMaxContext BuildMiniMaxContext(IPlayer minimizePlayer = null, IPlayer maximizePlayer = null, IBoard board = null) {
            minimizePlayer = minimizePlayer ?? new MockPlayer { Symbol = 'X' };
            maximizePlayer = maximizePlayer ?? new MockPlayer { Symbol = 'O' };
            var players = new List<IPlayer> { minimizePlayer, maximizePlayer };
            board = board ?? new MockBoard { Size = 3 };
            return new MiniMaxContext {
                Board = board,
                MinimizedPlayer = minimizePlayer,
                Players = players
            };
        }

        private static MiniMaxIntelligence BuildMiniMaxIntelligence() {
            return new MiniMaxIntelligence();
        }
    }
}