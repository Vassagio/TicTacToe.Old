using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.AI.Human;
using TicTacToe.Core.AI.MiniMax;
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
            var contextFactory = new IntelligenceContextFactory();
            var context = contextFactory.Create(game);
            do {
                game.MakeMove(context);
                game.SwitchPlayer();
                context = contextFactory.Create(game);
            } while (!game.IsOver());

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
            var contextFactory = new IntelligenceContextFactory();
            game.CurrentPlayer.ChoosePosition(game.Board, position);
            game.SwitchPlayer();
            var context = contextFactory.Create(game);
            do {
                game.MakeMove(context);
                game.SwitchPlayer();
                context = contextFactory.Create(game);
            } while (!game.IsOver());

            Assert.IsType<Nobody>(game.Board.GetWinner(game.Players));
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 3, 1)]
        [InlineData(3, 1, 2)]
        [InlineData(4, 5, 6)]
        [InlineData(6, 4, 5)]
        [InlineData(7, 8, 9)]
        [InlineData(8, 9, 7)]
        public void Game_Should_Block_Horizontal_Win(int firstMove, int secondMove, int blockingMove) {
            var gameSettings = BuildGameSettings(gamePlayerType: GamePlayerType.ComputerVsComputer);
            var gameInitializer = BuildGameInitializer();
            var game = gameInitializer.Create(gameSettings);
            var contextFactory = new IntelligenceContextFactory();
            game.CurrentPlayer.ChoosePosition(game.Board, firstMove);
            game.CurrentPlayer.ChoosePosition(game.Board, secondMove);
            game.SwitchPlayer();
            var context = contextFactory.Create(game);

            game.MakeMove(context);

            game.Board.IsPositionOpen(blockingMove).Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 4, 7)]
        [InlineData(4, 7, 1)]
        [InlineData(7, 1, 4)]
        [InlineData(2, 5, 8)]
        [InlineData(5, 8, 2)]
        [InlineData(8, 2, 5)]
        [InlineData(3, 6, 9)]
        [InlineData(6, 9, 3)]
        [InlineData(9, 3, 6)]
        public void Game_Should_Block_Vertical_Win(int firstMove, int secondMove, int blockingMove) {
            var gameSettings = BuildGameSettings(gamePlayerType: GamePlayerType.ComputerVsComputer);
            var gameInitializer = BuildGameInitializer();
            var game = gameInitializer.Create(gameSettings);
            var contextFactory = new IntelligenceContextFactory();
            game.CurrentPlayer.ChoosePosition(game.Board, firstMove);
            game.CurrentPlayer.ChoosePosition(game.Board, secondMove);
            game.SwitchPlayer();
            var context = contextFactory.Create(game);

            game.MakeMove(context);

            game.Board.IsPositionOpen(blockingMove).Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 5, 9)]
        [InlineData(5, 9, 1)]
        [InlineData(9, 1, 5)]
        [InlineData(3, 5, 7)]
        [InlineData(5, 7, 3)]
        [InlineData(7, 3, 5)]        
        public void Game_Should_Block_Diagonal_Win(int firstMove, int secondMove, int blockingMove) {
            var gameSettings = BuildGameSettings(gamePlayerType: GamePlayerType.ComputerVsComputer);
            var gameInitializer = BuildGameInitializer();
            var game = gameInitializer.Create(gameSettings);
            var contextFactory = new IntelligenceContextFactory();
            game.CurrentPlayer.ChoosePosition(game.Board, firstMove);
            game.CurrentPlayer.ChoosePosition(game.Board, secondMove);
            game.SwitchPlayer();
            var context = contextFactory.Create(game);

            game.MakeMove(context);

            game.Board.IsPositionOpen(blockingMove).Should().BeFalse();
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
            var io = new MockInputOutput();
            playersFactory = playersFactory ?? new PlayersFactory(new HumanIntelligence(io));
            patternFactory = patternFactory ?? new PatternFactory();
            aiFactory = aiFactory ?? new IntelligenceFactory(io);
            return new GameInitializer(playersFactory, patternFactory, aiFactory);
        }
    }
}