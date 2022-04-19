using System;
using System.Windows.Forms;
using System.Drawing;
using FourInARowLogic;
using FourInARowLogic.Enums;

namespace FourInARow
{
    public class GameSettingsForm : Form
    {
        private readonly Label r_LabelPlayers = new Label();
        private readonly Label r_LabelPlayer1 = new Label();
        private readonly Label r_LabelPlayer2 = new Label();
        private readonly Label r_LabelBoardSize = new Label();
        private readonly Label r_LabelBoardSizeRows = new Label();
        private readonly Label r_LabelBoardSizeCols = new Label();

        private readonly TextBox r_TextboxUsername1 = new TextBox();
        private readonly TextBox r_TextboxUsername2 = new TextBox();

        private readonly CheckBox r_CheckBoxIsPlayer = new CheckBox();

        private readonly NumericUpDown r_NumericUpDownRows = new NumericUpDown();
        private readonly NumericUpDown r_NumericUpDownCols = new NumericUpDown();

        private readonly Button r_ButtonStart = new Button();

        public GameSettingsForm()
        {
            this.Size = new Size(250, 270);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            initControls();
        }

        private void initControls()
        {
            this.Controls.AddRange(new Control[] { r_ButtonStart, r_NumericUpDownCols, r_LabelBoardSizeCols, r_NumericUpDownRows, r_LabelBoardSizeRows, r_LabelBoardSize, r_LabelPlayers, r_LabelPlayer1, r_TextboxUsername1, r_LabelPlayer2, r_TextboxUsername2, r_CheckBoxIsPlayer });
            r_LabelPlayers.Text = "Players:";
            r_LabelPlayers.Location = new Point(10, 20);

            r_LabelPlayer1.Text = "Player1:";
            r_LabelPlayer1.Location = new Point(20, 50);
            r_LabelPlayer1.AutoSize = true;

            r_LabelPlayer2.Text = "Player2:";
            r_LabelPlayer2.Location = new Point(36, 81);
            r_LabelPlayer2.AutoSize = true;

            int textboxUsername1 = r_LabelPlayer1.Top + r_LabelPlayer1.Height / 2;
            textboxUsername1 -= r_TextboxUsername1.Height / 2;

            r_TextboxUsername1.Location = new Point(r_LabelPlayer1.Right + 32, textboxUsername1);

            int textboxUsername2 = r_LabelPlayer2.Top + r_LabelPlayer2.Height / 2;
            textboxUsername2 -= r_TextboxUsername2.Height / 2;

            r_TextboxUsername2.Location = new Point(r_LabelPlayer2.Right + 16, textboxUsername2);
            r_TextboxUsername2.Enabled = false;
            r_TextboxUsername2.Text = "[Computer]";

            r_CheckBoxIsPlayer.Location = new Point(20, 77);

            r_LabelBoardSize.Text = "Board Size:";
            r_LabelBoardSize.Location = new Point(10, 130);

            r_LabelBoardSizeRows.Text = "Rows:";
            r_LabelBoardSizeRows.AutoSize = true;
            r_LabelBoardSizeRows.TextAlign = ContentAlignment.MiddleCenter;
            r_LabelBoardSizeRows.Location = new Point(20, 160);

            int numericUpDownRows = r_LabelBoardSizeRows.Top + r_LabelBoardSizeRows.Height / 2;
            numericUpDownRows -= r_LabelBoardSizeRows.Height / 2;

            r_NumericUpDownRows.Location = new Point(r_LabelBoardSizeRows.Right + 8, numericUpDownRows);
            r_NumericUpDownRows.Minimum = 4;
            r_NumericUpDownRows.Maximum = 10;
            r_NumericUpDownRows.Size = new Size(40, 40);

            r_LabelBoardSizeCols.Text = "Cols:";
            r_LabelBoardSizeCols.AutoSize = true;

            int lLabelBoardSizeCols = r_NumericUpDownRows.Top + r_NumericUpDownRows.Height / 2;
            lLabelBoardSizeCols -= r_NumericUpDownRows.Height / 2;

            r_LabelBoardSizeCols.Location = new Point(r_NumericUpDownRows.Right + 30, lLabelBoardSizeCols);
            r_LabelBoardSizeCols.TextAlign = ContentAlignment.MiddleCenter;

            int numericUpDownCols = r_LabelBoardSizeCols.Top + r_LabelBoardSizeCols.Height / 2;
            numericUpDownCols -= r_LabelBoardSizeCols.Height / 2;

            r_NumericUpDownCols.Location = new Point(r_LabelBoardSizeCols.Right + 8, numericUpDownCols);
            r_NumericUpDownCols.Minimum = 4;
            r_NumericUpDownCols.Maximum = 10;
            r_NumericUpDownCols.Size = new Size(40, 40);

            r_ButtonStart.Text = "Start";
            r_ButtonStart.Location = new Point(10, this.ClientSize.Height - r_ButtonStart.Height - 8);
            r_ButtonStart.Size = new Size(Width - 38, 25);

            this.r_CheckBoxIsPlayer.Click += new EventHandler(m_CheckBox_Click);
            this.r_ButtonStart.Click += new EventHandler(m_ButtonStart_Click);
        }

        private void m_ButtonStart_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private string parsePlayerName(string i_PlayerName, string i_DefaultName)
        {
            i_PlayerName = i_PlayerName?.Trim();
            if (!string.IsNullOrEmpty(i_PlayerName)) return i_PlayerName;
            return i_DefaultName;
        }

        public GameSettings Settings
        {
            get
            {
                Player m_Player1 = new Player()
                {
                    Name = parsePlayerName(r_TextboxUsername1.Text, "Player1"),
                    Type = ePlayerType.Player1,
                    Score = 0
                };

                Player m_Player2 = new Player()
                {
                    Name = r_CheckBoxIsPlayer.Checked ? parsePlayerName(r_TextboxUsername2.Text, "Player2") : "Computer",
                    Type = r_CheckBoxIsPlayer.Checked ? ePlayerType.Player2 : ePlayerType.Computer,
                    Score = 0
                };

                return new GameSettings()
                {
                    Rows = (int)r_NumericUpDownRows.Value,
                    Cols = (int)r_NumericUpDownCols.Value,
                    Player1 = m_Player1,
                    Player2 = m_Player2
                };
            }
        }

        private void m_CheckBox_Click(object sender, EventArgs e)
        {
            if (r_TextboxUsername2.Enabled)
            {
                r_TextboxUsername2.Enabled = false;
                r_TextboxUsername2.Text = "[Computer]";
            }
            else
            {
                r_TextboxUsername2.Enabled = true;
                r_TextboxUsername2.Text = string.Empty;
            }
        }
    }
}
