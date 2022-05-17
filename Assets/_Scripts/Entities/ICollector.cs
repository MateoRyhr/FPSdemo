using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollector
{
    public abstract void Collect(Collectable collectableObject);
}
