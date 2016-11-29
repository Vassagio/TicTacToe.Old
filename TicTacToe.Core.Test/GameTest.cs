using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test {
    public class GameTest {
        [Fact]
        public void Initializes_A_New_Game() {
            var game = BuildGame();

            game.Should().NotBeNull();
            game.Should().BeOfType<Game>();
            game.Board.Should().NotBeNull();
            game.Players.Should().NotBeNull();
            game.CurrentPlayer.Should().NotBeNull();
        }

        [Fact]
        public void Switches_The_Current_Player() {
            var player1 = new MockPlayer {Symbol = 'X'};
            var player2 = new MockPlayer {Symbol = 'Y'};

            var game = BuildGame(players: new List<IPlayer> {player1, player2});

            game.CurrentPlayer.Should().Be(player1);
            game.SwitchPlayer();
            game.CurrentPlayer.Should().Be(player2);
            game.SwitchPlayer();
            game.CurrentPlayer.Should().Be(player1);
        }

        [Fact]
        public void Returns_True_When_Game_Is_Over_Because_Player_Has_Won() {
            var player1 = new MockPlayer { Symbol = 'X' };
            var player2 = new MockPlayer { Symbol = 'Y' };
            var board = new MockBoard().GetWinnerStubbedToReturn(player1).GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> {new BoardCoordinate(1, 1)});

            var game = BuildGame(board, new List<IPlayer> { player1, player2 });

            game.IsOver().Should().BeTrue();
        }

        [Fact]
        public void Returns_True_When_Game_Is_Over_Because_Of_Tie() {
            var board = new MockBoard().GetWinnerStubbedToReturn(new Nobody()).GetOpenSpacesStubbedToReturn(new List<BoardCoordinate>());

            var game = BuildGame(board);

            game.IsOver().Should().BeTrue();
        }

        [Fact]
        public void Returns_False_When_Game_Is_Not_Over() {
            var board = new MockBoard().GetWinnerStubbedToReturn(new Nobody()).GetOpenSpacesStubbedToReturn(new List<BoardCoordinate> { new BoardCoordinate(1, 1) });

            var game = BuildGame(board);

            game.IsOver().Should().BeFalse();
        }

        [Fact]
        public void Returns_Board_Coordinate_With_Context() {
            var context = new MockIntelligenceContext();
            var boardCoordinate = new BoardCoordinate(1, 1);
            var ai = new MockIntelligence().DetermineBestStubbedToReturn(boardCoordinate);
            var game = BuildGame(ai: ai);

            game.MakeMove(context);

            ai.VerifyDetermineBestCalled(context);
        }

        [Fact]
        public void Get_Intelligence_Returns_AI() {
            var ai = new MockIntelligence();
            var game = BuildGame(ai: ai);

            game.GetIntelligence().Should().Be(ai);
        }

        private static Game BuildGame(IBoard board = null, IEnumerable<IPlayer> players = null, IPatternFactory patternFactory = null, IIntelligence ai = null) {
            patternFactory = patternFactory ?? new MockPatternFactory();
            board = board ?? new Board(3, patternFactory);
            players = players ?? new List<IPlayer> {
                          new MockPlayer(),
                          new MockPlayer()
                      };
            ai = ai ?? new MockIntelligence();

            return new Game(board, players, ai, players.First());
        }
    }
}

