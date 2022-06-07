using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerWithLayer : MonoBehaviour
{
    [SerializeField] int _layer;
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerStayEvent;
    public UnityEvent OnTriggerExitEvent;

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == _layer){
            OnTriggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.layer == _layer)
            OnTriggerStayEvent?.Invoke();
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == _layer)
            OnTriggerExitEvent?.Invoke();
    }
}
