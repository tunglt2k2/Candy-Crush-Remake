using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [Header("Board Variables")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;

    private EndGameManager endGameManager;
    private HintManager hintManager;
    private FindMatches findMatches;
    private Board board;
    public GameObject otherDot;
    private Vector2 firstTouchPosition = Vector2.zero;
    private Vector2 finalTouchPosition = Vector2.zero;
    private Vector2 tempPositon;

    [Header("Swipe Stuff")]
    public float swipeAngle = 0;
    public float swipeResist = 0.5f;

    [Header("Power Stuff")]
    public bool isColorBomb;
    public bool isColumnBomb;
    public bool isRowBomb;
    public bool isAdjacentBomb;

    [Header("Special Bomb")]
    public Sprite rowBomb;
    public Sprite columnBomb;
    public Sprite adjacentBomb;
    public Sprite colorBomb;

    public GameObject destroyEff;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        isColumnBomb = false;
        isRowBomb = false;
        isColorBomb = false;
        isAdjacentBomb = false;

        endGameManager = FindObjectOfType<EndGameManager>();
        hintManager = FindObjectOfType<HintManager>();
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        findMatches = FindObjectOfType<FindMatches>();
    }   
   
    private void Update()
    {
        targetX = column;
        targetY = row;
        if(Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //Move towards the target
            tempPositon = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPositon, .4f);
            if(board.allDots[column,row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
                StartCoroutine(FindMatchCo());
            }
        }
        else
        {
            //Directly set the position
            tempPositon = new Vector2(targetX, transform.position.y);
            transform.position = tempPositon;            
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Move towards the target
            tempPositon = new Vector2(transform.position.x,targetY);
            transform.position = Vector2.Lerp(transform.position, tempPositon, .4f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
                StartCoroutine(FindMatchCo());
            }
        }
        else
        {
            //Directly set the position
            tempPositon = new Vector2(transform.position.x, targetY);
            transform.position = tempPositon;
        }
    }

    public IEnumerator FindMatchCo()
    {
        yield return null;
        findMatches.FindAllMatches();
        StopCoroutine(FindMatchCo());
    }

    public IEnumerator CheckMoveCo()
    {
        if (isColorBomb)
        {
            //This piece is a color bomb, and the other piece is the color to destroy       
            findMatches.MatchPieceOfColor(otherDot.tag);
            isMatched = true;
        }
        else if(otherDot.GetComponent<Dot>().isColorBomb)
        {
            //The other piece is a color bomb, and this piece has the color to destroy
            findMatches.MatchPieceOfColor(this.gameObject.tag);
            otherDot.GetComponent<Dot>().isMatched = true;
        }
        yield return new WaitForSeconds(.5f);
        if(otherDot != null)
        {
            if (!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentDot = null;
                board.currentState = GameState.move;
            }
            else
            {
                if(endGameManager != null)
                {
                    if(endGameManager.requirements.gameType == GameType.Moves)
                    {
                        endGameManager.DecreaseCounterValue();
                    }
                }
                board.DestroyMatches();         
            }
        }
    }

    private void OnMouseDown()
    {
        //Destroy the hint
        if (hintManager != null)
        {
            hintManager.Destroyhint();
        } 

        if(board.currentState == GameState.move)
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CaculateAngle();
        }
    }


    void CaculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            board.currentState = GameState.wait;
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePiece();
            board.currentDot = this;
        }
        else
            board.currentState = GameState.move;
    }

    void MovePieceActual(Vector2 direction)
    {
        otherDot = board.allDots[column + (int)direction.x, row + (int)direction.y];
        previousRow = row;
        previousColumn = column;
        if (otherDot && !board.lockTiles[column, row] && board.lockTiles[column + (int)direction.x, row + (int)direction.y] == null)
        {
            otherDot.GetComponent<Dot>().column -= (int)direction.x;
            otherDot.GetComponent<Dot>().row -= (int)direction.y;
            column += (int)direction.x;
            row += (int)direction.y;
            StartCoroutine(CheckMoveCo());
        }
        else
            board.currentState = GameState.move;
    }

    void MovePiece()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            //Right Swipe
            MovePieceActual(Vector2.right);
        }
        else if (swipeAngle > 45 && swipeAngle < 135 && row < board.height - 1)
        {
            //Up Swipe
            MovePieceActual(Vector2.up);
        }
        else if ((swipeAngle >= 135 || swipeAngle < -135) && column > 0)
        {
            //Left Swipe
            MovePieceActual(Vector2.left);
        }
        else if (swipeAngle <= -45 && swipeAngle >= -135 && row > 0)
        {
            //Down Swipe
            MovePieceActual(Vector2.down);
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    public void MakeRowBomb()
    {
        if (!isColumnBomb && !isColorBomb && !isAdjacentBomb && !isRowBomb)
        {
            isRowBomb = true;
            spriteRenderer.sprite = rowBomb;
        }
            
    }

    public void MakeColumnBomb()
    {
        if (!isColumnBomb && !isColorBomb && !isAdjacentBomb && !isRowBomb)
        {
            isColumnBomb = true;
            spriteRenderer.sprite = columnBomb;
        }
    }

    public void MakeColorBomb()
    {
        if (!isColumnBomb && !isColorBomb && !isAdjacentBomb && !isRowBomb)
        {
            isColorBomb = true;
            spriteRenderer.sprite = colorBomb;
            this.gameObject.tag = "Color";
        }
    }

    public void MakeAdjacentBomb()
    {
        if (!isColumnBomb && !isColorBomb && !isAdjacentBomb && !isRowBomb)
        {
            isAdjacentBomb = true;
            spriteRenderer.sprite = adjacentBomb;
        }
    }
}
