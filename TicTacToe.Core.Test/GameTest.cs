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

