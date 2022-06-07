using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisionDetection : MonoBehaviour
{
    [SerializeField] private int _layerToDetect;
    public bool Collision;

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.layer == _layerToDetect){
            Collision = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        Collision = false;
    }
}
