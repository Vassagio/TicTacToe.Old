﻿using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core.AI;
using TicTacToe.Core.AI.Human;

namespace TicTacToe.Core.Players {
    public class PlayersFactory : IPlayersFactory {
        private readonly IIntelligence _humanAi;
        private const string DEFAULT_HUMAN_NAME = "Player";
        private const string DEFAULT_COMPUTER_NAME = "Computer";
        private const char DEFAULT_SYMBOL_1 = 'X';
        private const char DEFAULT_SYMBOL_2 = 'O';

        public PlayersFactory(IHumanIntelligence humanAI) {
            _humanAi = humanAI;
        }

        public IEnumerable<IPlayer> Create(GameSettings gameSettings, IIntelligence ai) {
            var playerSettings = gameSettings.PlayerSettings ?? BuildDefaultPlayerSettings(gameSettings, ai);

            switch (gameSettings.GamePlayerType) {
                case GamePlayerType.HumanVsHuman:
                    return CreateHumanVsHuman(playerSettings).OrderBy(gameSettings.PlayerStartType);
                case GamePlayerType.ComputerVsComputer:
                    return CreateComputerVsComputer(playerSettings).OrderBy(gameSettings.PlayerStartType);
                case GamePlayerType.HumanVsComputer:
                    return CreateHumanVsComputer(playerSettings).OrderBy(gameSettings.PlayerStartType);
                default:
                    throw new ArgumentException("invalid game player type");
            }
        }

        private IEnumerable<PlayerSettings> BuildDefaultPlayerSettings(GameSettings gameSettings, IIntelligence ai) {
            var player1Number = gameSettings.GamePlayerType == GamePlayerType.HumanVsComputer ? string.Empty : "1";
            var player1Type = gameSettings.GamePlayerType == GamePlayerType.ComputerVsComputer ? DEFAULT_COMPUTER_NAME : DEFAULT_HUMAN_NAME;
            var player1Intelligence = gameSettings.GamePlayerType != GamePlayerType.ComputerVsComputer ? _humanAi : ai;
            yield return new PlayerSettings {
                Name = $"{player1Number} {player1Type}",
                Symbol = DEFAULT_SYMBOL_1,
                Intelligence = player1Intelligence
            };

            var player2Number = gameSettings.GamePlayerType == GamePlayerType.HumanVsComputer ? string.Empty : "2";
            var player2Type = gameSettings.GamePlayerType == GamePlayerType.HumanVsHuman ? DEFAULT_HUMAN_NAME : DEFAULT_COMPUTER_NAME;
            var player2Intelligence = gameSettings.GamePlayerType != GamePlayerType.HumanVsHuman ? ai : _humanAi;
            yield return new PlayerSettings {
                Name = $"{player2Number} {player2Type}",
                Symbol = DEFAULT_SYMBOL_2,
                Intelligence = player2Intelligence
            };
        }

        private static IEnumerable<IPlayer> CreateHumanVsHuman(IEnumerable<PlayerSettings> playerSettings) {
            return playerSettings.Select(playerSetting => new HumanPlayer(playerSetting));
        }

        private static IEnumerable<IPlayer> CreateComputerVsComputer(IEnumerable<PlayerSettings> playerSettings) {
            return playerSettings.Select(playerSetting => new ComputerPlayer(playerSetting));
        }

        private static IEnumerable<IPlayer> CreateHumanVsComputer(IEnumerable<PlayerSettings> playerSettings) {
            yield return new HumanPlayer(playerSettings.Last());
            yield return new ComputerPlayer(playerSettings.First());
        }
    }
}