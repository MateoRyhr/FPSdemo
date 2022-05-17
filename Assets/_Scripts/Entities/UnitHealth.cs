using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable MaxHealth;
    public float Health;
    public float StartingHealth;
    public bool ResetHealthOnStart;
    public bool StartWithStartingHealth;

    // public bool CanTakeDamage { get; set; }

    public GameObject DamageEffect;
    
    public UnityEvent DamageEvent;
    public UnityEvent DestructionEvent;
    public bool HasBeenDestructed = false;

    // Start is called before the first frame update
    void Start()
    {
        if(ResetHealthOnStart) Health = MaxHealth.Value;
        if(StartWithStartingHealth) Health = StartingHealth;
        // CanTakeDamage = true;
    }

    public void TakeDamage(float damage, Vector3 contactPoint){
        // if(CanTakeDamage){
            // CanTakeDamage = false;
            // DamageEffect.position = contactPoint;
            PlayDamageEffect(contactPoint);
            DamageEvent.Invoke();
            if(Health > 0){
                Health -= damage;
            }
            if(Health <= 0){
                if(!HasBeenDestructed){
                    HasBeenDestructed = true;
                    Health = 0;
                    DestructionEvent.Invoke();
                }
            }
            // this.Invoke(() => CanTakeDamage = true,0.005f);
        // }
    }

    public void ResetHealth(){
        Health = MaxHealth.Value;
    }

    void PlayDamageEffect(Vector3 position){
        GameObject damageEffectInstance = Instantiate(DamageEffect,position,Quaternion.Euler(0,0,0));
        damageEffectInstance.GetComponent<ParticleSystem>().Play();
        this.Invoke(() => Destroy(damageEffectInstance),damageEffectInstance.GetComponent<ParticleSystem>().main.duration);
    }
}
