using UnityEngine;

public class SightRadius : MonoBehaviour
{
    [SerializeField] RectTransform _sightRadiusImage;
    [SerializeField] FloatVariable _sightRadius;

    void Update()
    {
        _sightRadiusImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,_sightRadius.Value);
        _sightRadiusImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,_sightRadius.Value);
    }
}
