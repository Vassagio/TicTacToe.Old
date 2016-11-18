using System.Collections.Generic;
using StructureMap;
using TicTacToe.Core;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Console.UI {
    public class Program {
        public static void Main(string[] args) {
            var container = new Container(c => {
                c.For <IApplication>().Use<Application>();
                c.For<IGameInitializer>().Use<GameInitializer>();
                c.For<IPlayersFactory>().Use<PlayersFactory>();
                c.For<IPatternFactory>().Use<PatternFactory>();
                c.For<IIntelligenceFactory>().Use<IntelligenceFactory>();
            });

            var settings = new GameSettings {
                BoardSize = 3,
                GamePlayerType = GamePlayerType.ComputerVsComputer,
                PlayerStartType = PlayerStartType.FirstPlayerFirst,
                PlayerSettings = new List<PlayerSettings> {
                    new PlayerSettings {Symbol = 'X' },
                    new PlayerSettings {Symbol = 'O' },
                }
            };

            Run(container.GetInstance<Application>(), settings);
        }

        public static void Run(IApplication application, GameSettings settings) {
            

            application.Run(settings);
        }
    }
}
