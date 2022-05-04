using UnityEngine;

public abstract class UnitStatus : MonoBehaviour
{
    int _currentAttack;
    public int CurrentAttack{
        get {
            return _currentAttack;
        }
        set{
            _currentAttack = value;
        }
    }

    // int _attackInProgress;
    // public int AttackInProgress{
    //     get {
    //         return _attackInProgress;
    //     }
    //     set{
    //         _attackInProgress = value;
    //     }
    // }
    
    public abstract bool GetIsOnGround();
    public abstract bool GetCanMove();
    public abstract bool GetCanTurn();
}
