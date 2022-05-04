using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : DamageTaker
{
    public FloatVariable MaxHealth;
    private float _health;
    public float StartingHealth;
    public bool ResetHealth;
    public bool StartWithStartingHealth;
    public Transform DamageEffect;

    public UnityEvent DamageEvent;
    public UnityEvent DestructionEvent;

    // Start is called before the first frame update
    void Start()
    {
        if(ResetHealth) _health = MaxHealth.Value;
        if(StartWithStartingHealth) _health = StartingHealth;
        CanTakeDamage = true;
    }

    public override void TakeDamage(float damage,Vector3 contactPoint){
        if(CanTakeDamage){
            CanTakeDamage = false;
            DamageEffect.position = contactPoint;
            DamageEvent.Invoke();
            _health -= damage;
            if(_health <= 0) DestructionEvent.Invoke();
            this.Invoke(() => CanTakeDamage = true,0.05f);
        }
    }
}
