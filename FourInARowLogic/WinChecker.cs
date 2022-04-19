using FourInARowLogic.Enums;

namespace FourInARowLogic
{
    public class WinChecker
    {
        private readonly int[,] r_Matrix;
        private readonly GameSettings r_GameSetting;
        public WinChecker(GameSettings i_GameSetting, int[,] i_Matrix)
        {
            r_Matrix = i_Matrix;
            r_GameSetting = i_GameSetting;
        }

        public bool IsWin(int i_Row, int i_Col)
        {
            return isWinRow(i_Row, i_Col) || isWinCol(i_Row, i_Col) || isWinLtrDiag(i_Row, i_Col) || isWinRtlDiag(i_Row, i_Col);
        }

        private bool isWinRtlDiag(int i_Row, int i_Col)
        {
            return isWinDirections(i_Row, i_Col, eDirection.UpLeft, eDirection.DownRight);
        }

        private bool isWinLtrDiag(int i_Row, int i_Col)
        {
            return isWinDirections(i_Row, i_Col, eDirection.DownLeft, eDirection.UpRight);
        }

        private bool isWinCol(int i_Row, int i_Col)
        {
            return isWinDirections(i_Row, i_Col, eDirection.Down, eDirection.Up);
        }

        private bool isWinRow(int i_Row, int i_Col)
        {
            return isWinDirections(i_Row, i_Col, eDirection.Left, eDirection.Right);
        }

        private bool isWinDirections(int i_Row, int i_Col, eDirection i_DirectionA, eDirection i_DirectionB)
        {
            int coinsCount = getCoinsCount(i_Row, i_Col, i_DirectionA);
            return getCoinsCount(i_Row, i_Col, i_DirectionB, coinsCount) == 3;
        }

        private int getCoinsCount(int i_Row, int i_Col, eDirection i_Direction, int i_CurrentCount = 0)
        {
            if (i_CurrentCount == 3) return i_CurrentCount;

            switch (i_Direction)
            {
                case eDirection.Up:
                case eDirection.UpLeft:
                case eDirection.UpRight:
                    i_Row--;
                    break;
                case eDirection.Down:
                case eDirection.DownLeft:
                case eDirection.DownRight:
                    i_Row++;
                    break;
            }

            switch (i_Direction)
            {
                case eDirection.Left:
                case eDirection.DownLeft:
                case eDirection.UpLeft:
                    i_Col--;
                    break;
                case eDirection.Right:
                case eDirection.DownRight:
                case eDirection.UpRight:
                    i_Col++;
                    break;
            }

            if (isSameCoin(i_Row, i_Col))
            {
                return getCoinsCount(i_Row, i_Col, i_Direction, i_CurrentCount + 1);
            }

            return i_CurrentCount;
        }

        private bool isSameCoin(int i_Row, int i_Col)
        {
            if (i_Row >= r_GameSetting.Rows || i_Row < 0 || i_Col < 0 || i_Col >= r_GameSetting.Cols)
            {
                return false;
            }

            return r_Matrix[i_Row, i_Col] == (int)r_GameSetting.CurrentPlayer.Type;
        }
    }
}
