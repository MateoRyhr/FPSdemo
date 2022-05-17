using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitToCount : MonoBehaviour
{
    public IntVariable unitsQuantity;
    public void Plus() => unitsQuantity.Value++;
    public void Minus() => unitsQuantity.Value--;
}
