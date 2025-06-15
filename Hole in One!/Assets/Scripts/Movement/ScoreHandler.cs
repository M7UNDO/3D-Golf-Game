using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int score = 0;

    public int Score
    {
        get
        {
            return score;

        }

        set
        {
            score = value;
        }

    }
}