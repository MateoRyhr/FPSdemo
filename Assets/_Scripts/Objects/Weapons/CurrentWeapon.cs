using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrentWeapon : MonoBehaviour
{
    public SightRadius SightRadius;
    public UnityEvent OnShoot;
    public UnityEvent OnReloadStart;
    public UnityEvent OnReloadFinish;

    public void SingleShoot(){
        if(GetComponentInChildren<RangeWeapon>()){
            GetComponentInChildren<RangeWeapon>().Shoot();
            // OnShoot?.Invoke();
        }
    }

    public void AutomaticShoot(){
        RangeWeapon gun = GetComponentInChildren<RangeWeapon>();
        if(gun){
            if(gun.WeaponData.type == WeaponType.Rifle){
                gun.Shoot();
                // OnShoot?.Invoke();
            }
        }
    }

    public void Reload(){
        if(GetComponentInChildren<RangeWeapon>()){
            GetComponentInChildren<RangeWeapon>().Reload();
            // OnReload?.Invoke();
        }
    }
}
