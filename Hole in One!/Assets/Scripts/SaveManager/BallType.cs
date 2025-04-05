using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallType : MonoBehaviour
{
    public GameObject[] BallTypes;

    public void Awake()
    {
        ChooseBallType(Save.instance.currentBall);
    }

    public void ChooseBallType(int _index)
    {
        Instantiate(BallTypes[_index], transform.position, Quaternion.identity, transform);
    }
}
