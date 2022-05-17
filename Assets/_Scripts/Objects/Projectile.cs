using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageDealer
{    
    // private void OnTriggerEnter(Collider other) {
    //     if(other.GetComponent<DamageTaker>()){
    //         Vector3 contactPoint = other.ClosestPoint(transform.position);
    //         other.GetComponent<DamageTaker>().TakeDamage(DamageAmount.Value,contactPoint);
    //     }
    //     Destroy(gameObject);
    // }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<DamageTaker>()){
            Vector3 contactPoint = other.collider.ClosestPoint(transform.position);
            other.gameObject.GetComponent<DamageTaker>().TakeDamage(DamageAmount,contactPoint);
        }
        Destroy(gameObject);
    }
}
