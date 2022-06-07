using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDamageDealer : DamageDealer
{
    [SerializeField] LayerMask _enemyLayer;

    public override bool TakeDamageCondition(GameObject other) => true;

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<DamageTaker>() && other.gameObject.layer == _enemyLayer){
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            other.GetComponent<DamageTaker>().TakeDamage(DamageAmount,contactPoint,gameObject);
        }
    }
}
