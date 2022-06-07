using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListOfNames", menuName = "List of names")]
public class ListOfNames : ScriptableObject
{
    public string[] names;
    public bool[] nameUsed;

    public string GetRandomName(){
        string name = "-";
        while(name == "-"){
            int randomName = Random.Range(0,names.Length);
            if(!nameUsed[randomName]){
                name = names[randomName];
                nameUsed[randomName] = true;
            }
        }
        return name;
    }

    public string GetName(){
        string name = "-";
        for(int i = 0; i < names.Length; i++){
            if(!nameUsed[i]){
                name = names[i];
                nameUsed[i] = true;
                return name;
            }
        }
        return name;
    }

    public void ResetValues(){
        for(int i = 0; i < nameUsed.Length; i++){
            nameUsed[i] = false;
        }
    }
}
