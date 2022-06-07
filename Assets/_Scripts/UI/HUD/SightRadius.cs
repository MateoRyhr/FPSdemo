using System.Collections;
using UnityEngine;

public class SightRadius : MonoBehaviour
{
    [SerializeField] GameObject _currentWeapon;
    [SerializeField] RectTransform _sightRadiusImage;
    [SerializeField] FloatVariable _sightMinimumRadius;
    [SerializeField] FloatVariable _sightMaximumRadius;
    private float _timeAfterShootToStartRecovery = 0.25f;

    [HideInInspector] public float SightRadiusValue;

    private void Start() {
        SightRadiusValue = _sightMinimumRadius.Value;
    }

    // void Update()
    // {
    //     _sightRadiusImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,SightRadiusValue);
    //     _sightRadiusImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,SightRadiusValue);
    // }

    private void FixedUpdate() {
        _sightRadiusImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,SightRadiusValue);
        _sightRadiusImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,SightRadiusValue);
    }

    public void SightRecoil(){
        RangeWeapon rangeWeapon = _currentWeapon.GetComponentInChildren<RangeWeapon>();
        if(SightRadiusValue < _sightMaximumRadius.Value)
            if(SightRadiusValue + rangeWeapon.RangeWeaponData.recoilForce > _sightMaximumRadius.Value)
                SightRadiusValue = _sightMaximumRadius.Value;
            else SightRadiusValue += rangeWeapon.RangeWeaponData.recoilForce;
    }

    public void SightRecoilRecovery(){
        CancelInvoke();
        StopCoroutine("RecoilRecovery");
        this.Invoke(
            () => StartCoroutine("RecoilRecovery",SightRadiusValue-_sightMinimumRadius.Value),
            _timeAfterShootToStartRecovery
        );
    }

    IEnumerator RecoilRecovery(float radiusToReduce){
        RangeWeapon rangeWeapon = _currentWeapon.GetComponentInChildren<RangeWeapon>();
        SightRadiusValue -= (radiusToReduce / (rangeWeapon.RangeWeaponData.recoilRecoveryTime / Time.deltaTime));
        if(SightRadiusValue < _sightMinimumRadius.Value) SightRadiusValue = _sightMinimumRadius.Value;
        yield return new WaitForSeconds(Time.deltaTime);
        if(SightRadiusValue > _sightMinimumRadius.Value) StartCoroutine("RecoilRecovery",radiusToReduce);
    }
}
