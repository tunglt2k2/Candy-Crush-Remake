using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Board board;
    public Text scoreText;
    public int score;
    public Image scoreBar;
    public Image[] starts;
    public int starsActive;

    private void Start()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
    }
    private void Update()
    {
        scoreText.text = score.ToString();
        for (int i = 0; i < starsActive; i++)
        {
            starts[i].enabled = true;
        }
    }
    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        if(board != null && scoreBar != null)
        {
            int length = board.scoreGoals.Length;
            float _score = Mathf.Clamp(score, 0, board.scoreGoals[length - 1]);
            scoreBar.fillAmount = _score / board.scoreGoals[length - 1];
            for(int i = 0; i < length; i++)
            {
                if (score > board.scoreGoals[i]) starsActive = i + 1;
            }
        }
    }

}
