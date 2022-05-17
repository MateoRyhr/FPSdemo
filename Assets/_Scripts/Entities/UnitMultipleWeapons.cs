using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitMultipleWeapons : MonoBehaviour, ICollector
{
    // [SerializeField] int _weaponQuantity;
    [SerializeField] int _weaponLayer;
    [SerializeField] List<GameObject> _equippedWeapons;
    [SerializeField] int _currentWeaponType;
    [SerializeField] GameObject _equippedWeaponsContainer;
    public GameObject CurrentWeaponContainer;
    [SerializeField] int[] _weaponPriority;
    [SerializeField] float dropWeaponForce;

    public UnityEvent OnWeaponChange;
    public UnityEvent OnDropWeapon;

    public void Collect(Collectable collectableObject)
    {
        if(collectableObject.GetComponent<Weapon>()){
            WeaponType weaponType = collectableObject.GetComponent<Weapon>().WeaponData.type;

            for(int i=0;i<_equippedWeapons.Count;i++){
                if(_equippedWeapons[i] != null)
                    if(_equippedWeapons[i].GetComponent<Weapon>().WeaponData.type == weaponType){
                        return;
                    }
            }
            collectableObject.transform.SetParent(_equippedWeaponsContainer.transform);
            collectableObject.transform.position = Vector3.zero;
            collectableObject.transform.rotation = Quaternion.Euler(0,0,0);
            collectableObject.transform.localPosition = collectableObject.GetComponent<Weapon>().WeaponData.prefab.transform.position;
            collectableObject.transform.localRotation = collectableObject.GetComponent<Weapon>().WeaponData.prefab.transform.rotation;

            _equippedWeapons.Add(collectableObject.gameObject);

            SetWeaponEquiped(collectableObject.gameObject);
            collectableObject.gameObject.SetActive(false);
            EquipBestWeapon(_weaponPriority);
        }
    }

    public void ChangeWeapon(int newWeaponType)
    {
        for (int i = 0; i < _equippedWeaponsContainer.transform.childCount; i++){
            Weapon weapon = _equippedWeaponsContainer.transform.GetChild(i).GetComponent<Weapon>();
            if((int)weapon.WeaponData.type == newWeaponType){
                if(HasWeaponInHand()){
                    GameObject weaponToChange = CurrentWeaponContainer.GetComponentInChildren<Weapon>().gameObject;
                    if(weaponToChange.GetComponent<Gun>()._reloading) weaponToChange.GetComponent<Gun>()._reloading = false;
                    weaponToChange.transform.SetParent(_equippedWeaponsContainer.transform);
                    weaponToChange.SetActive(false);
                }
                weapon.gameObject.SetActive(true);
                weapon.transform.SetParent(CurrentWeaponContainer.transform);
                _currentWeaponType = newWeaponType;
                OnWeaponChange?.Invoke();
            }
        }
    }

    public void NextWeapon()
    {
        ChangeWeapon(_currentWeaponType++);
    }

    public void PreviousWeapon()
    {
        ChangeWeapon(_currentWeaponType--);
    }

    public void DropCurrentWeapon()
    {
        if(HasWeaponInHand()){
            if(CurrentWeaponContainer.GetComponentInChildren<Weapon>().WeaponData.type != WeaponType.Melee){
                SetWeaponDropped();
                Rigidbody rb = CurrentWeaponContainer.GetComponentInChildren<Rigidbody>();
                rb.AddForce(Camera.main.transform.forward * dropWeaponForce,ForceMode.Impulse);
                rb.AddTorque(Camera.main.transform.right * dropWeaponForce,ForceMode.Impulse);
                CurrentWeaponContainer.GetComponentInChildren<Weapon>().gameObject.transform.parent = null;
                this.Invoke(() => EquipBestWeapon(_weaponPriority),0.25f);
                OnDropWeapon?.Invoke();
            }
        }
    }

    public void EquipBestWeapon(int[] weaponPriority)
    {
        for(int i=0;i<weaponPriority.Length;i++){
            foreach (GameObject equippedWeapon in _equippedWeapons){
                if((int)equippedWeapon.GetComponent<Weapon>().WeaponData.type == weaponPriority[i]){
                    ChangeWeapon(weaponPriority[i]);
                    return;
                }
            }
        }
    }

    void SetWeaponEquiped(GameObject weapon){
        Rigidbody rb = weapon.GetComponentInChildren<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.interpolation = RigidbodyInterpolation.None;
        rb.isKinematic = true;
        rb.useGravity = false;
        weapon.GetComponentInChildren<Animator>().enabled = true;
        weapon.layer = _weaponLayer;
        Destroy(weapon.GetComponent<Collectable>());
    }

    void SetWeaponDropped(){
        GameObject weaponToDrop = CurrentWeaponContainer.GetComponentInChildren<Weapon>().gameObject;
        for (int i = _equippedWeapons.Count-1; i >= 0; i--){
            if(_equippedWeapons[i].GetComponent<Weapon>().WeaponData.type ==
                weaponToDrop.GetComponent<Weapon>().WeaponData.type)
            {
                _equippedWeapons.Remove(_equippedWeapons[i]);
            }
        }
        Rigidbody rb = weaponToDrop.GetComponentInChildren<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.useGravity = true;
        CurrentWeaponContainer.GetComponentInChildren<Animator>().enabled = false;
        this.Invoke(() => weaponToDrop.layer = 0,0.25f);
        weaponToDrop.AddComponent<Collectable>();
    }

    bool HasWeaponInHand() => CurrentWeaponContainer.GetComponentInChildren<Weapon>();
}
