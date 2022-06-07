using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitDamageTaker : DamageTaker
{
    // public Transform DamageEffect;
    [SerializeField] UnitHealth _unitHealth;
    [SerializeField] FloatVariable _damageMultiplier;

    // public UnityEvent DamageEvent;
    // public UnityEvent DestructionEvent;

    // Start is called before the first frame update
    // void Start()
    // {
    //     CanTakeDamage = true;
    // }

    public override void TakeDamage(float damage,Vector3 contactPoint, GameObject entityDamageDealer){
        // if(CanTakeDamage){
            // CanTakeDamage = false;
            // DamageEffect.position = contactPoint;
            // DamageEvent.Invoke();
            _unitHealth.TakeDamage(damage * _damageMultiplier.Value,contactPoint,entityDamageDealer);
            // _health -= damage;
            // if(_health <= 0) DestructionEvent.Invoke();
            // this.Invoke(() => CanTakeDamage = true,0.01f);
        // }
    }
}
