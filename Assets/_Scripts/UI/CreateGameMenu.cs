using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateGameMenu : MonoBehaviour
{
    public CreateGameData CreateGameData;
    [SerializeField] private RectTransform _mapList;
    [SerializeField] private TMP_InputField _redBotsQuantityInput;
    [SerializeField] private TMP_InputField _blueBotsQuantityInput;
    [SerializeField] private TMP_InputField _roundsInput;

    private void Start() {
        _redBotsQuantityInput.text = CreateGameData.redTeamBotsQuantity.ToString();
        _blueBotsQuantityInput.text = CreateGameData.blueTeamBotsQuantity.ToString();
        _roundsInput.text = CreateGameData.totalRounds.ToString();
        _mapList.GetChild(CreateGameData.map-1).GetComponent<Button>().Select();
    }

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

    public void SetRounds(){
        if(int.TryParse(_roundsInput.text.ToString(),out int number)){
           CreateGameData.totalRounds =  number;
        }
    }
}
