using UnityEngine;

public abstract class DamageTaker : MonoBehaviour
{
    public bool CanTakeDamage { get; set; }
    public abstract void TakeDamage(float damage,Vector3 contactPoint, GameObject entityDamageDealer);
}
