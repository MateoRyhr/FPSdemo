using UnityEngine;

public class UnitJump : Action
{
    public FloatVariable JumpForce;
    public UnitStatus UnitStatus;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if(UnitStatus.GetIsOnGround()){
            rb.AddForce(Vector3.up * JumpForce.Value,ForceMode.Impulse);
            OnAction.Invoke();
        }
    }
}
