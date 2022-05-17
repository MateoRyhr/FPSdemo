using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<ICollector>() != null){
            other.gameObject.GetComponent<ICollector>().Collect(GetComponent<Collectable>());
        }
    }
}
