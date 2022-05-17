using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ButtonPressedInput : MonoBehaviour
{
    public InputActionAsset PlayerActionsAsset;
    public int ActionMapNumber;
    public int ActionNumber;
    public BoolVariable InputsEnabled;
    public UnityEvent ButtonPressed;
    public UnityEvent ButtonUp;

    private void Update() {
        if(InputsEnabled.Value && PlayerActionsAsset.actionMaps[ActionMapNumber].actions[ActionNumber].IsPressed())
            ButtonPressed.Invoke();
        else{
            ButtonUp.Invoke();
        }
    }
}
