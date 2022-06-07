using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsState : MonoBehaviour
{
    public InputActionAsset PlayerActionsAsset;
    public BoolVariable AreInputsEnabled;

    private void Start() {
        AreInputsEnabled.Value = true;
    }

    private void Update() {
        SetState(GameManager.Instance.CurrentGameState);
    }

    public void SetState(GameState currentGameState){
        if(currentGameState == GameState.InGame)
            PlayerActionsAsset.Enable();
        else
            PlayerActionsAsset.Disable();
    }

    public void EnableActions(float delay){
        this.Invoke(() => AreInputsEnabled.Value = true,delay);
    }

    public void DisableActions(float delay){
        this.Invoke(() => AreInputsEnabled.Value = false,delay);
    }
}
