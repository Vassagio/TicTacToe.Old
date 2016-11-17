using System;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class IntelligenceFactory : IIntelligenceFactory {
        public IIntelligence Create(GameSettings gameSettings) {
            switch (gameSettings.GamePlayerType) {
                case GamePlayerType.HumanVsHuman:
                    return new EmptyIntelligence();
                case GamePlayerType.ComputerVsComputer:
                case GamePlayerType.HumanVsComputer:
                    return new MiniMaxIntelligence();
                default:
                    throw new ArgumentException("invalid game player type");
            }            
        }
    }
}
