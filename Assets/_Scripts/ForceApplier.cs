using UnityEngine;

public class ForceApplier : MonoBehaviour
{
    Collider forceTrigger;
    public FloatVariable ForceAmount;

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<ForceReceiver>()){
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 forceDirection = (contactPoint - transform.position).normalized;
            Vector3 force = forceDirection * ForceAmount.Value;
            other.GetComponent<ForceReceiver>().ReceiveForce(force);
        }
    }
}
