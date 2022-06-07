using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeaponData", menuName = "Weapon/RangeWeaponData")]
public class RangeWeaponData : ScriptableObject
{
    // [Header("Info")]
    // public string weaponName;
    
    [Header("Shooting")]
    public float maxDistance;
    public float velocity;
    public float recoilForce;
    public float fireRate;                      //In Rounds per minute
    [Header("Reloading")] 
    // public int currentAmmo;
    public int magSize;
    public float recoilRecoveryTime;
    public float reloadTime;
    // public bool reloading;
}
