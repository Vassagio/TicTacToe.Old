using System.Collections.Generic;
using StructureMap;
using TicTacToe.Core;
using TicTacToe.Core.AI;
using TicTacToe.Core.AI.Human;
using TicTacToe.Core.Players;

namespace TicTacToe.Console.UI {
    public class Program {
        public static void Main(string[] args) {
            var container = new Container(c => {
                c.For<IApplication>().Use<Application>();
                c.For<IGameInitializer>().Use<GameInitializer>();
                c.For<IPlayersFactory>().Use<PlayersFactory>();
                c.For<IPatternFactory>().Use<PatternFactory>();
                c.For<IIntelligenceFactory>().Use<IntelligenceFactory>();
                c.For<IIntelligenceContextFactory>().Use<IntelligenceContextFactory>();
                c.For<IInputOutput>().Use<ConsoleIO>();
                c.For<IHumanIntelligence>().Use<HumanIntelligence>();
            });

            var settings = new GameSettings {
                BoardSize = 3,
                GamePlayerType = GamePlayerType.HumanVsComputer,
                PlayerStartType = PlayerStartType.FirstPlayerFirst                
            };

            Run(container.GetInstance<Application>(), settings);
        }

        public static void Run(IApplication application, GameSettings settings) {
            application.Run(settings);
        }
    }
}
