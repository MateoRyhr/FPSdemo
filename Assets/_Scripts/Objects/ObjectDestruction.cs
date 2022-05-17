using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    public GameObject[] Pieces;
    Component[] ComponentsToDesactive;

    private void Awake(){
        ComponentsToDesactive = GetComponents<Component>();
    }

    public void DestroyObject(){
        foreach(Component component in ComponentsToDesactive){
            if(!component.Equals(transform)) Destroy(component);
        }
        foreach (GameObject piece in Pieces)
        {
            piece.SetActive(true);
        }
    }
}
