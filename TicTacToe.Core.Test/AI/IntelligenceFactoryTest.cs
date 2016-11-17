using System;
using System.Collections.Generic;
using FluentAssertions;
using TicTacToe.Core.AI;
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
        [MemberData(nameof(GetGamePlayerTypesWithComputer))]
        public void Create_MiniMax_Intelligence_If_Computer_Exists(GamePlayerType gamePlayerType) {
            var players = new List<IPlayer>();
            var gameSettings = new GameSettings {
                GamePlayerType = gamePlayerType
            };
            var factory = new IntelligenceFactory();
            var ai = factory.Create(gameSettings, players);

            ai.Should().BeOfType<MiniMaxIntelligence>();
        }

        private static IEnumerable<object[]> GetGamePlayerTypesWithComputer() {
            yield return new object[] { GamePlayerType.ComputerVsComputer };
            yield return new object[] { GamePlayerType.HumanVsComputer };
        }
    }
}
