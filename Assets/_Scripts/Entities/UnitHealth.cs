using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable MaxHealth;
    public float Health;
    public float StartingHealth;
    public bool ResetHealthOnStart;
    public bool StartWithStartingHealth;
    public GameObject DamageEffect;
    
    public UnityEvent DamageEvent;
    public UnityEvent DestructionEvent;
    public bool HasBeenDestructed = false;

    [SerializeField] FloatVariable TimeForAssist;
    private List<PlayerGameData> _lastPlayersWhoDidDamage = new List<PlayerGameData>();

    void Start()
    {
        if(ResetHealthOnStart) Health = MaxHealth.Value;
        if(StartWithStartingHealth) Health = StartingHealth;
    }

    public void TakeDamage(float damage, Vector3 contactPoint, GameObject entityDamageDealer){
        PlayDamageEffect(contactPoint);
        if(Health > 0){
            if(damage > 0){
                Health -= damage;
                if(Health > 0) entityDamageDealer.GetComponentInChildren<HUD>().PlayHitMark();
                StartCoroutine(AddAsisstantForATime(entityDamageDealer.GetComponent<PlayerGameData>(),TimeForAssist.Value));
                GetComponentInChildren<HUD>().PositionDamageEffect(entityDamageDealer.transform.position);
                // GetComponentInChildren<HUD>().PlayDamageEffect();
                // _lastPlayersWhoDidDamage.Add(entityDamageDealer.GetComponent<PlayerGameData>());
                // this.Invoke(
                //     (GameObject entityDamageDealer) => {
                //     if(entityDamageDealer.GetComponent<PlayerGameData>())
                //         _lastPlayersWhoDidDamage.Remove(entityDamageDealer.GetComponent<PlayerGameData>());
                //     },
                //     TimeForAssist.Value
                // );
            }
        }
        DamageEvent.Invoke();
        if(Health <= 0){
            if(!HasBeenDestructed){
                HasBeenDestructed = true;
                Health = 0;
                DestructionEvent.Invoke();
                entityDamageDealer.GetComponentInChildren<HUD>().PlayDeathMark();
                GetComponent<PlayerGameData>().deaths++;
                entityDamageDealer.GetComponent<PlayerGameData>().kills++;
                _lastPlayersWhoDidDamage.Remove(entityDamageDealer.GetComponent<PlayerGameData>());//remove the killer from the list of assistants
                foreach(PlayerGameData assistant in _lastPlayersWhoDidDamage){
                    assistant.assits++;
                }
                StopCoroutine("AddAsisstantForATime");
                _lastPlayersWhoDidDamage.Clear();
                UIManager.Instance.GetComponent<Boards>().OrderPlayers();
            }
        }
    }

    public void ResetHealth(){
        Health = MaxHealth.Value;
    }

    void PlayDamageEffect(Vector3 position){
        GameObject damageEffectInstance = Instantiate(DamageEffect,position,Quaternion.Euler(0,0,0));
        damageEffectInstance.GetComponent<ParticleSystem>().Play();
        this.Invoke(() => Destroy(damageEffectInstance),damageEffectInstance.GetComponent<ParticleSystem>().main.duration);
    }

    private IEnumerator AddAsisstantForATime(PlayerGameData assistant,float time){
        for(int i = 0; i < _lastPlayersWhoDidDamage.Count; i++){
            if(_lastPlayersWhoDidDamage[i].playerName == assistant.playerName)
                _lastPlayersWhoDidDamage.Remove(_lastPlayersWhoDidDamage[i]);
        }
        _lastPlayersWhoDidDamage.Add(assistant);
        yield return new WaitForSeconds(time);
        _lastPlayersWhoDidDamage.Remove(assistant);
    }
}
