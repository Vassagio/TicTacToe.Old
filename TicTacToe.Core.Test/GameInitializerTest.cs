using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test {
    public class GameInitializerTest {
        [Fact]
        public void Initialize_A_New_Game() {
            var initializer = BuildGameInitializer();

            initializer.Should().NotBeNull();
            initializer.Should().BeOfType<GameInitializer>();
        }

        [Fact]
        public void Creates_New_Game() {
            var initializer = BuildGameInitializer();
            var gameSettings = new GameSettings {
                BoardSize = 3,
                GamePlayerType = GamePlayerType.HumanVsHuman,
                PlayerStartType = PlayerStartType.FirstPlayerFirst
            };
            var game = initializer.Create(gameSettings);

            game.Should().NotBeNull();
            game.Should().BeOfType<Game>();
        }

        [Fact]
        public void Creates_New_3x3_Game() {
            var gameSettings = new GameSettings {
                BoardSize = 3,
                GamePlayerType = GamePlayerType.HumanVsHuman
            };
            var initializer = BuildGameInitializer();

            var game = initializer.Create(gameSettings);

            game.Should().NotBeNull();
            game.Board.Size.Should().Be(3);
        }

        [Fact]
        public void Creates_New_3x3_Game_With_Winning_Patterns() {
            var gameSettings = new GameSettings {
                BoardSize = 3,
                GamePlayerType = GamePlayerType.HumanVsHuman
            };
            var patternFactory = new MockPatternFactory();
            var initializer = BuildGameInitializer(patternFactory: patternFactory);

            var game = initializer.Create(gameSettings);

            game.Should().NotBeNull();
            game.Board.Size.Should().Be(3);
            patternFactory.VerifyCreatedCalled(3);
        }

        [Fact]
        public void Creates_New_3x3_Game_With_Players() {
            var gameSettings = new GameSettings {
                BoardSize = 3,
                GamePlayerType = GamePlayerType.HumanVsHuman,
                PlayerStartType = PlayerStartType.FirstPlayerFirst,
            };
            var playersFactory = new MockPlayersFactory().CreateStubbedToReturn(new List<IPlayer> {new MockPlayer()});
            var initializer = BuildGameInitializer(playersFactory);

            var game = initializer.Create(gameSettings);

            game.Should().NotBeNull();
            game.Board.Size.Should().Be(3);
            game.Players.Count().Should().Be(1);
            playersFactory.VerifyCreatedCalled(gameSettings);
        }

        private static GameInitializer BuildGameInitializer(IPlayersFactory playersFactory = null, IPatternFactory patternFactory = null, IIntelligenceFactory aiFactory = null) {
            playersFactory = playersFactory ?? new MockPlayersFactory();
            patternFactory = patternFactory ?? new MockPatternFactory();
            aiFactory = aiFactory ?? new MockIntelligenceFactory();
            return new GameInitializer(playersFactory, patternFactory, aiFactory);
        }
    }
}
