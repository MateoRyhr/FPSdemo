using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    bool CanReceiveForce;
    public FloatVariable ForceMultiplier;
    Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        CanReceiveForce = true;
    }

    public void ReceiveForce(Vector3 force){
        // Debug.Log("ForceReceived");
        if(CanReceiveForce){
            _rb.AddForce(force * ForceMultiplier.Value,ForceMode.Impulse);
            CanReceiveForce = false;
            this.Invoke(() => CanReceiveForce = true,0.1f);
        }
    }
}
