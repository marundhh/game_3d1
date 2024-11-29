using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManeger : MonoBehaviour
{
  
    void Start()
    {
        
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Scene_Demo");
    }
    
}
