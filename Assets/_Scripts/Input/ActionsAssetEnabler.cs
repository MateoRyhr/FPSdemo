using UnityEngine.InputSystem;
using UnityEngine;

public class ActionsAssetEnabler : MonoBehaviour
{
    public InputActionAsset ActionsAsset;

    private void OnEnable(){
        ActionsAsset.Enable();
    }

    // private void OnDestroy(){
    //     ActionsAsset.Disable();
    // }
}
