using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum GameState{
    InGame,
    MainMenu,
    PauseMenu
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState CurrentGameState;
    public CreateGameData CreateGameData;
    public MapData[] Maps;
    public GameMode CurrentGameMode;
    [SerializeField] GameMode[] gameModes;
    [SerializeField] SceneLoader _sceneLoader;
    [SerializeField] UnitsManager _unitManager;
    [SerializeField] BoolVariable _areInputsEnabled;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGameMode(0);
        UpdateState(GameState.MainMenu);     //-->uncomment on release
        // SetInitialState();                          //-->comment on release
    }

    public void UpdateState(GameState newState){
        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                break;
            case GameState.InGame:
                Time.timeScale = 1;
                break;
            case GameState.PauseMenu:
                Time.timeScale = 1;
                break;
            default:
                break;
        }
        CurrentGameState = newState;
    }

    public void UpdateState(string newState){
        switch (newState)
        {
            case "MainMenu":
                CurrentGameState = GameState.MainMenu;
                break;
            case "PauseMenu":
                CurrentGameState = GameState.PauseMenu;
                break;
            case "InGame":
                CurrentGameState = GameState.InGame;
                break;
            default:
                break;
        }
    }

    public void ExitGame(float delay){
        this.Invoke(() => Application.Quit(),delay);
    }

    public void StartSinglePlayerGame(){
        UpdateState(GameState.InGame);
        _sceneLoader.LoadScene(Maps[CreateGameData.map].mapName);
        // this.Invoke(() => _unitManager.RespawnUnits(),5f);
        // InstantiatePlayer(0.5f);
    }

    public void ResumeGame(float delay){
        this.Invoke(() => UpdateState(GameState.InGame),delay);
    }

    public void BackToMainMenu(float delay){
        this.Invoke(() => UpdateState(GameState.MainMenu),delay);
    }

    public void InitGameMode(){
        CurrentGameMode.StartGame();
    }

    public void LockMouse(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetMap(int map){
       CreateGameData.map = map;
    }

    public void SetGameMode(int gameMode){
        CurrentGameMode = gameModes[gameMode];
    }

    public void DisableMapCamera(){
        Camera.main.gameObject.SetActive(false);
    }

    public void FreezeTeams(){
        _areInputsEnabled.Value = false;
    }

    public void UnfreezeTeams(){
        _areInputsEnabled.Value = true;
    }
}
