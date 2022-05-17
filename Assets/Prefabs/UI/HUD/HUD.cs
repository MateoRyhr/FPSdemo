using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [HideInInspector] public UnitHealth UnitHealth;
    [HideInInspector] public GameObject CurrentWeapon;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _bulletsText;

    void Update()
    {
        if(UnitHealth) _healthText.text = UnitHealth.Health.ToString();
        if(CurrentWeapon){
            if(CurrentWeapon.GetComponentInChildren<Weapon>()){
                _bulletsText.text =
                    $"{CurrentWeapon.GetComponentInChildren<Gun>().CurrentAmmo}/{CurrentWeapon.GetComponentInChildren<Gun>()._gunData.magSize}";
            }
        }

    }
}
