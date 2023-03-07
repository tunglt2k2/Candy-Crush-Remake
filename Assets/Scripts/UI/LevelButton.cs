using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Active Stuff")]
    public bool isActive;
    public bool isPassed;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    public Sprite playedSprite; 
    private Button myButton;
    private Image buttonImage;
    private int starsActive;

    [Header("Level UI")]
    public Image[] starsGray;
    public Image[] starsYellow;
    public Text levelText;
    public int level;
    public GameObject confirmPanel;

    private GameData gameData;
    private void Start()
    {
        gameData = FindObjectOfType<GameData>();
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        levelText.text = level.ToString();
        LoadData();
        DecideSprite();
    }
    
    void LoadData()
    {
        if(gameData != null)
        {
            isActive = gameData.saveData.attributes[level - 1].isActive;
            isPassed = gameData.saveData.attributes[level - 1].isPassed;
            starsActive = gameData.saveData.attributes[level - 1].starts;
            starsActive = Mathf.Clamp(starsActive, 0, 3);
        }
    }
    void DecideSprite()
    {
        if (isActive)
        {
            if (isPassed)
            {
                buttonImage.sprite = playedSprite;
                ActivateStars();
            }
            else
            {
                buttonImage.sprite = activeSprite;
            }          
            myButton.enabled = true;
            levelText.enabled = true;
        }
        else
        {
            buttonImage.sprite = lockedSprite;
            myButton.enabled = false;
            levelText.enabled = false;
        }
    }

   
    public void ConfirmPanel()
    {
        ConfirmPanel panel = confirmPanel.GetComponent<ConfirmPanel>();
        if (panel != null) 
        {
            panel.LoadLevel(this.level);
            panel.Setup();
        }
        confirmPanel.SetActive(true);
    }
    void ActivateStars()
    {
        for (int i = 0; i < starsGray.Length; i++)
        {
            starsGray[i].enabled = true;

        }
        for (int i = 0; i < starsActive ; i++)
        {
            starsYellow[i].enabled = true;
        }
    }
}
