using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmPanel : MonoBehaviour
{
    [Header("Level Information")]
    public string levelToLoad;
    public int level;
    private int starsActive;
    private GameData gameData;
    private int highScore;

    [Header("UI stuff")]
    public Text headerText;
    public Text highScoreText;
    public Image[] stars;

    private void Start()
    {
        gameData = FindObjectOfType<GameData>();
        Setup();
    }

    public void Setup()
    {
        
        DeactivateStars();
        LoadData();
        ActivateStars();
        SetText();
    }
    void LoadData()
    {
        if(gameData != null)
        {
            starsActive = gameData.saveData.attributes[level - 1].starts;
            highScore = gameData.saveData.attributes[level - 1].highScore;
        }
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    public void Play()
    {
        PlayerPrefs.SetInt("Current Level", level - 1);
        SceneManager.LoadScene(levelToLoad);
    }

    public void LoadLevel(int level)
    {
        this.level = level;    
    }

    void SetText()
    {
        headerText.text = "Level " + level;
        highScoreText.text = ""+ highScore;
    }
    void ActivateStars()
    {
        for (int i = 0; i < starsActive; i++)
        {
            stars[i].enabled = true;
        }
    }

    void DeactivateStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = false;
        }
    }


}
