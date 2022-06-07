using UnityEngine;

public abstract class DamageDealer : MonoBehaviour
{
    public float DamageAmount;
    public abstract bool TakeDamageCondition(GameObject other);
    // private void Awake() {
    //     _damageTrigger = GetComponent<Collider>();
    // }

    // private void OnTriggerEnter(Collider other) {
    //     if(other.GetComponent<DamageTaker>()){
    //         other.GetComponent<DamageTaker>().TakeDamage(DamageAmount.Value);
    //     }
    // }

    // public void Active(){
    //     _damageTrigger.enabled = true;
    //     _damageTrigger.isTrigger = true;
    // }

    // public void Desactive(){
    //     _damageTrigger.enabled = false;
    //     _damageTrigger.isTrigger = false;
    // }
}
