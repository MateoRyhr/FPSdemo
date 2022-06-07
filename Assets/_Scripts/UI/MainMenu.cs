using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nicknameInput;

    private void Start() {
        _nicknameInput.text = "Player";
    }

    public void SetPlayerNickname(){
        GameManager.Instance.playerNickname = _nicknameInput.text;
    }
}
