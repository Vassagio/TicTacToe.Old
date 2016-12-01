using System.Collections;
using System.Collections.Generic;
using Moq;
using TicTacToe.Core.Players;

namespace TicTacToe.Core.Test.Mocks {
    public class MockBoard : IBoard {
        public int Size { get; set; }
        public IEnumerable<string> WinningPatterns { get; set; }

        private readonly Mock<IBoard> _mock;

        public MockBoard() {
            _mock = new Mock<IBoard>();
        }

        public void Preset(IPlayer player, params BoardCoordinate[] coordinates) {
            foreach (var coordinate in coordinates)
                SetCoordinate(player, coordinate);
        }

        public IEnumerable<BoardCoordinate> GetAllSpaces() {
            return _mock.Object.GetAllSpaces();
        }

        public IEnumerable<BoardCoordinate> GetOpenSpaces() {
            return _mock.Object.GetOpenSpaces();
        }

        public IEnumerable<BoardCoordinate> GetClosedSpaces() {
            return _mock.Object.GetClosedSpaces();
        }

        public void SetCoordinate(IPlayer player, BoardCoordinate coordinate) {
            _mock.Object.SetCoordinate(player, coordinate);
        }

        public string GetCurrentPattern(IPlayer player) {
            return _mock.Object.GetCurrentPattern(player);
        }

        public object Clone() {
            return _mock.Object.Clone();
        }

        public IPlayer GetWinner(IEnumerable<IPlayer> players) {
            return _mock.Object.GetWinner(players);
        }

        public bool IsPositionOpen(int position) {
            return _mock.Object.IsPositionOpen(position);
        }

        public MockBoard GetAllSpacesStubbedToReturn(IEnumerable<BoardCoordinate> coordinates) {
            _mock.Setup(m => m.GetAllSpaces()).Returns(coordinates);
            return this;
        }

        public MockBoard GetOpenSpacesStubbedToReturn(IEnumerable<BoardCoordinate> coordinates) {
            _mock.Setup(m => m.GetOpenSpaces()).Returns(coordinates);
            return this;
        }

        public MockBoard GetOpenSpacesStubbedToReturn(params IEnumerable<BoardCoordinate>[] coordinateLists) {
            var queue = new Queue<IEnumerable<BoardCoordinate>>(coordinateLists);
            _mock.Setup(m => m.GetOpenSpaces()).Returns(queue.Dequeue());
            return this;
        }

        public MockBoard GetClosedSpacesStubbedToReturn(IEnumerable<BoardCoordinate> coordinates) {
            _mock.Setup(m => m.GetClosedSpaces()).Returns(coordinates);
            return this;
        }

        public MockBoard GetCurrentPatternStubbedToReturn(string pattern) {
            _mock.Setup(m => m.GetCurrentPattern(It.IsAny<IPlayer>())).Returns(pattern);
            return this;
        }

        public MockBoard CloneStubbedToReturn(IBoard board) {
            _mock.Setup(m => m.Clone()).Returns(board);
            return this;
        }

        public MockBoard CloneStubbedToReturn(params IBoard[] boards) {
            var queue = new Queue<IBoard>(boards);
            _mock.Setup(m => m.Clone()).Returns(queue.Dequeue());
            return this;
        }

        public MockBoard GetWinnerStubbedToReturn(IPlayer player) {
            _mock.Setup(m => m.GetWinner(It.IsAny<IEnumerable<IPlayer>>())).Returns(player);
            return this;
        }

        public MockBoard GetWinnerStubbedToReturn(params IPlayer[] players) {
            var queue = new Queue<IPlayer>(players);
            _mock.Setup(m => m.GetWinner(It.IsAny<IEnumerable<IPlayer>>())).Returns(queue.Dequeue());
            return this;
        }

        public void VerifyGetAllSpacesCalled(int times = 1) {
            _mock.Verify(m => m.GetAllSpaces(), Times.Exactly(times));
        }

        public void VerifyGetAllSpacesNotCalled() {
            _mock.Verify(m => m.GetAllSpaces(), Times.Never);
        }

        public void VerifyGetOpenSpacesCalled(int times = 1) {
            _mock.Verify(m => m.GetOpenSpaces(), Times.Exactly(times));
        }

        public void VerifyGetOpenSpacesNotCalled() {
            _mock.Verify(m => m.GetOpenSpaces(), Times.Never);
        }

        public void VerifyGetClosedSpacesCalled(int times = 1) {
            _mock.Verify(m => m.GetClosedSpaces(), Times.Exactly(times));
        }

        public void VerifyGetClosedSpacesNotCalled() {
            _mock.Verify(m => m.GetClosedSpaces(), Times.Never);
        }

        public void VerifySetCoordinateCalled(IPlayer player, BoardCoordinate coordinate, int times = 1) {
            _mock.Verify(m => m.SetCoordinate(player, coordinate), Times.Exactly(times));
        }

        public void VerifyAddTokenNotCalled() {
            _mock.Verify(m => m.SetCoordinate(It.IsAny<IPlayer>(), It.IsAny<BoardCoordinate>()), Times.Never);
        }

        public void VerifyGetCurrentPatternCalled(IPlayer player, int times = 1) {
            _mock.Verify(m => m.GetCurrentPattern(player), Times.Exactly(times));
        }

        public void VerifyGetCurrentPatternNotCalled() {
            _mock.Verify(m => m.GetCurrentPattern(It.IsAny<IPlayer>()), Times.Never);
        }

        public void VerifyCloneCalled(int times = 1) {
            _mock.Verify(m => m.Clone(), Times.Exactly(times));
        }

        public void VerifyCloneNotCalled() {
            _mock.Verify(m => m.Clone(), Times.Never);
        }

        public void VerifyGetWinnerCalled(IEnumerable<IPlayer> players, int times = 1) {
            _mock.Verify(m => m.GetWinner(players), Times.Exactly(times));
        }

        public void VerifyGetWinnerNotCalled() {
            _mock.Verify(m => m.GetWinner(It.IsAny<IEnumerable<IPlayer>>()), Times.Never);
        }
    }
}