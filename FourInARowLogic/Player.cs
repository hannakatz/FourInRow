using FourInARowLogic.Enums;

namespace FourInARowLogic
{
    public class Player
    {
        public string Name { get; set; }
        public ePlayerType Type { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            if (Type == ePlayerType.Computer)
            {
                return string.Format("Computer: {0}", Score);
            }

            return string.Format("{0}: {1}", Name, Score);
        }
    }
}
