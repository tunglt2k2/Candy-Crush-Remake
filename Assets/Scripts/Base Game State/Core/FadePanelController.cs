using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanelController : MonoBehaviour
{
    public Animator panelAnim;
    public Animator gameInfoAnim;

    private void Start()
    {
        gameInfoAnim.gameObject.SetActive(true);
    }
    public void OK()
    {
        if (panelAnim != null && gameInfoAnim != null)
        {
            panelAnim.SetBool("Out", true);
            panelAnim.gameObject.GetComponent<Image>().enabled = false;
            gameInfoAnim.SetBool("Out", true);
            gameInfoAnim.gameObject.SetActive(false);
            StartCoroutine(GameStateCo());
        }
    }

    public void GameOver()
    {
        panelAnim.gameObject.GetComponent<Image>().enabled = true;
        panelAnim.SetBool("Out", false);
        panelAnim.SetBool("GameOver", true);
    }

    IEnumerator GameStateCo()
    {
        yield return new WaitForSeconds(1f);
        Board board = FindObjectOfType<Board>();
        board.currentState = GameState.move;
    }
}
