using UnityEngine;
using UnityEngine.Events;

public class DamageOnlyEffect : DamageTaker
{
    public UnityEvent DamageEvent;
    public GameObject DamageEffect;

    public override void TakeDamage(float damage,Vector3 contactPoint,GameObject damageDealer)
    {
        PlayDamageEffect(contactPoint);
        DamageEvent.Invoke();
    }

    void PlayDamageEffect(Vector3 position){
        GameObject damageEffectInstance = Instantiate(DamageEffect,position,Quaternion.Euler(0,0,0));
        damageEffectInstance.GetComponent<ParticleSystem>().Play();
        this.Invoke(() => Destroy(damageEffectInstance),damageEffectInstance.GetComponent<ParticleSystem>().main.duration);
    }
}
