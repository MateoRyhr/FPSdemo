using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunReloadEvents : MonoBehaviour
{
    public UnityEvent OnClipOut;
    public UnityEvent OnClipIn;
    public UnityEvent OnSlide;

    public void ClipOut(){
        OnClipOut.Invoke();
    }

    public void ClipIn(){
        OnClipIn.Invoke();
    }

    public void Slide(){
        OnSlide.Invoke();
    }
}
