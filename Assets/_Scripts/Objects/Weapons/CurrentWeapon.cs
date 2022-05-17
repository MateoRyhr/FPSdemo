using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrentWeapon : MonoBehaviour
{
    public UnityEvent OnShoot;
    public UnityEvent OnReload;

    public void SingleShoot(){
        if(GetComponentInChildren<Gun>())
            GetComponentInChildren<Gun>().Shoot();
    }

    public void AutomaticShoot(){
        Gun gun = GetComponentInChildren<Gun>();
        if(gun){
            if(gun.WeaponData.type == WeaponType.Rifle)
                gun.Shoot();
        }
    }

    public void Reload(){
        if(GetComponentInChildren<Gun>())
            GetComponentInChildren<Gun>().Reload();
    }
}
