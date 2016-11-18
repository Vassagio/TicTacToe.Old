using System;
using System.Collections.Generic;
using Moq;
using TicTacToe.Core.AI;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Test.Mocks {
    public class MockGame : IGame {
        public IBoard Board { get; set; }
        public IEnumerable<IPlayer> Players { get; set; }
        public IIntelligence AI { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        private readonly Mock<IGame> _mock;

        public MockGame() {
            _mock = new Mock<IGame>();
        }

        public void SwitchPlayer() {
            _mock.Object.SwitchPlayer();
        }

        public void VerifySwitchPlayerCalled(int times = 1) {
            _mock.Verify(m => m.SwitchPlayer(), Times.Exactly(times));
        }

        public void VerifySwitchPlayerNotCalled() {
            _mock.Verify(m => m.SwitchPlayer(), Times.Never);
        }
    }
}