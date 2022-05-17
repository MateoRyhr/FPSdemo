using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject MainMenu;
    public GameObject CreateGame;
    // public GameObject HostGame;
    public GameObject PauseMenu;
    public GameObject SelectTeamMenu;
    public GameObject HUD;

    private void Awake() {
        if(Instance == null){
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    #region MainMenu

    public void EnableMainMenu(float delay){
        this.Invoke(() => MainMenu.SetActive(true),delay);
    }

    public void DisableMainMenu(float delay){
        this.Invoke(() => MainMenu.SetActive(false),delay);
    }

    #endregion

    #region Pause Menu

    public void EnablePauseMenu(float delay){
        this.Invoke(() => PauseMenu.SetActive(true),delay);
    }

    public void DisablePauseMenu(float delay){
        this.Invoke(() => PauseMenu.SetActive(false),delay);
    }

    #endregion

    #region HUD

    public void EnableHUD(float delay){
        this.Invoke(() => HUD.SetActive(true),delay);
    }

    public void DisableHUD(float delay){
        this.Invoke(() => HUD.SetActive(false),delay);
    }

    #endregion

    #region CreateGame

    public void EnableCreateGame(float delay){
        this.Invoke(() => CreateGame.SetActive(true),delay);
    }

    public void DisableCreateGame(float delay){
        this.Invoke(() => CreateGame.SetActive(false),delay);
    }

    #endregion

    #region SelectTeamMenu

    public void EnableSelectTeamMenu(float delay){
        this.Invoke(() => SelectTeamMenu.SetActive(true),delay);
    }

    public void DisableSelectTeamMenu(float delay){
        this.Invoke(() => SelectTeamMenu.SetActive(false),delay);
    }

    #endregion
}
