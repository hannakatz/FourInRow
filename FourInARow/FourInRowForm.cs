using System;
using FourInARowLogic;
using FourInARowLogic.Enums;
using System.Windows.Forms;
using System.Drawing;

namespace FourInARow
{
    public class FourInRowForm : Form
    {
        private readonly Control[,] r_DisplayMatrix;
        private readonly Control[] r_DisplayCols;
        private readonly GameSettings r_GameSettings;
        private readonly Board r_Board;

        private readonly Label r_LabelPlayer1 = new Label();
        private readonly Label r_LabelPlayer2 = new Label();

        public FourInRowForm(GameSettings i_GameSetting)
        {
            r_GameSettings = i_GameSetting;
            r_Board = new Board(r_GameSettings);

            r_Board.OnPlayerMoved += playerMoved;
            r_Board.OnBlockedCol += colBlocked;
            r_Board.OnGameStatusChanged += gameStatusChanged;

            r_DisplayMatrix = new Control[r_GameSettings.Rows, r_GameSettings.Cols];
            r_DisplayCols = new Control[r_GameSettings.Cols];
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Text = "4 in a Row !!";

            StartPosition = FormStartPosition.CenterScreen;
            SizeGripStyle = SizeGripStyle.Hide;
            MaximizeBox = false;

            initControls();
        }

        private void initControls()
        {
            FlowLayoutPanel flowPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown
            };

            Controls.Add(flowPanel);

            Panel rowsPanel = new FlowLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            Controls.Add(rowsPanel);

            for (int col = 0; col < r_GameSettings.Cols; col++)
            {
                Button playButton = new Button
                {
                    Text = string.Format("{0}", col + 1),
                    Width = 50,
                    Height = 25,
                    Margin = new Padding(5),
                };
                int currCol = col;
                playButton.Click += (sender, args) => r_Board.Move(currCol);
                r_DisplayCols[currCol] = playButton;
                rowsPanel.Controls.Add(playButton);
            }

            flowPanel.Controls.Add(rowsPanel);

            for (int row = 0; row < r_GameSettings.Rows; row++)
            {
                Panel buttonsPanel = new FlowLayoutPanel()
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink
                };

                this.Controls.Add(buttonsPanel);

                for (int cols = 0; cols < r_GameSettings.Cols; cols++)
                {
                    Button button = new Button
                    {
                        Width = 50,
                        Height = 50,
                        Margin = new Padding(5)
                    };

                    buttonsPanel.Controls.Add(button);
                    r_DisplayMatrix[row, cols] = button;
                }

                flowPanel.Controls.Add(buttonsPanel);
            }

            Panel bottomPanel = new FlowLayoutPanel() { Height = 20, FlowDirection = FlowDirection.LeftToRight };

            flowPanel.Controls.Add(bottomPanel);

            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Controls.Add(r_LabelPlayer1);
            bottomPanel.Controls.Add(r_LabelPlayer2);

            r_LabelPlayer1.Text = r_GameSettings.Player1.ToString();
            r_LabelPlayer1.Width = bottomPanel.Width / 2 - 6;

            r_LabelPlayer1.TextAlign = ContentAlignment.MiddleCenter;
            r_LabelPlayer2.Text = r_GameSettings.Player2.ToString();
            r_LabelPlayer2.Width = r_LabelPlayer1.Width;
            r_LabelPlayer2.TextAlign = ContentAlignment.MiddleCenter;

        }

        private void playerMoved(int i_Row, int i_Col)
        {
            r_DisplayMatrix[r_GameSettings.Rows - i_Row - 1, i_Col].Text = r_GameSettings.CurrentPlayer.Type == ePlayerType.Player1 ? "X" : "O";
        }

        private void colBlocked(int i_Col)
        {
            r_DisplayCols[i_Col].Enabled = false;
        }

        private void gameStatusChanged(eGameStatus i_GameStatus)
        {
            DialogResult anotherRound;

            if (i_GameStatus == eGameStatus.Win)
            {
                r_GameSettings.CurrentPlayer.Score++;
                anotherRound = askAnotherRound(string.Format("{0} Won !!", r_GameSettings.CurrentPlayer.Name), "A Win!");
            }
            else
            {
                anotherRound = askAnotherRound("Tie !!", "A Tie!");
            }

            if (anotherRound == DialogResult.Yes)
            {
                startRound();
            }
            else
            {
                Close();
            }
        }

        private DialogResult askAnotherRound(string i_Text, string i_Caption)
        {
            return MessageBox.Show(string.Format("{0}{1}Another Round?", i_Text, Environment.NewLine), i_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private void startRound()
        {
            for (int row = 0; row < r_GameSettings.Rows; row++)
            {
                for (int col = 0; col < r_GameSettings.Cols; col++)
                {
                    r_DisplayMatrix[row, col].Text = string.Empty;
                }
            }

            foreach (Control btn in r_DisplayCols)
            {
                btn.Enabled = true;
            }

            r_LabelPlayer1.Text = r_GameSettings.Player1.ToString();
            r_LabelPlayer2.Text = r_GameSettings.Player2.ToString();
            r_Board.Reset();
        }
    }
}
