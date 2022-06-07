using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBoardsAction : MonoBehaviour
{
    public void ShowBoards(){
        if(GameManager.Instance.CurrentGameState == GameState.InGame)
            UIManager.Instance.EnableBoards(0f);
    }

    public void HideBoards(){
        UIManager.Instance.DisableBoards(0f);
    }
}
