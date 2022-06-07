using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameAction : MonoBehaviour
{
    public void PauseGame(){
        if(GameManager.Instance.CurrentGameState == GameState.InGame){
            GameManager.Instance.UpdateState(GameState.PauseMenu);
            GameManager.Instance.UnlockMouse();
            UIManager.Instance.EnablePauseMenu(0f);
            // UIManager.Instance.DisableHUD(0f);
            return;
        }
        if(GameManager.Instance.CurrentGameState == GameState.PauseMenu){
            GameManager.Instance.UpdateState(GameState.InGame);
            GameManager.Instance.LockMouse();
            UIManager.Instance.DisablePauseMenu(0f);
            // UIManager.Instance.EnableHUD(0f);
            return;
        }
    }

}
