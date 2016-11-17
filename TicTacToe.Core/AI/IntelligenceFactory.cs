using System;
using System.Collections.Generic;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class IntelligenceFactory : IIntelligenceFactory {
        public IIntelligence Create(GameSettings gameSettings, IEnumerable<IPlayer> players) {
            switch (gameSettings.GamePlayerType) {
                case GamePlayerType.HumanVsHuman:
                    return new EmptyIntelligence();
                case GamePlayerType.ComputerVsComputer:
                case GamePlayerType.HumanVsComputer:
                    return new MiniMaxIntelligence(players);
                default:
                    throw new ArgumentException("invalid game player type");
            }
        }
    }
}