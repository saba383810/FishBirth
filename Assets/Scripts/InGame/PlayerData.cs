
namespace InGame
{
    public class PlayerData
    {
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Score { get; set;}

        public void Init()
        {
            MaxHp = 3;
            Hp = 3;
            Score = 0;
        }
    }
}
