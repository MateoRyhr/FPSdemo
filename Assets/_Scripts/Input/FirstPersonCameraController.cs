using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCameraController : SightController
{
    public Camera Camera;
    public GameObject Player;
    [SerializeField] private int _aimColliderLayerMask;
    [Header("Input")]
    [SerializeField] private InputActionAsset _playerActionsAsset;
    InputAction _lookDirectionInput;        //store the reference to the move action
    [SerializeField] private int _actionMapNumber;
    [SerializeField] private int _actionNumber;
    [Header("Variables")]
    [SerializeField] private FloatVariable _sensivityX;
    [SerializeField] private FloatVariable _sensivityY;
    [SerializeField] private FloatVariable _sensivity;
    [SerializeField] private FloatVariable xRotationAngle;

    Vector2 lookPosition;
    Vector2 lookRotation;

    // [HideInInspector] public Vector3 LookingPoint;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateCamera();
        GetLookingPoint();
    }

    void GetInput(){
        lookPosition = _lookDirectionInput.ReadValue<Vector2>();
        lookRotation = new Vector2(
            lookRotation.x -= lookPosition.y * _sensivityY.Value * _sensivity.Value,
            lookRotation.y += lookPosition.x * _sensivityX.Value * _sensivity.Value
        );
        lookRotation.x = Mathf.Clamp(lookRotation.x,xRotationAngle.Value*-1,xRotationAngle.Value);
    }

    void UpdateCamera(){
        Camera.transform.localRotation = Quaternion.Euler(lookRotation.x,0,0);
        Player.transform.rotation = Quaternion.Euler(0,lookRotation.y,0);
    }

    void OnEnable()
    {
        _lookDirectionInput = _playerActionsAsset.actionMaps[_actionMapNumber].actions[_actionNumber];
        _playerActionsAsset.Enable();       //Turn on the action map
    }

    void OnDisable()
    {
        _playerActionsAsset.actionMaps[_actionMapNumber].Disable();       //turn off the action map
    }

    void GetLookingPoint(){
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit,999f,_aimColliderLayerMask))
            SightPoint = raycastHit.point;
    }
}
