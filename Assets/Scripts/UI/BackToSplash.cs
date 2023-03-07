using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSplash : MonoBehaviour
{
    public string sceneToLoad;
    private GameData gameData;
    private Board board;
    private ScoreManager scoreManager;
    public void WinOK()
    {
        if(gameData != null)
        {
            UpdateData();
            gameData.Save();
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    void UpdateData()
    {
        gameData.saveData.attributes[board.level + 1].isActive = true;
        gameData.saveData.attributes[board.level].isPassed = true;

        int highscore = gameData.saveData.attributes[board.level].highScore;
        gameData.saveData.attributes[board.level].highScore = Mathf.Max(highscore, scoreManager.score);

        int starsActive = gameData.saveData.attributes[board.level].starts;
        gameData.saveData.attributes[board.level].starts = Mathf.Max(starsActive, scoreManager.starsActive);
    }

    public void LoseOK()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void Start()
    {
        gameData = FindObjectOfType<GameData>();
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

}
