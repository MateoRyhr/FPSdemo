using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeaponAnimation : MonoBehaviour
{
    [SerializeField] GameObject _currentWeaponContainer;
    Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();
        UpdateWeaponAnimation();
    }

    public void UpdateWeaponAnimation(){
        if(_currentWeaponContainer.GetComponentInChildren<Weapon>()){
            int currentWeaponType = (int)_currentWeaponContainer.GetComponentInChildren<Weapon>().WeaponData.type;
            _animator.SetInteger("weaponType",currentWeaponType);
        }
    }
}
