using System;
using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Core.AI;
using TicTacToe.Core.AI.AlphaBetaMiniMax;
using TicTacToe.Core.AI.MiniMax;
using TicTacToe.Core.Players;
using Xunit;

namespace TicTacToe.Core.Test.AI {
    public class IntelligenceFactoryTest {
        [Fact]
        public void Creates_A_New_Intelligence_Factory() {
            var factory = new IntelligenceFactory();

            factory.Should().BeAssignableTo<IIntelligenceFactory>();
        }

        [Fact]
        public void Throws_Exception_With_Invalid_Game_Player_Type() {
            var gameSettings = new GameSettings();
            var factory = new IntelligenceFactory();

            Action action = () => factory.Create(gameSettings, new List<IPlayer>());

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Create_Empty_Intelligence_If_All_Human() {
            var gameSettings = new GameSettings {
                GamePlayerType = GamePlayerType.HumanVsHuman
            };
            var factory = new IntelligenceFactory();
            var ai = factory.Create(gameSettings, new List<IPlayer>());

            ai.Should().BeOfType<EmptyIntelligence>();
        }

        [Theory]
        [MemberData(nameof(GetMiniMaxGamePlayerTypesWithComputer))]
        public void Throws_Expection_When_Board_Size_Is_Invalid(int boardSize, GamePlayerType gamePlayerType) {
            var gameSettings = new GameSettings {
                BoardSize = boardSize,
                GamePlayerType = gamePlayerType
            };
            var factory = new IntelligenceFactory();
            var ai = factory.Create(gameSettings, new List<IPlayer>());

            ai.Should().BeOfType<MiniMaxIntelligence>();
        }

        [Theory]
        [MemberData(nameof(GetMiniMaxGamePlayerTypesWithComputer))]
        public void Create_MiniMax_Intelligence_If_Computer_Exists_When_Board_Size_Is_3_Or_Less(int boardSize, GamePlayerType gamePlayerType) {
            var gameSettings = new GameSettings {
                BoardSize = boardSize,
                GamePlayerType = gamePlayerType
            };
            var factory = new IntelligenceFactory();
            var ai = factory.Create(gameSettings, new List<IPlayer>());

            ai.Should().BeOfType<MiniMaxIntelligence>();
        }

        [Theory]
        [MemberData(nameof(GetAlphaBetaGamePlayerTypesWithComputer))]
        public void Create_AlphaBeta_Intelligence_If_Computer_Exists_When_Board_Size_Is_4_Or_More(int boardSize, GamePlayerType gamePlayerType) {
            var gameSettings = new GameSettings {
                BoardSize = boardSize,
                GamePlayerType = gamePlayerType
            };
            var factory = new IntelligenceFactory();
            var ai = factory.Create(gameSettings, new List<IPlayer>());

            ai.Should().BeOfType<AlphaBetaMiniMaxIntelligence>();
        }

        private static IEnumerable<object[]> GetInvalidSizeGamePlayerTypesWithComputer() {
            yield return new object[] { 0, GamePlayerType.ComputerVsComputer };
            yield return new object[] { 0, GamePlayerType.HumanVsComputer };
            yield return new object[] { -100, GamePlayerType.ComputerVsComputer };
            yield return new object[] { -100, GamePlayerType.HumanVsComputer };
        }

        private static IEnumerable<object[]> GetMiniMaxGamePlayerTypesWithComputer() {
            yield return new object[] { 2, GamePlayerType.ComputerVsComputer };
            yield return new object[] { 2, GamePlayerType.HumanVsComputer };
            yield return new object[] { 3, GamePlayerType.ComputerVsComputer };
            yield return new object[] { 3, GamePlayerType.HumanVsComputer };
        }

        private static IEnumerable<object[]> GetAlphaBetaGamePlayerTypesWithComputer() {
            yield return new object[] { 4, GamePlayerType.ComputerVsComputer };
            yield return new object[] { 4, GamePlayerType.HumanVsComputer };
            yield return new object[] { 100, GamePlayerType.ComputerVsComputer };
            yield return new object[] { 100, GamePlayerType.HumanVsComputer };
        }
    }
}
