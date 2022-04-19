namespace FourInARowLogic
{
    public class GameSettings
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set; }
    }
}
