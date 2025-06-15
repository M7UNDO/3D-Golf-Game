using System.Collections.Generic;
using UnityEngine;

public class BallType : MonoBehaviour
{
    public List<Material> AllBallPrefabs; // Matches order of Save.ballsUnlocked
    public GameObject playerBall;
    private Renderer ballRender;

    void Awake()
    {
        
        ballRender = playerBall.GetComponent<Renderer>();
        ChooseBallType(Save.instance.currentBall);

    }

    public void ChooseBallType(int _index)
    {
        if (_index >= 0 && _index < AllBallPrefabs.Count && Save.instance.ballsUnlocked[_index])
        {
            ballRender.material = AllBallPrefabs[_index];
            //Instantiate(playerBall, transform.position, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Invalid or locked ball index: " + _index);
        }
    }
}