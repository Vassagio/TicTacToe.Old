using System;
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
            var gameSettings = BuildGameSettings();
            var game = initializer.Create(gameSettings);

            game.Should().NotBeNull();
            game.Should().BeOfType<Game>();
        }

        [Fact]
        public void Creates_New_3x3_Game() {
            var gameSettings = BuildGameSettings(boardSize: 3);
            var initializer = BuildGameInitializer();

            var game = initializer.Create(gameSettings);

            game.Should().NotBeNull();
            game.Board.Size.Should().Be(3);
        }

        [Fact]
        public void Creates_New_Game_With_Winning_Patterns() {
            var gameSettings = BuildGameSettings();
            var patternFactory = new MockPatternFactory();
            var initializer = BuildGameInitializer(patternFactory: patternFactory);

            var game = initializer.Create(gameSettings);

            game.Should().NotBeNull();
            game.Board.Size.Should().Be(3);
            patternFactory.VerifyCreatedCalled(3);
        }

        [Fact]
        public void Creates_New_Game_With_Players() {
            var gameSettings = BuildGameSettings();
            var playersFactory = new MockPlayersFactory().CreateStubbedToReturn(new List<IPlayer> {new MockPlayer()});
            var initializer = BuildGameInitializer(playersFactory);

            var game = initializer.Create(gameSettings);

            game.Should().NotBeNull();
            game.Board.Size.Should().Be(3);
            game.Players.Count().Should().Be(1);
            playersFactory.VerifyCreatedCalled(gameSettings);
        }

        [Fact]
        public void Throws_Exception_When_Setting_Current_Player_With_Unknown_Type() {
            var gameSettings = BuildGameSettings(playerStartType: 0);
            var initializer = BuildGameInitializer();

            Action action = () => initializer.Create(gameSettings);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Throws_Exception_When_Setting_Current_Player_With_No_Players() {
            var gameSettings = BuildGameSettings();
            var playersFactory = new MockPlayersFactory().CreateStubbedToReturn(new List<IPlayer>());
            var initializer = BuildGameInitializer(playersFactory);

            Action action = () => initializer.Create(gameSettings);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Creates_New_Game_Setting_Current_Player_To_Be_First_Player() {
            var gameSettings = BuildGameSettings(playerStartType: PlayerStartType.FirstPlayerFirst);
            var player1 = new MockPlayer();
            var player2 = new MockPlayer();
            var playersFactory = new MockPlayersFactory().CreateStubbedToReturn(new List<IPlayer> { player1, player2});
            var initializer = BuildGameInitializer(playersFactory);

            var game = initializer.Create(gameSettings);

            game.CurrentPlayer.Should().Be(player1);
        }

        [Fact]
        public void Creates_New_Game_Setting_Current_Player_To_Be_Last_Player() {
            var gameSettings = BuildGameSettings(playerStartType: PlayerStartType.LastPlayerFirst);
            var player1 = new MockPlayer();
            var player2 = new MockPlayer();
            var playersFactory = new MockPlayersFactory().CreateStubbedToReturn(new List<IPlayer> { player1, player2 });
            var initializer = BuildGameInitializer(playersFactory);

            var game = initializer.Create(gameSettings);

            game.CurrentPlayer.Should().Be(player2);
        }

        private static GameSettings BuildGameSettings(int? boardSize = null, GamePlayerType? gamePlayerType = null, PlayerStartType? playerStartType = null) {
            boardSize = boardSize ?? 3;
            gamePlayerType = gamePlayerType ?? GamePlayerType.HumanVsHuman;
            playerStartType = playerStartType ?? PlayerStartType.FirstPlayerFirst;
            return new GameSettings {
                BoardSize = boardSize.Value,
                GamePlayerType = gamePlayerType.Value,
                PlayerStartType = playerStartType.Value
            };
        }

        private static GameInitializer BuildGameInitializer(IPlayersFactory playersFactory = null, IPatternFactory patternFactory = null, IIntelligenceFactory aiFactory = null) {
            var player1 = new MockPlayer();
            var player2 = new MockPlayer();
            playersFactory = playersFactory ?? new MockPlayersFactory().CreateStubbedToReturn(new List<IPlayer> { player1, player2 }); ;
            patternFactory = patternFactory ?? new MockPatternFactory();
            aiFactory = aiFactory ?? new MockIntelligenceFactory();
            return new GameInitializer(playersFactory, patternFactory, aiFactory);
        }
    }
}
