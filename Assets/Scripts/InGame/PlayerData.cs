using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Score { get; set;}

    public void Init()
    {
        MaxHp = 3;
        Hp = 2;
        Score = 0;
    }
}
