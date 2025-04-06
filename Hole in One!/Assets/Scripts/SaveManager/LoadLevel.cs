using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    // Start is called before the first frame update
   public void LoadLevelNumber(int _index)
   {
        Time.timeScale = 1;
       SceneManager.LoadScene(_index);
   }
}
