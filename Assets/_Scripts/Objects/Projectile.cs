using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamageDealer
{    
    public PlayerGameData playerGameData;

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
            if(TakeDamageCondition(other.gameObject))
                other.gameObject.GetComponent<DamageTaker>().TakeDamage(DamageAmount,contactPoint,playerGameData.gameObject);
            else
                other.gameObject.GetComponent<DamageTaker>().TakeDamage(0f,contactPoint,playerGameData.gameObject);
        }
        Destroy(gameObject);
        // this.Invoke(() => Destroy(gameObject),0.02f);
    }

    public override bool TakeDamageCondition(GameObject other){
        if(other.GetComponentInParent<PlayerGameData>()){
            return playerGameData.team != other.GetComponentInParent<PlayerGameData>().team;
        }
        return false;
    }
}
