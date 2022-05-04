using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SightController : MonoBehaviour
{
    private Vector3 _sightPoint;    
    [HideInInspector] public Vector3 SightPoint {
         get{
             return _sightPoint;
         }
         set{
            _sightPoint = value;
         }
    }
}
