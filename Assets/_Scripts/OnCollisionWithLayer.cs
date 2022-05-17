using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionWithLayer : MonoBehaviour
{
    [SerializeField] int _layer;
    public UnityEvent OnCollisionEnterEvent;
    public UnityEvent OnCollisionStayEvent;
    public UnityEvent OnCollisionExitEvent;


    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == _layer){
            OnCollisionEnterEvent?.Invoke();
        }
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.layer == _layer)
            OnCollisionStayEvent?.Invoke();
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.layer == _layer)
            OnCollisionExitEvent?.Invoke();
    }
}
