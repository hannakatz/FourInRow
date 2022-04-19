namespace FourInARow
{
    static class Program
    {
        static void Main()
        {
            GameSettingsForm gameSettingsFrom = new GameSettingsForm();
            if (gameSettingsFrom.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FourInRowForm fourInRowForm = new FourInRowForm(gameSettingsFrom.Settings);
                fourInRowForm.ShowDialog();
            }
        }
    }
}
