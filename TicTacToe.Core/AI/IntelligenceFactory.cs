using System;
using System.Collections.Generic;
using TicTacToe.Core.AI.AlphaBetaMiniMax;
using TicTacToe.Core.AI.MiniMax;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class IntelligenceFactory : IIntelligenceFactory {
        public IIntelligence Create(GameSettings gameSettings, IEnumerable<IPlayer> players) {
            switch (gameSettings.GamePlayerType) {
                case GamePlayerType.HumanVsHuman:
                    return new EmptyIntelligence();
                case GamePlayerType.ComputerVsComputer:
                case GamePlayerType.HumanVsComputer:
                    if (gameSettings.BoardSize <= 0)
                        throw new ArgumentException("invalid board size");
                    if (gameSettings.BoardSize <= 3)
                        return new MiniMaxIntelligence(players);

                    return new AlphaBetaMiniMaxIntelligence();
                default:
                    throw new ArgumentException("invalid game player type");
            }
        }
    }
}