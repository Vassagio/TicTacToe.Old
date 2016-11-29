using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TicTacToe.Core.Players;
using TicTacToe.Core.Test.Mocks;
using Xunit;

namespace TicTacToe.Core.Test.Players {
    public class PlayersFactoryTest {
        [Fact]
        public void Throws_Exception_With_Invalid_Game_Player_Type() {
            var ai = new MockIntelligence();
            var gameSettings = new GameSettings();
            var factory = new PlayersFactory();

            Action action = () => factory.Create(gameSettings, ai);

            action.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [MemberData(nameof(GetGamePlayerTypes))]
        public void Throws_Exception_With_Invalid_Player_Start_Type(GamePlayerType gamePlayerType) {
            var ai = new MockIntelligence();
            var gameSettings = new GameSettings {
                GamePlayerType = gamePlayerType
            };
            var factory = new PlayersFactory();

            Action action = () => factory.Create(gameSettings, ai);

            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Returns_Human_vs_Human_First_Human_Goes_First() {
            var ai = new MockIntelligence();
            var gameSettings = new GameSettings {
                GamePlayerType = GamePlayerType.HumanVsHuman,
                PlayerStartType = PlayerStartType.FirstPlayerFirst
            };
            var factory = new PlayersFactory();

            var players = factory.Create(gameSettings, ai);

            players.Count().Should().Be(2);
            players.First().Should().BeOfType<HumanPlayer>();
            players.Last().Should().BeOfType<HumanPlayer>();
        }

        [Fact]
        public void Returns_Human_vs_Human_Last_Human_Goes_First() {
            var ai = new MockIntelligence();
            var gameSettings = new GameSettings {
                GamePlayerType = GamePlayerType.HumanVsHuman,
                PlayerStartType = PlayerStartType.LastPlayerFirst
            };
            var factory = new PlayersFactory();

            var players = factory.Create(gameSettings, ai);

            players.Count().Should().Be(2);
            players.First().Should().BeOfType<HumanPlayer>();            
            players.Last().Should().BeOfType<HumanPlayer>();
        }

        [Fact]
        public void Returns_Computer_vs_Computer_First_Computer_Goes_First() {
            var ai = new MockIntelligence();
            var gameSettings = new GameSettings {
                GamePlayerType = GamePlayerType.ComputerVsComputer,
                PlayerStartType = PlayerStartType.FirstPlayerFirst
            };
            var factory = new PlayersFactory();

            var players = factory.Create(gameSettings, ai);

            players.Count().Should().Be(2);
            players.First().Should().BeOfType<ComputerPlayer>();
            players.Last().Should().BeOfType<ComputerPlayer>();
        }

        [Fact]
        public void Returns_Computer_vs_Computer_Last_Computer_Goes_First() {
            var ai = new MockIntelligence();
            var gameSettings = new GameSettings {
                GamePlayerType = GamePlayerType.ComputerVsComputer,
                PlayerStartType = PlayerStartType.LastPlayerFirst
            };
            var factory = new PlayersFactory();

            var players = factory.Create(gameSettings, ai);

            players.Count().Should().Be(2);
            players.First().Should().BeOfType<ComputerPlayer>();
            players.Last().Should().BeOfType<ComputerPlayer>();
        }

        [Fact]
        public void Returns_Human_vs_Computer_Human_Goes_First() {
            var ai = new MockIntelligence();
            var gameSettings = new GameSettings {
                GamePlayerType = GamePlayerType.HumanVsComputer,
                PlayerStartType = PlayerStartType.FirstPlayerFirst
            };
            var factory = new PlayersFactory();

            var players = factory.Create(gameSettings, ai);

            players.Count().Should().Be(2);
            players.First().Should().BeOfType<HumanPlayer>();
            players.Last().Should().BeOfType<ComputerPlayer>();
        }

        [Fact]
        public void Returns_Human_vs_Computer_Computer_Goes_First() {
            var ai = new MockIntelligence();
            var gameSettings = new GameSettings {
                GamePlayerType = GamePlayerType.HumanVsComputer,
                PlayerStartType = PlayerStartType.LastPlayerFirst
            };
            var factory = new PlayersFactory();

            var players = factory.Create(gameSettings, ai);

            players.Count().Should().Be(2);
            players.First().Should().BeOfType<ComputerPlayer>();
            players.Last().Should().BeOfType<HumanPlayer>();
        }

        [Fact]
        public void Returns_Players_Based_On_Settings() {
            var ai = new MockIntelligence();
            var gameSettings = new GameSettings {
                GamePlayerType = GamePlayerType.HumanVsComputer,
                PlayerStartType = PlayerStartType.LastPlayerFirst,
                PlayerSettings = new List<PlayerSettings> {
                    new PlayerSettings {
                        Name = "John Doe",
                        Symbol = '@'
                    },
                    new PlayerSettings {
                        Name = "Jane Doe",
                        Symbol = '#'
                    }
                }
            };
            var factory = new PlayersFactory();

            var players = factory.Create(gameSettings, ai);

            players.Count().Should().Be(2);
            var first = players.First();
            first.Should().BeOfType<ComputerPlayer>();
            first.Name.Should().Be("John Doe");
            first.Symbol.Should().Be('@');
            var last = players.Last();
            last.Should().BeOfType<HumanPlayer>();
            last.Name.Should().Be("Jane Doe");
            last.Symbol.Should().Be('#');
        }

        private static IEnumerable<object[]> GetGamePlayerTypes() {
            yield return new object[] {GamePlayerType.ComputerVsComputer};
            yield return new object[] {GamePlayerType.HumanVsComputer};
            yield return new object[] {GamePlayerType.HumanVsHuman};
        }
    }
}