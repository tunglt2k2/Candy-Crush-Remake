using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
   
    private void Start()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Splash");
    }

}
