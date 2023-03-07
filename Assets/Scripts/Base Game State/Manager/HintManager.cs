using System.Collections.Generic;
using UnityEngine;

//Find all possible match and choose random one of them to recommend
public class HintManager : MonoBehaviour
{
    private Board board;
    public float hintDelay;
    private float hintDelaySecond;
    public GameObject hintParticle;
    public GameObject currentHint;

    private void Start()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        hintDelaySecond = hintDelay;
    }

    private void Update()
    {
        hintDelaySecond -= Time.deltaTime;
        if (hintDelaySecond <=0 && currentHint == null)
        {
            MarkHint();
            hintDelaySecond = hintDelay;
        }
    }

    List<GameObject> FindAlllMatches()
    {
        List<GameObject> possibleMoves = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                if(board.allDots[i,j] != null)
                {
                    if(i< board.width-1)
                    {
                        if (board.SwitchAndCheck(i, j, Vector2.right))
                            possibleMoves.Add(board.allDots[i, j]);
                    }

                    if (j < board.height-1)
                    {
                        if (board.SwitchAndCheck(i, j, Vector2.up))
                            possibleMoves.Add(board.allDots[i, j]);
                    }
                }
            }
        }
        return possibleMoves;
    }

    GameObject PickOneRandomly()
    {
        List<GameObject> possibleMoves = FindAlllMatches();
        if(possibleMoves.Count > 0)
        {
            int pieceToUse = Random.Range(0, possibleMoves.Count);
            return possibleMoves[pieceToUse];
        }
        return null;
    }

    public void MarkHint()
    {
        GameObject move = PickOneRandomly();
        if(move != null)
        {
            currentHint = Instantiate(hintParticle, move.transform.position, Quaternion.identity);
        }
    }

    public void Destroyhint()
    {
        if(currentHint != null)
        {
            Destroy(currentHint);
            currentHint = null;
            hintDelaySecond = hintDelay;
        }
    }
}
