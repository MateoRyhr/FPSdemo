using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovementInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerActionsAsset;
    InputAction _moveDirectionInput;        //store the reference to the move action
    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private int _actionMapNumber;
    [SerializeField] private int _actionNumber;
    public BoolVariable AreInputsEnabled;
    public BoolVariable ArePlayersFreezed;
    public UnitMovement UnitMovement;

    // Update is called once per frame
    private void Update()
    {
        GetMoveDirection();
    }

    private void FixedUpdate() {
        if(AreInputsEnabled.Value && !ArePlayersFreezed.Value)
            if(_playerStatus.GetCanMove())
                UnitMovement.Move();
    }

    void OnEnable()
    {
        _moveDirectionInput = _playerActionsAsset.actionMaps[_actionMapNumber].actions[_actionNumber];
        _playerActionsAsset.Enable();       //Turn on the action map
    }

    void OnDisable()
    {
        _playerActionsAsset.actionMaps[_actionMapNumber].Disable();       //turn off the action map
    }

    void GetMoveDirection(){
        UnitMovement.Direction = new Vector2(
            _moveDirectionInput.ReadValue<Vector2>().x,
            _moveDirectionInput.ReadValue<Vector2>().y
        );
    }
}
