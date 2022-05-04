using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static void Invoke(this MonoBehaviour mb, System.Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }
 
    private static IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        f();
    }
}
