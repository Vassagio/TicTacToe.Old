using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test {
    public class IntegrationTest {
        [Fact]
        public void Player_1_Wins_Game() {
            var gameSettings = BuildGameSettings();
            var gameInitializer = BuildGameInitializer();

            var game = gameInitializer.Create(gameSettings);

            var currentPlayer = game.Players.First();
            currentPlayer.ChoosePosition(game.Board, 1);
            currentPlayer.HasWon(game.Board).Should().BeFalse();

            currentPlayer = game.Players.Last();
            currentPlayer.ChoosePosition(game.Board, 4);
            currentPlayer.HasWon(game.Board).Should().BeFalse();

            currentPlayer = game.Players.First();
            currentPlayer.ChoosePosition(game.Board, 2);
            currentPlayer.HasWon(game.Board).Should().BeFalse();

            currentPlayer = game.Players.Last();
            currentPlayer.ChoosePosition(game.Board, 5);
            currentPlayer.HasWon(game.Board).Should().BeFalse();

            currentPlayer = game.Players.First();
            currentPlayer.ChoosePosition(game.Board, 3);
            currentPlayer.HasWon(game.Board).Should().BeTrue();
        }

        [Fact]
        public void Player_2_Wins_Game() {
            var gameSettings = BuildGameSettings();
            var gameInitializer = BuildGameInitializer();

            var game = gameInitializer.Create(gameSettings);

            var currentPlayer = game.Players.First();

            currentPlayer.ChoosePosition(game.Board, 1);
            currentPlayer.HasWon(game.Board).Should().BeFalse();

            currentPlayer = game.Players.Last();
            currentPlayer.ChoosePosition(game.Board, 5);
            currentPlayer.HasWon(game.Board).Should().BeFalse();

            currentPlayer = game.Players.First();
            currentPlayer.ChoosePosition(game.Board, 2);
            currentPlayer.HasWon(game.Board).Should().BeFalse();

            currentPlayer = game.Players.Last();
            currentPlayer.ChoosePosition(game.Board, 3);
            currentPlayer.HasWon(game.Board).Should().BeFalse();

            currentPlayer = game.Players.First();
            currentPlayer.ChoosePosition(game.Board, 4);
            currentPlayer.HasWon(game.Board).Should().BeFalse();

            currentPlayer = game.Players.Last();
            currentPlayer.ChoosePosition(game.Board, 7);
            currentPlayer.HasWon(game.Board).Should().BeTrue();
        }

        [Fact]
        public void Game_No_Moves_Should_Always_Tie() {
            var gameSettings = BuildGameSettings(3, GamePlayerType.ComputerVsComputer);
            var gameInitializer = BuildGameInitializer();

            var game = gameInitializer.Create(gameSettings);

            var currentPlayer = game.Players.First();
            var opponent = game.Players.Last();
            do {
                var move = game.AI.DetermineBest(game.Board, currentPlayer, opponent);
                currentPlayer.ChoosePosition(game.Board, move.ToPosition(game.Board.Size));
                PlayerSwitch(ref currentPlayer, ref opponent);
            } while (game.Board.GetWinner(game.Players) is Nobody && game.Board.GetOpenSpaces().Any());

            Assert.IsType<Nobody>(game.Board.GetWinner(game.Players));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public void Game_One_Move_Should_Always_Tie(int position) {
            var gameSettings = BuildGameSettings(gamePlayerType: GamePlayerType.ComputerVsComputer);
            var gameInitializer = BuildGameInitializer();

            var game = gameInitializer.Create(gameSettings);

            var currentPlayer = game.Players.First();
            var opponent = game.Players.Last();
            opponent.ChoosePosition(game.Board, position);
            do {
                var move = game.AI.DetermineBest(game.Board, currentPlayer, opponent);
                currentPlayer.ChoosePosition(game.Board, move.ToPosition(game.Board.Size));
                PlayerSwitch(ref currentPlayer, ref opponent);
            } while (game.Board.GetWinner(game.Players) is Nobody && game.Board.GetOpenSpaces().Any());

            Assert.IsType<Nobody>(game.Board.GetWinner(game.Players));
        }

        private static void PlayerSwitch(ref IPlayer currentPlayer, ref IPlayer opponent) {
            var temp = currentPlayer;
            currentPlayer = opponent;
            opponent = temp;
        }

        private static GameSettings BuildGameSettings(int? boardSize = null, GamePlayerType? gamePlayerType = null, PlayerStartType? playerStartType = null) {
            boardSize = boardSize ?? 3;
            gamePlayerType = gamePlayerType ?? GamePlayerType.HumanVsHuman;
            playerStartType = playerStartType ?? PlayerStartType.FirstPlayerFirst;
            return new GameSettings {
                BoardSize = boardSize.Value,
                GamePlayerType = gamePlayerType.Value,
                PlayerStartType = playerStartType.Value,
                PlayerSettings = new List<PlayerSettings> {
                    new PlayerSettings {Symbol = 'X' },
                    new PlayerSettings {Symbol = 'O' },
                }
            };
        }

        private static GameInitializer BuildGameInitializer(IPlayersFactory playersFactory = null, IPatternFactory patternFactory = null, IIntelligenceFactory aiFactory = null) {
            playersFactory = playersFactory ?? new PlayersFactory();
            patternFactory = patternFactory ?? new PatternFactory();
            aiFactory = aiFactory ?? new IntelligenceFactory();
            return new GameInitializer(playersFactory, patternFactory, aiFactory);
        }
    }
}