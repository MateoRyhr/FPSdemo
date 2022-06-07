using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    bool CanReceiveForce;
    public FloatVariable ForceMultiplier;
    public FloatVariable TorqueForceMultiplier;
    [SerializeField] float applyTorqueAfterTime = 0f;
    Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        CanReceiveForce = true;
    }

    public void ReceiveForce(Vector3 force,Vector3 contactPoint){
        if(CanReceiveForce){
            if(ForceMultiplier) _rb.AddForceAtPosition(force * ForceMultiplier.Value,contactPoint,ForceMode.Impulse);
            else _rb.AddForceAtPosition(force,contactPoint,ForceMode.Impulse);
            CanReceiveForce = false;
            this.Invoke(() => CanReceiveForce = true,0.1f);
        }
    }
}
