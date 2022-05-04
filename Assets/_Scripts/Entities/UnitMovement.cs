using UnityEngine;

public abstract class UnitMovement : MonoBehaviour
{
    private Vector2 _direction;
    public Vector2 Direction {
        get{
            return _direction;
        }
        set{
            _direction = value;
        }    
    }
    public abstract void Move();
    public abstract void Turn();
    public abstract void Turn(float turnSmoothTime);
}
