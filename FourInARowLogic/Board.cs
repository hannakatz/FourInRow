using FourInARowLogic.Enums;
using System;
using System.Collections.Generic;

namespace FourInARowLogic
{
    public delegate void PlayerMovedHandler(int i_Row, int i_Col);
    public delegate void ColBlockedHandler(int i_Col);
    public delegate void GameStatusChangedHandler(eGameStatus i_GameStatus);
    public class Board
    {
        private readonly int[,] r_Matrix;
        private readonly GameSettings r_GameSettings;
        private readonly WinChecker r_WinChecker;
        private readonly Random r_Random;

        private int m_MovesLeft;

        public event PlayerMovedHandler OnPlayerMoved;
        public event ColBlockedHandler OnBlockedCol;
        public event GameStatusChangedHandler OnGameStatusChanged;

        public Board(GameSettings i_GameSettings)
        {
            r_GameSettings = i_GameSettings;
            r_Matrix = new int[r_GameSettings.Rows, r_GameSettings.Cols];
            m_MovesLeft = r_Matrix.Length;
            r_WinChecker = new WinChecker(r_GameSettings, r_Matrix);
            r_GameSettings.CurrentPlayer = r_GameSettings.Player1;
            r_Random = new Random();
        }

        public void Reset()
        {
            for (int row = 0; row < r_GameSettings.Rows; row++)
            {
                for (int col = 0; col < r_GameSettings.Cols; col++)
                {
                    r_Matrix[row, col] = 0;
                }
            }

            m_MovesLeft = r_Matrix.Length;

            computerMove();
        }

        public void Move(int i_Col)
        {
            for (int row = 0; row < r_GameSettings.Rows; row++)
            {
                if (r_Matrix[row, i_Col] == 0)
                {
                    r_Matrix[row, i_Col] = (int)r_GameSettings.CurrentPlayer.Type;

                    m_MovesLeft--;

                    OnPlayerMoved?.Invoke(row, i_Col);

                    if (row == r_GameSettings.Rows - 1)
                    {
                        OnBlockedCol?.Invoke(i_Col);
                    }

                    checkGameStatus(row, i_Col);

                    break;
                }
            }
        }

        private void checkGameStatus(int i_Row, int i_Col)
        {
            if (r_WinChecker.IsWin(i_Row, i_Col))
            {
                OnGameStatusChanged?.Invoke(eGameStatus.Win);
            }
            else if (m_MovesLeft == 0)
            {
                OnGameStatusChanged?.Invoke(eGameStatus.Tie);
            }
            else
            {
                switchPlayers();
                computerMove();
            }
        }

        private void switchPlayers()
        {
            r_GameSettings.CurrentPlayer = r_GameSettings.CurrentPlayer == r_GameSettings.Player2 ? r_GameSettings.Player1 : r_GameSettings.Player2;
        }

        private void computerMove()
        {
            if (r_GameSettings.CurrentPlayer.Type == ePlayerType.Computer)
            {
                Move(getRandomCol());
            }
        }

        private int getRandomCol()
        {
            List<int> availableCols = new List<int>();

            for (int col = 0; col < r_GameSettings.Cols; col++)
            {
                if (r_Matrix[r_GameSettings.Rows - 1, col] == 0) availableCols.Add(col);
            }

            return availableCols[r_Random.Next(0, availableCols.Count)];
        }
    }
}
