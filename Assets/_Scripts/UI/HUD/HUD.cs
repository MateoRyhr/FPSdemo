using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private UnitHealth UnitHealth;
    [SerializeField] private GameObject CurrentWeapon;
    [SerializeField] private RectTransform hudContainer;
    [SerializeField] private GameObject DamageIndicator;
    [SerializeField] private float DamageIndicatorEffectTime;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _bulletsText;
    [SerializeField] private Animator _hitMarkAnimator;
    [SerializeField] private Animator _deathMarkAnimator;

    private void Start() {
        this.Invoke(() => UpdateHealth(),0.1f);
        this.Invoke(() => UpdateAmmo(),0.1f);
    }

    public void UpdateHealth(){
        _healthText.text = UnitHealth.Health.ToString();
    }

    public void UpdateAmmo(){
        if(CurrentWeapon.GetComponentInChildren<RangeWeapon>()){
            RangeWeapon rangeWeapon = CurrentWeapon.GetComponentInChildren<RangeWeapon>();
            _bulletsText.text =
                $"{rangeWeapon.CurrentAmmo}/{rangeWeapon.RangeWeaponData.magSize}";
        }else{
            _bulletsText.text = "-";
        }
    }

    public void PlayHitMark(){
        _hitMarkAnimator.SetTrigger("enable");
    }

    public void PlayDeathMark(){
        _deathMarkAnimator.SetTrigger("enable");
    }

    public void PositionDamageEffect(Vector3 enemyPosition){
        //The Z-axis rotation of the effect will be equal to the Cos and Sin values obtained from the damageImpactPoint
        Vector3 enemyDirectionLocal = UnitHealth.transform.InverseTransformDirection((enemyPosition - UnitHealth.transform.position).normalized);
        float enemyDirectionAngle = Mathf.Atan2(enemyDirectionLocal.z,enemyDirectionLocal.x) * Mathf.Rad2Deg;
        GameObject damageIndicator = Instantiate(DamageIndicator,hudContainer);
        damageIndicator.transform.Rotate(new Vector3(0f,0f,enemyDirectionAngle));
        this.Invoke(() => Destroy(damageIndicator),DamageIndicatorEffectTime);

        // float effectSize = DamageIndicatorEffect.rect.height;
        // float distanceFromCenter =
        //     (Screen.width/2 * damageDirectionOnHUD.x) +
        //     (Screen.height/2 * damageDirectionOnHUD.y) - effectSize/6;

        // DamageIndicatorEffect.anchoredPosition = new Vector2(0,distanceFromCenter);
    }

    // public void PlayDamageEffect(){
    //     _damageIndicatorAnimator.SetTrigger("enable");
    // }
}
