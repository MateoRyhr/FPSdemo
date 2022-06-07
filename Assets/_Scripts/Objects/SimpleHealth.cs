using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleHealth : DamageTaker
{
    [SerializeField] FloatVariable Health;
    GameObject DamageEffect;
    float currentHealth;

    public UnityEvent DamageEvent;
    public UnityEvent DestructionEvent;

    void Start()
    {
        currentHealth = Health.Value;
    }

    void PlayDamageEffect(Vector3 position){
        GameObject damageEffectInstance = Instantiate(DamageEffect,position,Quaternion.Euler(0,0,0));
        damageEffectInstance.GetComponent<ParticleSystem>().Play();
        this.Invoke(() => Destroy(damageEffectInstance),damageEffectInstance.GetComponent<ParticleSystem>().main.duration);
    }

    public override void TakeDamage(float damage, Vector3 contactPoint, GameObject entityDamageDealer)
    {
        if(currentHealth > 0){
            currentHealth -= damage;
            DamageEvent?.Invoke();
        }
        if(currentHealth <= 0){
            currentHealth = 0;
            DestructionEvent?.Invoke();
        }
    }
}
