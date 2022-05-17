using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Weapon",menuName = "Weapon/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType type;
    public GameObject prefab;
    public float damage;
    
}

public enum WeaponType{
    Melee,
    Handgun,
    Rifle,
    Throwable
}
