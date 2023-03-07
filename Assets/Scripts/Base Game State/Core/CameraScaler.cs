using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private Board board;
    public float CameraOffset;
    public float padding;
    public float yOffset;
    void Start()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        if (board != null) 
        {
            RepositonCamera(board.width-1, board.height-1);
        }
    }

     void RepositonCamera(float x, float y)
    {
        Vector3 tempPosition = new Vector3(x/2, y/2+ yOffset, CameraOffset);
        transform.position = tempPosition;
        if(board.width >= board.height)
        {
            Camera.main.orthographicSize = (board.width / 2  + padding) * 1.8f;
        }
        else
        {
            Camera.main.orthographicSize = board.height / 2 + padding*2 ;
        }
    }

}
