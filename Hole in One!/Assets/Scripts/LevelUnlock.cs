using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlock : MonoBehaviour
{

    public static LevelUnlock Instance;

    [Header("Level Manager")]
    [Space(5)]

    public bool[] isCompleted = new bool[4] {true, false, false, false};
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
