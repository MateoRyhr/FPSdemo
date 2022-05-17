using UnityEngine;

public class AnimationMovement : MonoBehaviour
{   
    Animator _animator;
    Rigidbody _rb;
    public FloatVariable MaxSpeed;

    //Animator parameters names
    const string SPEED = "speed";
    // const string VEL_Y = "yVel";

    void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateMoveAnimation();
    }

    void UpdateMoveAnimation()
    {
        // float magnitudeXZ = Mathf.Sqrt(Mathf.Pow(rigidBody.velocity.x,2)+Mathf.Pow(rigidBody.velocity.z,2));
        _animator.SetFloat(SPEED,_rb.velocity.magnitude / (MaxSpeed.Value*1f));
        // _animator.SetFloat(VEL_Y,_rb.velocity.y);
    }
}
