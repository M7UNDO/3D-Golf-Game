using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int currentBallIndex = 0;
    public int Coins = 0;
    public List<string> purchasedBallIDs = new List<string>();
}
