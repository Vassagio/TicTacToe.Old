using System.Collections.Generic;
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
        }

        private static Game BuildGame(IBoard board = null, IEnumerable<IPlayer> players = null, IPatternFactory patternFactory = null, IIntelligence ai = null) {
            patternFactory = patternFactory ?? new MockPatternFactory();
            board = board ?? new Board(3, patternFactory);
            players = players ?? new List<IPlayer> {
                          new MockPlayer(),
                          new MockPlayer()
                      };
            ai = ai ?? new MockIntelligence();

            return new Game(board, players, ai);
        }
    }
}

