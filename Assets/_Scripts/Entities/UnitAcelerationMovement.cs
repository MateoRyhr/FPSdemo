using UnityEngine;

public class UnitAcelerationMovement : UnitMovement
{
    //Movement data needed
    public FloatVariable MaxSpeed;
    public FloatVariable MovementForce;
    // public Vector2 MovementDirection;
    public FloatVariable TurnSmoothTime;
    float turnSmoothVelocity;
    Vector3 _forceDirection;

    //Components
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override void Move()
    {
        _forceDirection = new Vector3(Direction.normalized.x,0f,Direction.normalized.y) * MovementForce.Value;
        _forceDirection = transform.TransformDirection(_forceDirection);        //world to local
        _rb.AddForce(_forceDirection,ForceMode.Impulse);
        if(_forceDirection == Vector3.zero){
            _rb.velocity = new Vector3(0f,_rb.velocity.y,0f);
        }
        _forceDirection = Vector3.zero;
        ClampVelocity();

    }

    void ClampVelocity()
    {
        Vector3 horizontalVelocity = _rb.velocity;
        horizontalVelocity.y = 0;
        if(horizontalVelocity.sqrMagnitude > MaxSpeed.Value * MaxSpeed.Value)                  //a * a, is faster than, Mathf.pow(a,2)
            _rb.velocity = horizontalVelocity.normalized * MaxSpeed.Value + Vector3.up * _rb.velocity.y;
    }

    public override void Turn(float turnSmoothTime)
    {
        float targetAngle = Mathf.Atan2(Direction.normalized.x,Direction.normalized.y) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f,smoothAngle,0f);
    }

    public override void Turn()
    {
        float targetAngle = Mathf.Atan2(Direction.normalized.x,Direction.normalized.y) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,TurnSmoothTime.Value);
        transform.rotation = Quaternion.Euler(0f,smoothAngle,0f);
    }
}
