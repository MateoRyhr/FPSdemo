using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateGameMenu : MonoBehaviour
{
    public CreateGameData CreateGameData;
    [SerializeField] private TMP_InputField _redBotsQuantityInput;
    [SerializeField] private TMP_InputField _blueBotsQuantityInput;

    public void SetRedBotsQuantity(){
        if(int.TryParse(_redBotsQuantityInput.text.ToString(),out int number)){
           CreateGameData.redTeamBotsQuantity =  number;
        }
    }

    public void SetBlueBotsQuantity(){
        if(int.TryParse(_blueBotsQuantityInput.text.ToString(),out int number)){
           CreateGameData.blueTeamBotsQuantity =  number;
        }
    }
}
