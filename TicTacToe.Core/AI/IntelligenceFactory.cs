using System;
using TicTacToe.Core.AI.AlphaBetaMiniMax;
using TicTacToe.Core.AI.Human;
using TicTacToe.Core.AI.MiniMax;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.AI {
    public class IntelligenceFactory : IIntelligenceFactory {
        private readonly IInputOutput _io;

        public IntelligenceFactory(IInputOutput io) {
            _io = io;
        }
        public IIntelligence Create(GameSettings gameSettings) {
            switch (gameSettings.GamePlayerType) {
                case GamePlayerType.HumanVsHuman:
                    return new HumanIntelligence(_io);
                case GamePlayerType.ComputerVsComputer:
                case GamePlayerType.HumanVsComputer:
                    if (gameSettings.BoardSize <= 0)
                        throw new ArgumentException("invalid board size");
                    if (gameSettings.BoardSize <= 3)
                        return new MiniMaxIntelligence();

                    return new AlphaBetaMiniMaxIntelligence();
                default:
                    throw new ArgumentException("invalid game player type");
            }
        }
    }
}