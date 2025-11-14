using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Balls/Ball Database")]
public class BallDatabase : ScriptableObject
{
    public List<BallItem> allBalls;
}
