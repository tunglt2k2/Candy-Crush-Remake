using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //singleton
    public static SoundManager instance { get; private set; }


    public AudioSource[] destroyNoise;

    private void Awake()
    {
        //Keep this object even when we go to new scence
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Destroy duplicate gameobjects
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void PlayRandomDestroyNoise()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if(PlayerPrefs.GetInt("Sound") == 1)
            {
                int clipToPlay = Random.Range(0, destroyNoise.Length);
                destroyNoise[clipToPlay].Play();
            }
        }
        else
        {
            int clipToPlay = Random.Range(0, destroyNoise.Length);
            destroyNoise[clipToPlay].Play();
        }
       
    }
}
