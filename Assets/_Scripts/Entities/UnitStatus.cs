using UnityEngine;

public abstract class UnitStatus : MonoBehaviour
{    
    public abstract bool GetIsOnGround();
    public abstract bool GetCanMove();
}
